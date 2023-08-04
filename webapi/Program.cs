using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using WebApi.Context;
using WebApi.Extensions;
using WebApi.Initializers;
using WebApi.Managers.Account;
using WebApi.Managers.AI;
using WebApi.Managers.Learner;
using WebApi.Managers.Learner.ByLanguage;
using WebApi.Managers.RoleManagers;
using WebApi.Managers.UserManagers;
using WebApi.Models.Database;
using WebApi.Services.Account;
using WebApi.Services.AI;
using WebApi.Services.Learner;
using WebApi.Services.Learner.ByLanguage;
using WebApi.Services.RoleServices;
using WebApi.Services.UserServices;
using WebApi.Settings;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

IServiceCollection services = builder.Services;

string connection = config.GetConnectionString("DefaultConnection")!;
services.AddDbContext<LearnerContext>(opts =>
{
    opts.UseSqlServer(connection);
    opts.EnableSensitiveDataLogging();
});

services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<LearnerContext>()
    .AddDefaultTokenProviders();

var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>()!;
services.AddSingleton(jwtSettings);

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
//services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
    options.TokenValidationParameters = (TokenValidationParameters)jwtSettings
);

var chatGPTSettings = config.GetSection("ChatGPTSettings").Get<ChatGPTSettings>()!;
services.AddSingleton(chatGPTSettings);

services.AddHttpContextAccessor();

services.AddScoped<IJwtService, JwtManager>();
services.AddScoped<IRoleService, RoleManager>();
services.AddLearnerServices();
services.AddUserServices();
services.AddChatGPTService();
services.AddScoped<IAccountService, AccountManager>();

string[] originSettings = config.GetSection("OriginSettings").Get<string[]>()!;

services.AddCors(options =>
    options.AddDefaultPolicy(builder =>
        builder.WithOrigins(originSettings)
               .AllowAnyHeader()
               .AllowAnyMethod()
));

services.AddControllers().AddNewtonsoftJson(options =>
      options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
   );
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseRouting();

using (IServiceScope serviceScope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = serviceScope.ServiceProvider;

    var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
    await RolesInitializer.InitializeAsync(roleManager);

    string adminName = config.GetValue<string>("Admin:Name")!;
    string adminPassword = config.GetValue<string>("Admin:Password")!;
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    await UsersInitializer.AdminInitializeAsync(userManager, chatGPTSettings, adminName, adminPassword);
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

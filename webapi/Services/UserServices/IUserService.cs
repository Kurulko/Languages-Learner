using System.Security.Claims;
using WebApi.Models.Account;
using WebApi.Models.Database;
using WebApi.Models.Database.Learner;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Models.Helpers;

namespace WebApi.Services.UserServices;

public interface IUserService : IBaseUserService, IUserRolesService, IUserPasswordService, IUsedUserService, IUserModelsService
{
}
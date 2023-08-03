using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using WebApi.Models.Account;
using WebApi.Models.Database;
using WebApi.Services.Account;
using System.Text;
using Azure.Core;
using Microsoft.Win32;

namespace WebApi.Controllers.Account;

[AllowAnonymous]
public class AccountController : ApiController
{
    readonly IAccountService accManager;
    readonly IJwtService jwtService;
    public AccountController(IAccountService accManager, IJwtService jwtService)
        => (this.accManager, this.jwtService) = (accManager, jwtService);


    [AllowAnonymous]
    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register(RegisterModel register)
        => await ReturnOkTokenIfEverithingIsGood(async () => await accManager.RegisterUserAsync(register), register);


    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login(LoginModel login)
        => await ReturnOkTokenIfEverithingIsGood(async () => await accManager.LoginUserAsync(login), login);

    
    [Authorize]
    [HttpPost(nameof(Logout))]
    public async Task<IActionResult> Logout()
        => await ReturnOkIfEverithingIsGood(accManager.LogoutUserAsync);

    async Task<IActionResult> ReturnOkTokenIfEverithingIsGood(Func<Task<IEnumerable<string>>> action, AccountModel accountModel)
        => await ReturnOkIfEverithingIsGood(async () =>
        {
            IEnumerable<string> roles = await action();
            var tokenInfo = jwtService.GenerateJwtToken((User)accountModel, roles);
            return new { tokenInfo.token, tokenInfo.expirationDays };
        });
}
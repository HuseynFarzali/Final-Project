using SMS.Authentication.Models;
using SMS.DAL.Data.Entities.Concrete;
using SMS.Tools.Enums;
using SMS.Tools.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Authentication.Services.Contracts
{
    public interface IAppAuthenticationService
    {
        Task<AppUser> Authenticate(LoginRequestModel loginRequest);
        Task<(AppUser, Result)> TryAuthenticate(LoginRequestModel loginRequest);

        string CreateToken(TokenRequestModel tokenRequest);
        (string, Result) TryCreateToken(TokenRequestModel tokenRequest);

        Task<string> Register(RegisterRequestModel registerRequest);
        Task<(string, Result)> TryRegister(RegisterRequestModel registerRequest);

        Task<string> Login(LoginRequestModel loginRequest);
        Task<(string, Result)> TryLogin(LoginRequestModel loginRequest);

        Task<AppUser?> GetCurrentUser();
    }
}

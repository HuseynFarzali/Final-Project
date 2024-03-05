using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SMS.Authentication.Models;
using SMS.Authentication.Services.Contracts;
using SMS.Authentication.Setting_Models;
using SMS.DAL.Data.Entities.Concrete;
using SMS.DAL.Repositories.Contracts;
using SMS.Tools.Enums;
using SMS.Tools.Extensions;
using SMS.Tools.Tools;
using SMS.WebTools.Attributes;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SMS.Authentication.Services
{
    [Injectible(
        Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped,
        Implements = typeof(IAppAuthenticationService))]
    public class AppAuthenticationManager : IAppAuthenticationService
    {
        private readonly JwtSettings _settings;
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;

        public AppAuthenticationManager(IOptions<JwtSettings> jwtOptions, IAppUserRepository userRepository, IMapper mapper, IHttpContextAccessor accessor)
        {
            _settings = jwtOptions.Value;
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContext = accessor.HttpContext!;
        }

        public async Task<AppUser?> GetCurrentUser()
        {
            var emailOfCurrentUser = _httpContext.User.Claims.Where(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").SingleOrDefault()?.Value;

            if (emailOfCurrentUser == null)
                return null;

            var matchingUser = (await _userRepository.GetRange(predicate: user => user.Email == emailOfCurrentUser)).SingleOrDefault()
                ?? throw new ArgumentNullException($"Not found any matching AppUser with email: [{emailOfCurrentUser}]");

            return matchingUser;
        }

        public async Task<AppUser> Authenticate(LoginRequestModel model)
        {
            var potentialUsers = await _userRepository.GetRange(predicate: user => user.Email == model.Email);
            var foundUser = potentialUsers.SingleOrDefault()
                ?? throw new ArgumentException("Provided email does not exist in the database.");

            //await Console.Out.WriteAsync($"\nHashed password from the database: {foundUser.HashedPassword}\n");

            //var modelHashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            //await Console.Out.WriteLineAsync($"\nHashed password of the login model: {BCrypt.Net.BCrypt.HashPassword(model.Password)}\n");

            if (model.Password != foundUser.HashedPassword)
                throw new ArgumentException("Provided password is incorrect.");

            return foundUser;
        }

        public async Task<(AppUser, Result)> TryAuthenticate(LoginRequestModel model)
        {
            try
            {
                var user = await Authenticate(model);
                return (user, Result.Success);
            }
            catch (Exception ex)
            {
                return (null, Result.In(ex));
            }
        }

        public string CreateToken(TokenRequestModel model)
        {
            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.Email, model.Email),
                new Claim("isAdmin", model.IsAdmin.ToString()),
                new Claim("userType", model.UserType.ToString())
            };

            if (model.UserType is Tools.Enums.UserType.Student && model.IsAdmin)
                throw new Exception("Student type users cannot be assigned as admin.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key
                ?? throw new ArgumentNullException("Key for token generation cannot be null.")));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _settings.Issuer
                        ?? throw new ArgumentNullException("Issuer for token generation cannot be null."),
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_settings.DurationTimeInDays),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public (string, Result) TryCreateToken(TokenRequestModel model)
        {
            try
            {
                string token = CreateToken(model);
                return (token, Result.Success);
            }
            catch (Exception ex)
            {
                return (null, Result.In(ex))!;
            }
        }

        public async Task<string> Register(RegisterRequestModel model)
        {
            if (model is null)
                throw new NullReferenceException(nameof(model));

            var userExistWithSameEmail = (await _userRepository.GetRange(predicate: user => user.Email == model.Email)).Any();

            if (userExistWithSameEmail) throw new ArgumentException($"{model.Email} is already registered.");

            var mappedUserObject = _mapper.Map<AppUser>(model);

            //mappedUserObject.HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            mappedUserObject.HashedPassword = model.Password;
            await _userRepository.Add(mappedUserObject);

            var tokenRequest = _mapper.Map<TokenRequestModel>(model);
            return CreateToken(tokenRequest);
        }

        public async Task<(string, Result)> TryRegister(RegisterRequestModel model)
        {
            try
            {
                var token = await Register(model);
                return (token, Result.Success);
            }
            catch (Exception ex)
            {
                return (null, Result.In(ex));
            }
        }

        public async Task<string> Login(LoginRequestModel model)
        {
            var appUser = await Authenticate(model);

            var tokenRequestModel = _mapper.Map<TokenRequestModel>(appUser);
            return CreateToken(tokenRequestModel);
        }
        
        public async Task<(string, Result)> TryLogin(LoginRequestModel model)
        {
            try
            {
                var token = await Login(model);
                return (token, Result.Success);
            }
            catch (Exception ex)
            {
                return (null, Result.In(ex));
            }
        }
    }
}

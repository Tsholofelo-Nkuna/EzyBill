using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.DataTranserObjects.Auth;
using EzyBill.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.T4Templating;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json;

namespace EzyBill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(AuthenticationSchemes = "OIDC")]
    public class TokenController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _config;
        private readonly ILogger<TokenController> _logger;
        public TokenController(IJwtService jwtService, IConfiguration config, ILogger<TokenController> logger)
        {
            _jwtService = jwtService;
            _config = config;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public TokenResponseDto Token(UserCredentialsDto credentials)
        {
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, credentials.Username), 
            };
           
            var token =  this._jwtService.GenerateToken(userClaims, TimeSpan.FromMinutes(Convert.ToInt32(_config["JwtTokenOptions:TokenTimeSpanMinutes"])));
            var refreshToken = this._jwtService.GenerateToken(userClaims, TimeSpan.FromMinutes(Convert.ToInt32(_config["JwtTokenOptions:RefreshTokenTimeSpanMinutes"])));
            Response.Cookies.Append(JwtService.RefreshTokenCookieKey, refreshToken);
            Response.Cookies.Append(JwtService.TokenCookieKey, token);
            return new TokenResponseDto { Token = token, RefreshToken = refreshToken };
        }

        [HttpGet("[action]")]
        public string TestEndPoint()
        {
            var userClaims = this.HttpContext.User.Claims;
            var name = userClaims.FirstOrDefault(x => x.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Name)?.Value;
            return $"{name}";
        }

    }
}

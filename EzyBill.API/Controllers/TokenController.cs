using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.DataTranserObjects.Auth;
using EzyBill.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EzyBill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _config;
        public TokenController(IJwtService jwtService, IConfiguration config) {
            _jwtService = jwtService;
            _config = config;
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

    }
}

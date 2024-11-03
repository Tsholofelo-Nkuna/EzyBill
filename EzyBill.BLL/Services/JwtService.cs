using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EzyBill.BLL.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly JwtOptions _jwtOptions;
        private readonly SymmetricSecurityKey _signingKey;
        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _jwtOptions = jwtOptions.Value;
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtOptions.SecretKey)); 
            this.SigningCredentials = new SigningCredentials(_signingKey,SecurityAlgorithms.HmacSha256);
            this.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = this._signingKey,
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
            };

        }
        public string GenerateToken(IEnumerable<Claim> claims, TimeSpan validFor)
        {
           
          var token = new JwtSecurityToken(
              this._jwtOptions.Issuer,
              null, claims, 
              null, 
              DateTime.Now.Add(validFor), 
              this.SigningCredentials
           );
          return this._jwtSecurityTokenHandler.WriteToken(token); 
        }

        public ClaimsPrincipal? ValidateToken(string tokenStr, out SecurityToken? token)
        {
            try
            {
                return this._jwtSecurityTokenHandler.ValidateToken(tokenStr,this.TokenValidationParameters, out token); 
            }
            catch(Exception ex)
            {
                token = null;
                return null;
            }
        }

       public (string? token, string? refreshToken) NewTokenPair(string refreshToken, TimeSpan tokenValidFor, TimeSpan refreshTokenValidFor)
        {
            var validationResult = this.ValidateToken(refreshToken, out _);
            if (validationResult != null)
            {
                return (this.GenerateToken(validationResult.Claims, tokenValidFor), this.GenerateToken(validationResult.Claims, refreshTokenValidFor));
            }
            else
            {
                return (null, null);
            }
           
        }

        public SigningCredentials SigningCredentials { get; init; }
        public TokenValidationParameters TokenValidationParameters { get; init; }
        public static string TokenCookieKey { get; } = "Token";
        public static string RefreshTokenCookieKey { get; } = "RefreshToken";
    }
}
 
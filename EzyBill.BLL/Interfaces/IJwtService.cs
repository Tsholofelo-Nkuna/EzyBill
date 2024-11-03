using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace EzyBill.BLL.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Generate a new token valid for the provided time span indicated by validFor
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="validFor"></param>
        /// <returns></returns>
        public string GenerateToken(IEnumerable<Claim> claims, TimeSpan validFor);
        /// <summary>
        /// Validates the given token string
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <param name="token"></param>
        /// <returns>returns a ClaimsPrincipal if tokenStr is valid otherwhise returns a null </returns>
        public ClaimsPrincipal? ValidateToken(string tokenStr, out SecurityToken? token);
        /// <summary>
        /// Generates a new pair of tokens if  the given refreshToken is valid
        /// </summary>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        /// <param name="refreshTokenValidFor"></param>
        /// <param name="tokenValidFor"></param>
        /// <returns></returns>
        public (string? token, string? refreshToken) NewTokenPair(string refreshToken, TimeSpan tokenValidFor, TimeSpan refreshTokenValidFor);
    }
}

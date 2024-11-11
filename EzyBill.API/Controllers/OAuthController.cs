using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace EzyBill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly ILogger<OAuthController> _logger;
        public OAuthController(ILogger<OAuthController> logger) { 
          this._logger = logger;
        }
        [HttpGet("git-cb")]
        public void GitCallBack(string code)
        {
            try
            {
                _logger.LogInformation(code);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"Inputs:\n {code}");
               
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> Authorize(string client_id, string? redirect_uri, string? scope, string? response_type, string state)
        {
            try
            {
                var authResult = await this.HttpContext.AuthenticateAsync();
                if (authResult.Succeeded)
                {
                    _logger.LogInformation($"{client_id} {redirect_uri} {scope}");
                }
                else
                {
                    return Unauthorized();
                }

                return Empty;
            }
            catch (Exception ex)
            {

               return Empty;
            }
        }
    }
}

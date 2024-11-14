using Azure.Core;
using EzyBill.API.Models.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mono.TextTemplating;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace EzyBill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : Controller
    {
        private readonly ILogger<OAuthController> _logger;
        public OAuthController(ILogger<OAuthController> logger) { 
          this._logger = logger;
        }
        [HttpGet("google-cb")]
        public void GoogleCallBack(string access_token, string token_type, string id_token, string state)
        {

            try
            {
                _logger.LogInformation($"Inputs:\n{access_token}\n{token_type}\n{id_token}\n{state}");
               
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"Inputs:\n{access_token}\n{token_type}\n{id_token}\n{state}");
               
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
                    
                  
                   var link = Url.Action(nameof(Login), "OAuth", new {redirect_uri = redirect_uri});
                   return Redirect(link!);
                    
                }

                return Empty;
            }
            catch (Exception ex)
            {

               return Empty;
            }
        }

        [HttpGet("[action]")]
        public ViewResult Login(string? redirect_uri)
        {
            
            return View(new LoginViewModel());
        }

        [HttpPost("[action]")]
        public void Login([FromQuery] string? redirect_uri,[FromForm] LoginViewModel model)
        {
            _logger.LogInformation(redirect_uri);
        }
    }
}

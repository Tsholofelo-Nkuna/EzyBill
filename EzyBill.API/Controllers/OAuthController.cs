using Azure.Core;
using EzyBill.API.Models.ViewModels.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Mono.TextTemplating;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text.Json;

namespace EzyBill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : Controller
    {
        private readonly ILogger<OAuthController> _logger;
        private readonly IConfiguration _configuration;
        public OAuthController(ILogger<OAuthController> logger, IConfiguration configuration) { 
          this._logger = logger;
          this._configuration = configuration;
        }
        [HttpPost("[action]")]
        public void GoogleCallBack()
        {

            try
            {
                this._logger.LogInformation(Request.Form.ToString());
                var payload = Request.Form;
                var userEndPoint = this._configuration["OIDC:Google:userinfo_endpoint"];
                var accessToken = payload.ContainsKey("access_token") ? payload["access_token"].ToString() : string.Empty;
                var idToken = payload.ContainsKey("id_token") ? payload["id_token"].ToString() : string.Empty;
                
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"Input:\n {JsonSerializer.Serialize(Request.Form)}");
                //return "";
               
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

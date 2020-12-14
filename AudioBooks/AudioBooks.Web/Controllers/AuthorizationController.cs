using AudioBooks.Web.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AudioBooks.Web.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthorizationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        public async Task<IActionResult> AuthInfoAsync()
        {
            var idpClient = _httpClientFactory.CreateClient("LinIDPClient");
            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();
            if (metaDataResponse.IsError)
            {
                throw new ApplicationException("Problem accessing the discovery endpoint.", metaDataResponse.Exception);
            }
            var idToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            //get access token

            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            //call user info end point
            var userInfo = await idpClient.GetUserInfoAsync(new UserInfoRequest { Address = metaDataResponse.UserInfoEndpoint, Token = accessToken });
            if (userInfo.IsError)
            {
                throw new ApplicationException("Problem accessing the user info endpoint.", userInfo.Exception);
            }

            var model = new AuthInfoViewModel();
            model.IdToken = idToken;
            model.AccesToken = accessToken;
            model.Email = userInfo.Claims.FirstOrDefault(u => u.Type == "email")?.Value;
            model.Role = userInfo.Claims.FirstOrDefault(u => u.Type == "role")?.Value;
            model.Operations = userInfo.Claims.FirstOrDefault(u => u.Type == "operations")?.Value;
            return View("AuthInfo", model);
        }
    }
}

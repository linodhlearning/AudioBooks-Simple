using AudioBooks.Web.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AudioBooks.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)

        {
            _logger = logger;
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserInfo()
        { 
            var idpClient = _httpClientFactory.CreateClient(Constants.APIClientNames.IDPClient);
            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();
            if (metaDataResponse.IsError)
            {
                throw new ApplicationException("Problem accessing the discovery endpoint.", metaDataResponse.Exception);
            }
            //get access token
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            //call user info end point
            var userInfo = await idpClient.GetUserInfoAsync(new UserInfoRequest { Address = metaDataResponse.UserInfoEndpoint, Token = accessToken });
            if (userInfo.IsError)
            {
                throw new ApplicationException("Problem accessing the user info endpoint.", userInfo.Exception);
            }
            
            var model = new UserInfoViewModel();
            model.GivenName = userInfo.Claims.FirstOrDefault(u => u.Type == "given_name")?.Value;
            model.FamilyName = userInfo.Claims.FirstOrDefault(u => u.Type == "family_name")?.Value;
            model.Address = userInfo.Claims.FirstOrDefault(u => u.Type == "address")?.Value;
            model.Email = userInfo.Claims.FirstOrDefault(u => u.Type == "email")?.Value;
            model.Gender = userInfo.Claims.FirstOrDefault(u => u.Type == "gender")?.Value;
            return await Task.Run(() => View("UserInformation", model));//bad usage
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}

using AudioBooks.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AudioBooks.Web.Controllers
{

    [Authorize]
    public class LibraryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LibraryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IActionResult> Index()
        {
            await WriteOutIdentityInfo(); //to learn

            var httpClient = _httpClientFactory.CreateClient("AudioBooksAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/audiobooks");
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);//.ConfigureAwait(false);// not needed for code .net core 

            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return View(new AudioBookSummaryListViewModel(await JsonSerializer.DeserializeAsync<List<Model.AudioBookItemSummaryModel>>(responseStream)));
            }
        }

        public async Task WriteOutIdentityInfo()
        {
            var idToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            System.Diagnostics.Debug.WriteLine($"ID Token: {idToken}");
            foreach (var claim in User.Claims)
            {
                System.Diagnostics.Debug.WriteLine($"Claim Type: {claim.Type} - Claim Value: {claim.Value}");
            }
        }
    }
} 

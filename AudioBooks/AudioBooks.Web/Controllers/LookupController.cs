using AudioBooks.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AudioBooks.Web.Controllers
{
    public class LookupController : Controller
    {
 
        private readonly IHttpClientFactory _httpClientFactory;

        public LookupController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IActionResult> Index()
        {  
            var httpClient = _httpClientFactory.CreateClient("AudioBooksAPIClient");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/lookupdata");
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);//.ConfigureAwait(false);// not needed for code .net core 

            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var model = await JsonSerializer.DeserializeAsync<Model.LookupDataCacheModel>(responseStream);
                var viewModel = new LookupDataCacheViewModel(model);
                return View(viewModel);
            }
        } 

    }
}

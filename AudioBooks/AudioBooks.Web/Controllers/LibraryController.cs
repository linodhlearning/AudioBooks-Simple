using AudioBooks.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AudioBooks.Web.Controllers
{
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
            var httpClient = _httpClientFactory.CreateClient("AudioBooksAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/audiobooks");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);//.ConfigureAwait(false);// not needed for code .net core 

            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                //var summaryList = await JsonSerializer.DeserializeAsync<List<Model.AudioBookItemSummaryModel>>(responseStream);
                //var viewModel = new AudioBookSummaryListViewModel(summaryList);
                //return View(viewModel);

                return View(new AudioBookSummaryListViewModel(await JsonSerializer.DeserializeAsync<List<Model.AudioBookItemSummaryModel>>(responseStream)));
            }
        }

    }
}

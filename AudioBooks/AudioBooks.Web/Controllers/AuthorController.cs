using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AudioBooks.Web.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        [HttpGet]
        public ActionResult AddAuthor()
        {
            return View("AddAuthor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAuthor(Model.LookupItemModel author)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient(Constants.APIClientNames.AudioBooksAPIClient);
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/lookupdata/addauthor");
                request.Content = new StringContent(JsonConvert.SerializeObject(author), System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Lookup");
            }
            else
            {
                return View("AddAuthor", author);
            }
        }

 

        [HttpGet]
        public ActionResult EditAuthor(int id, string name, string description)
        {
            if (id == 0)
            {
                return RedirectToAction("AddAuthor");
            }
            else
            {
                return View("EditAuthor", new Model.LookupItemModel { Id = id, Name = name, Description = description });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAuthor(Model.LookupItemModel author)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient(Constants.APIClientNames.AudioBooksAPIClient);
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/lookupdata/updateauthor");
                request.Content = new StringContent(JsonConvert.SerializeObject(author), System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Lookup");
            }
            else
            {
                return View("EditAuthor", author);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (id > 0)
            {
                var httpClient = _httpClientFactory.CreateClient(Constants.APIClientNames.AudioBooksAPIClient);
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/lookupdata/deleteauthor");                 
                request.Content = new StringContent(JsonConvert.SerializeObject(new { id }), System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();  
            }
            return RedirectToAction("Index", "Lookup");
        }

    }
}

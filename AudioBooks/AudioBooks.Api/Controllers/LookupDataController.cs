using AudioBooks.Api.Repositories.Contracts;
using AudioBooks.Model;
using AudioBooks.Response;
using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AudioBooks.Api.Controllers
{

    [Route("api/lookupdata")]
    [ApiController]
    //[Authorize]
    public class LookupDataController : BaseController
    {
        private readonly ILookupDataRepository _lookupDataRepository;

        public LookupDataController(ILookupDataRepository lookupDataRepository, TelemetryClient telemetry, IMapper mapper) : base(telemetry, mapper)
        {
            this._lookupDataRepository = lookupDataRepository ?? throw new ArgumentNullException(nameof(lookupDataRepository));
        }


        [HttpGet]
        public async Task<IActionResult> GetLookupDataCacheData()
        {
            try
            {
                var authors = await _lookupDataRepository.GetAuthors();
                var categories = await _lookupDataRepository.GetCategories();
                var publishers = await _lookupDataRepository.GetPublishers();

                // running the tasks in parallel -cant use this as AddDbContext is used and not set as transient
                // await Task.WhenAll(authors, categories, publishers); 

                var model = new LookupDataCacheModel
                {
                    Authors = authors,
                    Catgegories = categories,
                    Publishers = publishers
                };
                return Ok(model);
            }
            catch (Exception ex)
            {
                this._telemetry.TrackException(ex);
                var response = LookupResponse<AudioBookItemModel>.BuildErrorResponse("FailedGet", "Could not retrieve Authors data");
                return BadRequest(response);
            }
        }

    }
}

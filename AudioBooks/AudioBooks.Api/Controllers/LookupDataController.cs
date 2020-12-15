using AudioBooks.Api.Repositories.Contracts;
using AudioBooks.Model;
using AudioBooks.Response;
using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AudioBooks.Api.Controllers
{
     
    [Route("api/[controller]")]
    [ApiController]
    //[Microsoft.AspNetCore.Authorization.Authorize]
    public class LookupDataController : BaseController
    {
        private readonly ILookupDataRepository _lookupDataRepository;

        public LookupDataController(ILookupDataRepository lookupDataRepository, TelemetryClient telemetry, IMapper mapper) : base(telemetry, mapper)
        {
            this._lookupDataRepository = lookupDataRepository ?? throw new ArgumentNullException(nameof(lookupDataRepository));
        }


        [HttpGet(Name="lookupdata")]
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

        [HttpPost("addauthor")]
        public async Task<IActionResult> AddAuthor([FromBody] LookupItemModel author)
        {
            try
            {
                var id = await _lookupDataRepository.CreateAuthor(author);
                return Ok(id);
            }
            catch (Exception ex)
            {
                this._telemetry.TrackException(ex);
                var response = LookupResponse<AudioBookItemModel>.BuildErrorResponse("FailedAddAuthor", "Could not add Author");
                return BadRequest(response);
            }
        }

        [HttpPost("updateauthor")]
        public async Task<IActionResult> UpdateAuthor([FromBody] LookupItemModel author)
        {
            try
            {
                var status = await _lookupDataRepository.UpdateAuthor(author);
                return Ok(status);
            }
            catch (Exception ex)
            {
                this._telemetry.TrackException(ex);
                var response = LookupResponse<AudioBookItemModel>.BuildErrorResponse("FailedUpadteAuthor", "Could not update Author");
                return BadRequest(response);
            }
        }
        //[HttpPost("deleteauthor")] 
        [HttpDelete("deleteauthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var status = await _lookupDataRepository.DeleteAuthor(id);
                return Ok(status);
            }
            catch (Exception ex)
            {
                this._telemetry.TrackException(ex);
                var response = LookupResponse<AudioBookItemModel>.BuildErrorResponse("FailedUpadteAuthor", "Could not remove Author");
                return BadRequest(response);
            }
        }

    }
}

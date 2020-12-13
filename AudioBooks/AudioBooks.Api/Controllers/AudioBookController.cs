using AudioBooks.Model;
using AudioBooks.Api.Repositories;
using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AudioBooks.Response;
using Microsoft.AspNetCore.Authorization;

namespace AudioBooks.Api.Controllers
{
    [Route("api/audiobooks")]
    [ApiController]
    [Authorize]
    public class AudioBookController : BaseController
    {
        private readonly IAudioBookRepository _audioBookRepository;

        public AudioBookController(IAudioBookRepository audioBookRepository, TelemetryClient telemetry, IMapper mapper) : base(telemetry, mapper)
        {
            this._audioBookRepository = audioBookRepository ?? throw new ArgumentNullException(nameof(audioBookRepository));
        }


        [HttpGet()]
        public async Task<IActionResult> GetAudioBooks()
        {
            try
            {
                // get data from repo
                var data = await _audioBookRepository.GetAudioBooks();
                // map to model
                var result = _mapper.Map<IEnumerable<AudioBookItemSummaryModel>>(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this._telemetry.TrackException(ex);
                var response = LookupResponse<AudioBookItemSummaryModel>.BuildErrorResponse("FailedGet", "Could not retrieve all audiobooks info");
                return BadRequest(response);
            }
        }

        [HttpGet("{id}", Name = "GetAudioBook")]
        public async Task<IActionResult> GetAudioBook(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            try
            {
                // this._telemetry.TrackEvent(new EventTelemetry($"AudioBookController : GetAudioBook - Start retrieving audiobook with Id : {id }"));               
                var data = await _audioBookRepository.GetAudioBookById(id);
                var result = _mapper.Map<AudioBookItemModel>(data);
                //  this._telemetry.TrackEvent(new EventTelemetry($"AudioBookController : GetAudioBook - Finish retrieving audiobook with Id : {id}"));
                return Ok(result);
            }
            catch (Exception ex)
            {
                this._telemetry.TrackException(ex);
                var response = LookupResponse<AudioBookItemModel>.BuildErrorResponse("FailedGet", "Could not retrieve audiobook data");
                return BadRequest(response);
            }
        }

    }
}

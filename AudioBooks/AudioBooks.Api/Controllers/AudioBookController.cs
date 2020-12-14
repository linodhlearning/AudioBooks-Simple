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
using AudioBooks.Api.Repositories.Contracts;

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


        [HttpPost(Name = "AddAudioBook")]
        public async Task<IActionResult> CreateAudioBook([FromBody] AudioBookItemModel model)
        {
            try
            {
                var audioBookDomain = _mapper.Map<Domain.AudioBook>(model);
                var audioBookId = await _audioBookRepository.CreateAudioBook(audioBookDomain);
                return Ok(audioBookId);
            }
            catch (Exception ex)
            {
                this._telemetry.TrackException(ex);
                var response = LookupResponse<AudioBookItemModel>.BuildErrorResponse("FailedAddAudioBook", "Could not add a new audiobook");
                return BadRequest(response);
            }
        }

        [HttpPut("{id}", Name = "UpdateAudioBook")]
        public async Task<IActionResult> UpdateAudioBook(int id, [FromBody] AudioBookItemModel model)
        {
            if (id != model.AudioBookId)
            {
                return BadRequest();
            }

            try
            {
                var audioBookDomain = _mapper.Map<Domain.AudioBook>(model);
                var status = await _audioBookRepository.UpdateAudioBook(audioBookDomain);
                return Ok(status);
            }
            catch (Exception ex)
            {
                this._telemetry.TrackException(ex);
                var response = LookupResponse<AudioBookItemModel>.BuildErrorResponse("FailedAudioBookUpdate", "Could not update the audiobook");
                return BadRequest(response);
            }
        }
    }
}

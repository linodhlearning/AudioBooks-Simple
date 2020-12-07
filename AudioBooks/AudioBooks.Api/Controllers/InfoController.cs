using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AudioBooks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : BaseController
    {
        public InfoController(TelemetryClient telemetry, IMapper mapper) : base(telemetry, mapper)
        {
        }

        [HttpGet("telemetry/exception")]
        public IActionResult LogTelemetry()
        {
            try
            {
                throw new Exception("Debug: Tracking Excpetion via TelemetryClient");
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
            }

            return Ok("Debug: Tracking Excpetion via TelemetryClient");
        }

        [HttpGet("telemetry/event")]
        public IActionResult LogTelemetryEvent()
        {
            // This works fine.
            _telemetry.TrackEvent(new EventTelemetry("Debug: Tracking Custom Event"));
            return Ok("Debug: Tracking Custom Event");
        }

        [HttpGet("error")]
        public IActionResult RaiseError()
        {
            // This works fine.
            throw new Exception("Debug: Exception Thrown");
        }

        [HttpGet("health")]
        public IActionResult CheckHealth()
        {
            // to help with health check when placed in dockers
            return Ok("Health Check: OK!");
        }
    }
}

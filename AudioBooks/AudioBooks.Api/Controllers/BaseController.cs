using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AudioBooks.Api.Controllers
{
    public class BaseController : ControllerBase
    {

        protected readonly TelemetryClient _telemetry;

        protected readonly IMapper _mapper;
        public BaseController(TelemetryClient telemetry, IMapper mapper)
        {
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._telemetry = telemetry ?? throw new ArgumentNullException(nameof(telemetry));
        }

    }
}

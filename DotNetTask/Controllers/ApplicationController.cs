using Console_Api.Interfaces;
using Console_Api.Models;
using Console_Api.Models.Request_Response.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;

        }

        [HttpPut]
        [Route(nameof(UpdateApplication))]
        public async Task<IActionResult> UpdateApplication([FromForm] ApplicationUpdate.Application request)
        {
            var response = await _applicationService.Update(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

        [HttpGet]
        [Route(nameof(GetApplication))]
        public async Task<IActionResult> GetApplication(string ProgramId)
        {
            var response = await _applicationService.GetApplicationTemplate(ProgramId);
            return Ok(response);
        }

    }
}

using Console_Api.Interfaces;
using Console_Api.Models;
using Console_Api.Models.Request_Response.Program;
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
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;
        public ProgramController(IProgramService programService)
        {
            _programService = programService;

        }

        [HttpPost]
        [Route(nameof(CreateProgram))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(object))]
        public async Task<IActionResult> CreateProgram([FromBody]ProgramCreate request)
        {
            var response = await _programService.Create(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut]
        [Route(nameof(UpdateProgram))]
        public async Task<IActionResult> UpdateProgram([FromBody] ProgramUpdate request)
        {
            var response = await _programService.Update(request);
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
        [Route(nameof(GetAllPrograms))]
        public async Task<IActionResult> GetAllPrograms()
        {
            var response = await _programService.GetPrograms();
            return Ok(response);
        }

    }
}

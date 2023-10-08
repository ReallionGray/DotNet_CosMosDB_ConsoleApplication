using Console_Api.Interfaces;
using Console_Api.Models;
using Console_Api.Models.Request_Response.WorkFlow;
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
    public class WorkFlowController : ControllerBase
    {
        private readonly IWorkFlowService _workFlowService;
        public WorkFlowController(IWorkFlowService workFlowService)
        {
            _workFlowService = workFlowService;

        }

        [HttpPut]
        [Route(nameof(UpdateWorkFlow))]
        public async Task<IActionResult> UpdateWorkFlow([FromBody] WorkFlowUpdate.WorkFlow request)
        {
            var response = await _workFlowService.Update(request);
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
        [Route(nameof(GetWorkFlow))]
        public async Task<IActionResult> GetWorkFlow(string ProgramId)
        {
            var response = await _workFlowService.GetWorkFlow(ProgramId);
            return Ok(response);
        }

    }
}

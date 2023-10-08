using Console_Api.Interfaces;
using Console_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreviewController : ControllerBase
    {
        private readonly IPreviewService _previewService;
        public PreviewController(IPreviewService previewService)
        {
            _previewService = previewService;

        }

        [HttpGet]
        [Route(nameof(GetPreview))]
        public async Task<IActionResult> GetPreview(string ProgramId)
        {
            var response = await _previewService.GetPreview(ProgramId);
            return Ok(response);
        }

    }
}

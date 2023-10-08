using Console_Api.Models.Request_Response.Application;
using Console_Api.Models.Request_Response.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Interfaces
{
    public interface IApplicationService
    {
        Task<ApplicationResponse.Application> GetApplicationTemplate(string ProgramId);
        Task<ProgramCreateResponse> Update(ApplicationUpdate.Application request);
    }
}

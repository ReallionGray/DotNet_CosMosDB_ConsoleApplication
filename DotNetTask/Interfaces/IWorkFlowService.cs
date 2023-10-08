using Console_Api.Models.Request_Response.Application;
using Console_Api.Models.Request_Response.Program;
using Console_Api.Models.Request_Response.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Interfaces
{
    public interface IWorkFlowService
    {
        Task<WorkFlowResponse.WorkFlow> GetWorkFlow(string ProgramId);
        Task<ProgramCreateResponse> Update(WorkFlowUpdate.WorkFlow request);
    }
}

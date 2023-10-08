using Console_Api.Models.Request_Response.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Interfaces
{
    public interface IProgramService
    {
        Task<ProgramCreateResponse> Create(ProgramCreate reques);
        Task<List<ProgramResponse>> GetPrograms();
        Task<ProgramCreateResponse> Update(ProgramUpdate request);
    }
}

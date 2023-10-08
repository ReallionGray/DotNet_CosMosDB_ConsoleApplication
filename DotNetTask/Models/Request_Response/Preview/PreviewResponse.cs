using Console_Api.Models.Request_Response.Application;
using Console_Api.Models.Request_Response.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Models.Request_Response.Preview
{
    public class PreviewResponse
    {
        public ProgramResponse program { get; set; }
        public ApplicationResponse.Application application { get; set; }    
    }
}

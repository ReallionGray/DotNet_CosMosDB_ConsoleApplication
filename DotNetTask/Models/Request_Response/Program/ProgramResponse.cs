using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Models.Request_Response.Program
{
    public class ProgramResponse
    {
        public string Id { get; set; }
        public string ProgramTitle { get; set; }
        public string ProgramSummary { get; set; }
        public string ProgramDescription { get; set; }
        public string[] RequiredSkills { get; set; }
        public string ProgramBenefit { get; set; }
        public string ApplicationCriteria { get; set; }
        public string ProgramType { get; set; }
        public DateTime ProgramStartDate { get; set; }
        public DateTime ApplicationOpen { get; set; }
        public DateTime ApplicationClose { get; set; }
        public int ProgramDuration { get; set; }
        public string ProgramLocation { get; set; }
        public string MinimumQalification { get; set; }
        public int MaxNumOfApplications { get; set; }
    }
    public class ProgramCreateResponse
    {
        public string Message { get; set; } 
        public HttpStatusCode StatusCode { get; set; }    
    }
}

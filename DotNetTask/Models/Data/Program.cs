using Console_Api.Models.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Models
{
    public class Program
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("programTitle")]
        public string ProgramTitle { get; set; }

        [JsonProperty("programSummary")]
        public string ProgramSummary { get; set; }

        [JsonProperty("programDescription")]
        public string ProgramDescription { get; set; }

        [JsonProperty("requiredSkills")]
        public string[] RequiredSkills { get; set; }

        [JsonProperty("programBenefit")]
        public string ProgramBenefit { get; set; }

        [JsonProperty("applicationCriteria")]
        public string ApplicationCriteria { get; set; }

        [JsonProperty("programType")]
        public string ProgramType { get; set; }

        [JsonProperty("programStartDate")]
        public DateTime ProgramStartDate { get; set; }

        [JsonProperty("applicationOpen")]
        public DateTime ApplicationOpen { get; set; }

        [JsonProperty("applicationClose")]
        public DateTime ApplicationClose { get; set; }

        [JsonProperty("programDuration")]
        public int ProgramDuration { get; set; }

        [JsonProperty("programLocation")]
        public string ProgramLocation { get; set; }

        [JsonProperty("minimumQalification")]
        public string MinimumQalification { get; set; }

        [JsonProperty("maxNumOfApplications")]
        public int MaxNumOfApplications { get; set; }

        [JsonProperty("applicationTemplate")]
        public Application ApplicationTemplate { get; set; }

        [JsonProperty("workFlow")]
        public WorkFlow WorkFlow { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Models.Request_Response.WorkFlow
{
    public class WorkFlowUpdate
    {
        public class WorkFlow
        {
            [Required]
            public string ProgramId { get; set; }    
            public Stage[] Stages { get; set; }
        }

        public class Stage
        {
            public string StageName { get; set; }
            public string StageType { get; set; }
            public VideoInterview VideoInterview { get; set; }
            public bool IsPlacement { get; set; }
            public bool ShowStage { get; set; }
        }

        public class VideoInterview
        {
            public string InterviewQuestion { get; set; }
            public string InterviewInfo { get; set; }
            public int MaxVideoDuration { get; set; }
            public int VideoSubmissionDeadlineInDays { get; set; }
        }
    }
}

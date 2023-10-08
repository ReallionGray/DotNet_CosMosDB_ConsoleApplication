using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Models.Data
{
    public class WorkFlow
    {
        [JsonProperty("stages")]
        public Stage[] Stages { get; set; }
    }

    public class Stage
    {
        [JsonProperty("stageName")]
        public string StageName { get; set; }

        [JsonProperty("stageType")]
        public string StageType { get; set; }

        [JsonProperty("videoInterview")]
        public VideoInterview VideoInterview { get; set; }

        [JsonProperty("isPlacement")]
        public bool IsPlacement { get; set; }

        [JsonProperty("showStage")]
        public bool ShowStage { get; set; }
    }

    public class VideoInterview
    {
        [JsonProperty("interviewQuestion")]
        public string InterviewQuestion { get; set; }

        [JsonProperty("interviewInfo")]
        public string InterviewInfo { get; set; }

        [JsonProperty("coverImage")]
        public int MaxVideoDuration { get; set; }

        [JsonProperty("videoSubmissionDeadlineInDays")]
        public int VideoSubmissionDeadlineInDays { get; set; }
    }
}

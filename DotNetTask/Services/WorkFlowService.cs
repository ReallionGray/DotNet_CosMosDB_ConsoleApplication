using Console_Api.Interfaces;
using Console_Api.Models.Data;
using Console_Api.Models.Request_Response.Application;
using Console_Api.Models.Request_Response.Program;
using Console_Api.Models.Request_Response.WorkFlow;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Services
{
    public class WorkFlowService: IWorkFlowService
    {
        private static CosMosDBContext _context = new CosMosDBContext();
        private readonly Task<Container> container = null;
        public WorkFlowService(CosMosDBContext context)
        {
            _context = context;
            container = _context.CreateContextAsync(nameof(Models.Program));
        }

        #region Updating Document 
        public async Task<ProgramCreateResponse> Update(WorkFlowUpdate.WorkFlow request)
        {
            try
            {
                //Check that Id Exists
                string query = $"SELECT * FROM c WHERE c.id = '{request.ProgramId}'";
                QueryDefinition queryDefinition = new QueryDefinition(query);
                FeedIterator<Models.Program> queryResultSetIterator = this.container.Result.GetItemQueryIterator<Models.Program>(queryDefinition);

                var response = await queryResultSetIterator.ReadNextAsync();
                var responseData = response.FirstOrDefault();
                if (responseData == null)
                {
                    return new ProgramCreateResponse() { Message = "Invalid Id", StatusCode = System.Net.HttpStatusCode.BadRequest };
                }

#pragma warning disable CS8601 // Possible null reference assignment.
                var data = new Models.Program
                {
                    Id = responseData.Id,
                    ProgramTitle = responseData.ProgramTitle,
                    ProgramDescription = responseData.ProgramDescription,
                    ProgramSummary = responseData.ProgramSummary,
                    ProgramBenefit = responseData.ProgramBenefit,
                    ProgramDuration = responseData.ProgramDuration,
                    ProgramLocation = responseData.ProgramLocation,
                    ProgramStartDate = responseData.ProgramStartDate,
                    ProgramType = responseData.ProgramType,
                    RequiredSkills = responseData.RequiredSkills,
                    MinimumQalification = responseData.MinimumQalification,
                    ApplicationCriteria = responseData.ApplicationCriteria,
                    ApplicationClose = responseData.ApplicationClose,
                    ApplicationOpen = responseData.ApplicationOpen,
                    MaxNumOfApplications = responseData.MaxNumOfApplications,

                    ApplicationTemplate = responseData.ApplicationTemplate,

                    WorkFlow = request!= null? new WorkFlow()
                    {
                        Stages = request.Stages!= null? request.Stages.Select(x => new Stage()
                        {
                            StageName = x.StageName,
                            StageType = x.StageType,
                            IsPlacement = x.IsPlacement,
                            ShowStage = x.ShowStage,
                            VideoInterview = x.VideoInterview!=null? new VideoInterview()
                            {
                                InterviewQuestion = x.VideoInterview.InterviewQuestion,
                                VideoSubmissionDeadlineInDays = x.VideoInterview.VideoSubmissionDeadlineInDays,
                                InterviewInfo = x.VideoInterview.InterviewInfo,
                                MaxVideoDuration = x.VideoInterview.MaxVideoDuration
                            }: null
                        }).ToArray(): null,


                    }:null
                };
#pragma warning restore CS8601 // Possible null reference assignment.

                var result = await this.container.Result.ReplaceItemAsync(data, data.Id);

                return new ProgramCreateResponse() { Message = "Updated Successfuly", StatusCode = result.StatusCode };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        #endregion


        public async Task<WorkFlowResponse.WorkFlow> GetWorkFlow(string ProgramId)
        {

            string query = $"SELECT * FROM c WHERE c.id = '{ProgramId}'";
            QueryDefinition queryDefinition = new QueryDefinition(query);
            FeedIterator<Models.Program> queryResultSetIterator = this.container.Result.GetItemQueryIterator<Models.Program>(queryDefinition);

            var data = await queryResultSetIterator.ReadNextAsync();
            var a = data.FirstOrDefault();

#pragma warning disable CS8601 // Possible null reference assignment.
            var result = a != null ? (a.WorkFlow!= null ? new WorkFlowResponse.WorkFlow
                          {
                              Stages = a.WorkFlow.Stages!= null? a.WorkFlow.Stages.Select(x => new WorkFlowResponse.Stage()
                              {
                                  StageName = x.StageName,
                                  StageType = x.StageType,
                                  IsPlacement = x.IsPlacement,
                                  ShowStage = x.ShowStage,
                                  VideoInterview = x.VideoInterview!=null? new WorkFlowResponse.VideoInterview()
                                  {
                                      InterviewQuestion = x.VideoInterview.InterviewQuestion,
                                      VideoSubmissionDeadlineInDays = x.VideoInterview.VideoSubmissionDeadlineInDays,
                                      InterviewInfo = x.VideoInterview.InterviewInfo,
                                      MaxVideoDuration = x.VideoInterview.MaxVideoDuration
                                  }: null
                              }).ToArray(): null
                              
                          } : null) : null;
#pragma warning restore CS8601 // Possible null reference assignment.

            return result;

        }

        
    }
}

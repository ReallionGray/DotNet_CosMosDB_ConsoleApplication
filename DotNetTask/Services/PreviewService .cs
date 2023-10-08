using Console_Api.Interfaces;
using Console_Api.Models.Data;
using Console_Api.Models.Request_Response.Application;
using Console_Api.Models.Request_Response.Preview;
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
    public class PreviewService: IPreviewService
    {
        private static CosMosDBContext _context = new CosMosDBContext();
        private readonly Task<Container> container = null;
        private readonly IApplicationService _applicationService;
        private readonly IWorkFlowService _workFlowService;
        public PreviewService(CosMosDBContext context, IApplicationService applicationService, IWorkFlowService workFlowService)
        {
            _context = context;
            container = _context.CreateContextAsync(nameof(Models.Program));
            _applicationService = applicationService;
            _workFlowService = workFlowService;
        }

        public async Task<PreviewResponse> GetPreview(string ProgramId)
        {

            string query = $"SELECT * FROM c WHERE c.id = '{ProgramId}'";
            QueryDefinition queryDefinition = new QueryDefinition(query);
            FeedIterator<Models.Program> queryResultSetIterator = this.container.Result.GetItemQueryIterator<Models.Program>(queryDefinition);

            var data = await queryResultSetIterator.ReadNextAsync();

            var responseData = data.FirstOrDefault();

            ApplicationResponse.Application application = await _applicationService.GetApplicationTemplate(ProgramId);
            WorkFlowResponse.WorkFlow workFlow = await _workFlowService.GetWorkFlow(ProgramId);

#pragma warning disable CS8601 // Possible null reference assignment.
            var result = new PreviewResponse()
            {
                program = responseData!= null? new ProgramResponse()
                {
                    Id = responseData.Id,
                    ProgramTitle = responseData.ProgramTitle,
                    ProgramDescription = responseData.ProgramDescription,
                    ProgramSummary = responseData.ProgramSummary,
                    ProgramBenefit = responseData.ProgramBenefit,
                    RequiredSkills = responseData.RequiredSkills,
                    ProgramType = responseData.ProgramType,
                    ProgramStartDate = responseData.ProgramStartDate,
                    ApplicationOpen = responseData.ApplicationOpen,
                    ApplicationClose = responseData.ApplicationClose,
                    ProgramDuration = responseData.ProgramDuration,
                    ApplicationCriteria = responseData.ApplicationCriteria,
                    MaxNumOfApplications = responseData.MaxNumOfApplications,
                    MinimumQalification = responseData.MinimumQalification,
                    ProgramLocation = responseData.ProgramLocation
                }: null,
                application = responseData!= null? (responseData.ApplicationTemplate!= null? application: null): null,
                              

             };
#pragma warning restore CS8601 // Possible null reference assignment.

            return result;

        }

        
    }
}

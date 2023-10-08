using Console_Api.Interfaces;
using Console_Api.Models.Data;
using Console_Api.Models.Request_Response.Program;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Services
{
    public class ProgramService: IProgramService
    {
        private static CosMosDBContext _context = new CosMosDBContext();
        private readonly Task<Container> container = null;
        public ProgramService(CosMosDBContext context)
        {
            _context = context;
            container = _context.CreateContextAsync(nameof(Models.Program));
        }


        #region Creating Docuemnt 
        public async Task<ProgramCreateResponse> Create(ProgramCreate request)
        {
            try
            {
                var data = new Models.Program
                {
                    Id = Guid.NewGuid().ToString(),
                    ProgramTitle = request.ProgramTitle,
                    ProgramDescription = request.ProgramDescription,
                    ProgramSummary = request.ProgramSummary,
                    ProgramBenefit = request.ProgramBenefit,
                    ProgramDuration = request.ProgramDuration,
                    ProgramLocation = request.ProgramLocation,
                    ProgramStartDate = request.ProgramStartDate,
                    ProgramType = request.ProgramType,
                    RequiredSkills = request.RequiredSkills,
                    MinimumQalification = request.MinimumQalification,
                    ApplicationCriteria = request.ApplicationCriteria,
                    ApplicationClose = request.ApplicationClose,
                    ApplicationOpen = request.ApplicationOpen,
                    MaxNumOfApplications = request.MaxNumOfApplications,

                    ApplicationTemplate = null,
                    WorkFlow = null,
                    
                };

                var result = await this.container.Result.CreateItemAsync(data);

                return new ProgramCreateResponse() { Message = "Created Successfuly", StatusCode = result.StatusCode };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        #endregion

        #region Updating Document 
        public async Task<ProgramCreateResponse> Update(ProgramUpdate request)
        {
            try
            {
                //Check that Id Exists
                string query = $"SELECT * FROM c WHERE c.id = '{request.Id}'";
                QueryDefinition queryDefinition = new QueryDefinition(query);
                FeedIterator<Models.Program> queryResultSetIterator = this.container.Result.GetItemQueryIterator<Models.Program>(queryDefinition);

                var response = await queryResultSetIterator.ReadNextAsync();
                var responseData = response.FirstOrDefault();
                if (responseData == null)
                {
                    return new ProgramCreateResponse() { Message = "Invalid Id", StatusCode = System.Net.HttpStatusCode.BadRequest };
                }

                var data = new Models.Program
                {
                    Id = request.Id,
                    ProgramTitle = request.ProgramTitle,
                    ProgramDescription = request.ProgramDescription,
                    ProgramSummary = request.ProgramSummary,
                    ProgramBenefit = request.ProgramBenefit,
                    ProgramDuration = request.ProgramDuration,
                    ProgramLocation = request.ProgramLocation,
                    ProgramStartDate = request.ProgramStartDate,
                    ProgramType = request.ProgramType,
                    RequiredSkills = request.RequiredSkills,
                    MinimumQalification = request.MinimumQalification,
                    ApplicationCriteria = request.ApplicationCriteria,
                    ApplicationClose = request.ApplicationClose,
                    ApplicationOpen = request.ApplicationOpen,
                    MaxNumOfApplications = request.MaxNumOfApplications,

                    ApplicationTemplate = responseData.ApplicationTemplate!= null? responseData.ApplicationTemplate: null,
                    WorkFlow = responseData.WorkFlow!=null? responseData.WorkFlow: null
                };

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


        public async Task<List<ProgramResponse>> GetPrograms()
        {

            string query = "SELECT * FROM c";
            QueryDefinition queryDefinition = new QueryDefinition(query);
            FeedIterator<Models.Program> queryResultSetIterator = this.container.Result.GetItemQueryIterator<Models.Program>(queryDefinition);

            var apps = await queryResultSetIterator.ReadNextAsync();

            var result = (from a in apps
                          select new ProgramResponse
                          {
                              Id = a.Id,
                              ProgramTitle = a.ProgramTitle,
                              ProgramDescription = a.ProgramDescription,
                              ProgramSummary = a.ProgramSummary,
                              ProgramBenefit = a.ProgramBenefit,
                              RequiredSkills = a.RequiredSkills,
                              ProgramType = a.ProgramType,
                              ProgramStartDate = a.ProgramStartDate,
                              ApplicationOpen = a.ApplicationOpen,
                              ApplicationClose = a.ApplicationClose,
                              ProgramDuration = a.ProgramDuration,
                              ApplicationCriteria = a.ApplicationCriteria,
                              MaxNumOfApplications = a.MaxNumOfApplications,
                              MinimumQalification = a.MinimumQalification,
                              ProgramLocation = a.ProgramLocation

                          }).ToList();

            return result;

        }
    }
}

using Console_Api.Interfaces;
using Console_Api.Models.Data;
using Console_Api.Models.Request_Response.Application;
using Console_Api.Models.Request_Response.Program;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Services
{
    public class ApplicationService: IApplicationService
    {
        private static CosMosDBContext _context = new CosMosDBContext();
        private readonly Task<Container> container = null;
        public ApplicationService(CosMosDBContext context)
        {
            _context = context;
            container = _context.CreateContextAsync(nameof(Models.Program));
        }

        #region Updating Document 

        private async Task<string> UploadImage(IFormFile image, string Extension)
        {
            string response = "";
            string webRootPath = Directory.GetCurrentDirectory();

            string fileName = string.Format("CoverImage_{0}{1}", DateTime.Now.ToFileTime(), Extension);
            var filePath = @$"{Path.Combine(webRootPath, "Uploads", "CoverImages", fileName)}";

            if (System.IO.File.Exists(filePath)) 
            { 
                System.IO.File.Delete(filePath); 
            }

            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            using (Stream stream = new FileStream(filePath, FileMode.Append)) { image.CopyTo(stream); }

            response = filePath;
            return response;
        }
        public async Task<ProgramCreateResponse> Update(ApplicationUpdate.Application request)
        {
            try
            {
                //Check that Id Exists
                string query = $"SELECT * FROM c WHERE c.id = '{request.ProgramId}'";
                QueryDefinition queryDefinition = new QueryDefinition(query);
                FeedIterator<Models.Program> queryResultSetIterator = this.container.Result.GetItemQueryIterator<Models.Program>(queryDefinition);

                var response = await queryResultSetIterator.ReadNextAsync();
                var responseData = response.FirstOrDefault();
                if(responseData == null)
                {
                    return new ProgramCreateResponse() { Message = "Invalid Id", StatusCode = System.Net.HttpStatusCode.BadRequest };
                }

                string coverImage = "";
                if(request.CoverImage != null)
                {
                    string extension = Path.GetExtension(request.CoverImage.FileName).ToLower(); ;
                    string[] AllowedFormats = { ".jpg", ".jpeg", ".png" };

                    if (!AllowedFormats.Contains(extension))
                    {
                        return new ProgramCreateResponse() { Message = "Invalid Image Format", StatusCode = System.Net.HttpStatusCode.BadRequest };
                    }
                    coverImage = await UploadImage(request.CoverImage, extension);
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

                    ApplicationTemplate = new Application
                    {
                        CoverImage = coverImage,
                        PersonalInformation = request.PersonalInformation!=null? new PersonInfo
                        {
                            FirstName = request.PersonalInformation.FirstName,
                            LastName = request.PersonalInformation.LastName,
                            DateOfBirth = request.PersonalInformation.DateOfBirth,
                            Email = request.PersonalInformation.Email,
                            Gender = request.PersonalInformation.Gender,
                            PhoneNumber = request.PersonalInformation.PhoneNumber,
                            IdNumber = request.PersonalInformation.IdNumber,
                            Nationality = request.PersonalInformation.Nationality,
                            CurrentResidence = request.PersonalInformation.CurrentResidence,
                            AdditionalQuestions = request.PersonalInformation.AdditionalQuestions.Select(item => new Questions() 
                            { 
                                Question = item.Question,
                                QuestionType = item.QuestionType,
                                Choices = item.Choices,
                                EnableOptions = item.EnableOptions,
                                MaxNumOfChoices =  item.MaxNumOfChoices
                            }).ToArray(),
                        }: null, 
                        Profile = request.Profile != null? new Profile 
                        {
                            Education = request.Profile.Education!=null? new Education
                            {
                                IsMandatory = request.Profile.Education.IsMandatory,
                                Show = request.Profile.Education.Show,
                                SchoolsAttended = request.Profile.Education.SchoolsAttended!=null? request.Profile.Education.SchoolsAttended.Select(x => new SchoolsAttended()
                                {
                                    CourseName = x.CourseName,
                                    SchoolName = x.SchoolName,
                                    Degree = x.Degree,
                                    LocationOfStudy = x.LocationOfStudy,
                                    StartDate = x.StartDate,
                                    EndDate = x.EndDate,
                                    IsCurrent = x.IsCurrent

                                }).ToArray(): null,
                            }:null,
                            Experience = request.Profile.Experience!=null? new Experience
                            {
                                IsMandatory = request.Profile.Experience.IsMandatory,
                                Show = request.Profile.Experience.Show,
                                CompaniesEmployed = request.Profile.Experience.CompaniesEmployed!= null? request.Profile.Experience.CompaniesEmployed.Select(x => new CompaniesEmployed()
                                {
                                    CompanyName = x.CompanyName,
                                    Title = x.Title,
                                    LocationOfWork = x.LocationOfWork,
                                    StartDate = x.StartDate,
                                    EndDate = x.EndDate,
                                    IsCurrent = x.IsCurrent

                                }).ToArray(): null,
                            }: null,
                            Resume = request.Profile.Resume!= null? new Resume 
                            {
                                IsMandatory = request.Profile.Resume.IsMandatory,  
                                Show = request.Profile.Resume.Show,
                            }: null,
                            AdditionalQuestions = request.Profile.AdditionalQuestions!=null? request.Profile.AdditionalQuestions.Select(x => new Questions()
                            {
                               Question = x.Question,
                               QuestionType = x.QuestionType,
                               Choices = x.Choices,
                               EnableOptions = x.EnableOptions,
                               MaxNumOfChoices = x.MaxNumOfChoices
                               
                            }).ToArray():null,
                        }: null,
                        AdditionalQuestions = request.AdditionalQuestions!= null? request.AdditionalQuestions.Select(x => new Questions()
                        {
                            Question = x.Question,
                            QuestionType = x.QuestionType,
                            Choices = x.Choices,
                            EnableOptions = x.EnableOptions,
                            MaxNumOfChoices = x.MaxNumOfChoices

                        }).ToArray(): null,
                    },
                    WorkFlow = responseData.WorkFlow!= null? responseData.WorkFlow: null
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


        public async Task<ApplicationResponse.Application> GetApplicationTemplate(string ProgramId)
        {

            string query = $"SELECT * FROM c WHERE c.id = '{ProgramId}'";
            QueryDefinition queryDefinition = new QueryDefinition(query);
            FeedIterator<Models.Program> queryResultSetIterator = this.container.Result.GetItemQueryIterator<Models.Program>(queryDefinition);

            var data = await queryResultSetIterator.ReadNextAsync();
            var a = data.FirstOrDefault();

            var result =  a!= null? (a.ApplicationTemplate!= null? new ApplicationResponse.Application
                {
                    CoverImage = a.ApplicationTemplate.CoverImage,
                    PersonalInformation = new ApplicationResponse.PersonInfo
                    {
                        FirstName = a.ApplicationTemplate.PersonalInformation.FirstName,
                        LastName = a.ApplicationTemplate.PersonalInformation.LastName,
                        DateOfBirth = a.ApplicationTemplate.PersonalInformation.DateOfBirth,
                        Email = a.ApplicationTemplate.PersonalInformation.Email,
                        Gender = a.ApplicationTemplate.PersonalInformation.Gender,
                        PhoneNumber = a.ApplicationTemplate.PersonalInformation.PhoneNumber,
                        IdNumber = a.ApplicationTemplate.PersonalInformation.IdNumber,
                        Nationality = a.ApplicationTemplate.PersonalInformation.Nationality,
                        CurrentResidence = a.ApplicationTemplate.PersonalInformation.CurrentResidence,
                        AdditionalQuestions = a.ApplicationTemplate.PersonalInformation.AdditionalQuestions.Select(item => new ApplicationResponse.Questions()
                        {
                            Question = item.Question,
                            QuestionType = item.QuestionType,
                            Choices = item.Choices,
                            EnableOptions = item.EnableOptions,
                            MaxNumOfChoices = item.MaxNumOfChoices
                        }).ToArray(),
                    },
                    Profile = new ApplicationResponse.Profile
                    {
                        Education = new ApplicationResponse.Education
                        {
                            IsMandatory = a.ApplicationTemplate.Profile.Education.IsMandatory,
                            Show = a.ApplicationTemplate.Profile.Education.Show,
                            SchoolsAttended = a.ApplicationTemplate.Profile.Education.SchoolsAttended.Select(x => new ApplicationResponse.SchoolsAttended()
                            {
                                CourseName = x.CourseName,
                                SchoolName = x.SchoolName,
                                Degree = x.Degree,
                                LocationOfStudy = x.LocationOfStudy,
                                StartDate = x.StartDate,
                                EndDate = x.EndDate,
                                IsCurrent = x.IsCurrent

                            }).ToArray(),
                        },
                        Experience = new ApplicationResponse.Experience
                        {
                            IsMandatory = a.ApplicationTemplate.Profile.Experience.IsMandatory,
                            Show = a.ApplicationTemplate.Profile.Experience.Show,
                            CompaniesEmployed = a.ApplicationTemplate.Profile.Experience.CompaniesEmployed.Select(x => new ApplicationResponse.CompaniesEmployed()
                            {
                                CompanyName = x.CompanyName,
                                Title = x.Title,
                                LocationOfWork = x.LocationOfWork,
                                StartDate = x.StartDate,
                                EndDate = x.EndDate,
                                IsCurrent = x.IsCurrent

                            }).ToArray(),
                        },
                        Resume = new ApplicationResponse.Resume
                        {
                            IsMandatory = a.ApplicationTemplate.Profile.Resume.IsMandatory,
                            Show = a.ApplicationTemplate.Profile.Resume.Show,
                        },
                        AdditionalQuestions = a.ApplicationTemplate.Profile.AdditionalQuestions.Select(x => new ApplicationResponse.Questions()
                        {
                            Question = x.Question,
                            QuestionType = x.QuestionType,
                            Choices = x.Choices,
                            EnableOptions = x.EnableOptions,
                            MaxNumOfChoices = x.MaxNumOfChoices

                        }).ToArray(),
                    },
                    AdditionalQuestions = a.ApplicationTemplate.AdditionalQuestions.Select(x => new ApplicationResponse.Questions()
                    {
                        Question = x.Question,
                        QuestionType = x.QuestionType,
                        Choices = x.Choices,
                        EnableOptions = x.EnableOptions,
                        MaxNumOfChoices = x.MaxNumOfChoices

                    }).ToArray(),
            }: null): null;

            return result;

        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Models.Data
{
    public class Application
    {

        [JsonProperty("coverImage")]
        public string CoverImage { get; set; }

        [JsonProperty("personalInformation")]
        public PersonInfo PersonalInformation { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }

        [JsonProperty("additionalQuestions")]
        public Questions[] AdditionalQuestions { get; set; }

    }

    public class PersonInfo
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("currentResidence")]
        public string CurrentResidence { get; set; }

        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("additionalQuestions")]
        public Questions[] AdditionalQuestions { get; set; }

        
    }

    public class Profile
    {
        [JsonProperty("education")]
        public Education Education { get; set; }

        [JsonProperty("experience")]
        public Experience Experience { get; set; }

        [JsonProperty("resume")]
        public Resume Resume { get; set; }

        [JsonProperty("additionalQuestions")]
        public Questions[] AdditionalQuestions { get; set; }
    }

    public class Education
    {
        [JsonProperty("schoolsAttended")]
        public SchoolsAttended[] SchoolsAttended { get; set; }

        [JsonProperty("isMandatory")]
        public bool IsMandatory { get; set; }

        [JsonProperty("show")]
        public bool Show { get; set; }
    }

    public class SchoolsAttended
    {
        [JsonProperty("schoolName")]
        public string SchoolName { get; set; }

        [JsonProperty("degree")]
        public string Degree { get; set; }

        [JsonProperty("courseName")]
        public string CourseName { get; set; }

        [JsonProperty("locationOfStudy")]
        public string LocationOfStudy { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("isCurrent")]
        public bool IsCurrent { get; set; }
    }


    public class Experience
    {
        [JsonProperty("companiesEmployed")]
        public CompaniesEmployed[] CompaniesEmployed { get; set; }

        [JsonProperty("isMandatory")]
        public bool IsMandatory { get; set; }

        [JsonProperty("show")]
        public bool Show { get; set; }
    }

    public class CompaniesEmployed
    {
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("locationOfWork")]
        public string LocationOfWork { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("isCurrent")]
        public bool IsCurrent { get; set; }

    }




    public class Resume
    {
        [JsonProperty("isMandatory")]
        public bool IsMandatory { get; set; }

        [JsonProperty("show")]
        public bool Show { get; set; }
    }

    public class Questions
    {
        [JsonProperty("questionType")]
        public string QuestionType { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("choices")]
        public string[] Choices { get; set; }

        [JsonProperty("enableOptions")]
        public bool EnableOptions { get; set; }

        [JsonProperty("maxNumOfChoices")]
        public int MaxNumOfChoices { get; set; }
    }
}

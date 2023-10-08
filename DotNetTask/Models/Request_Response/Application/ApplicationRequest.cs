using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Api.Models.Request_Response.Application
{
    public class ApplicationUpdate
    {
        public class Application
        {
            [Required]
            public string ProgramId { get; set; }
            public IFormFile CoverImage { get; set; }
            public PersonInfo? PersonalInformation { get; set; }
            public Profile? Profile { get; set; }
            public Questions[]? AdditionalQuestions { get; set; }

        }

        public class PersonInfo
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Nationality { get; set; }
            public string CurrentResidence { get; set; }
            public string IdNumber { get; set; }
            public string DateOfBirth { get; set; }
            public string Gender { get; set; }
            public Questions[] AdditionalQuestions { get; set; }


        }

        public class Profile
        {
            public Education Education { get; set; }
            public Experience Experience { get; set; }
            public Resume Resume { get; set; }
            public Questions[] AdditionalQuestions { get; set; }
        }

        public class Education
        {
            public SchoolsAttended[] SchoolsAttended { get; set; }
            public bool IsMandatory { get; set; }
            public bool Show { get; set; }
        }

        public class SchoolsAttended
        {
            public string SchoolName { get; set; }
            public string Degree { get; set; }
            public string CourseName { get; set; }
            public string LocationOfStudy { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public bool IsCurrent { get; set; }
        }


        public class Experience
        {
            public CompaniesEmployed[] CompaniesEmployed { get; set; }
            public bool IsMandatory { get; set; }
            public bool Show { get; set; }
        }

        public class CompaniesEmployed
        {
            public string CompanyName { get; set; }
            public string Title { get; set; }
            public string LocationOfWork { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public bool IsCurrent { get; set; }

        }




        public class Resume
        {
            public bool IsMandatory { get; set; }
            public bool Show { get; set; }
        }

        public class Questions
        {
            public string QuestionType { get; set; }
            public string Question { get; set; }
            public string[] Choices { get; set; }
            public bool EnableOptions { get; set; }
            public int MaxNumOfChoices { get; set; }
        }
    }
}

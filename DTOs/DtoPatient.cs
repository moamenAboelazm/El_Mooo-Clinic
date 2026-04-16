using El_Mooo_Clinic.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace El_Mooo_Clinic.DTOs
{
    public class DtoPatient
    {
        public class CreateDTO
        {
            [Required(ErrorMessage = "Name Field is Required")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Birth Date Field is Required")]
            public DateTime BirthDate { get; set; }

            [Required(ErrorMessage = "Gender Field is Required")]
            public ClsEnums.Gender Gender { get; set; }

            [Required(ErrorMessage = "Phone Number Field is Required")]
            [MinLength(11, ErrorMessage = "Phone Number should consist of 11 digit")]
            [MaxLength(11, ErrorMessage = "Phone Number should consist of 11 digit")]
            public string PhoneNumber { get; set; }

            public IFormFile? ImageFile { get; set; }
            public string? ProfilePicture { get; set; }
        }

        public class ReadDTO
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime BirthDate { get; set; }
            public ClsEnums.Gender Gender { get; set; }
            public int Age { get; set; }
            public string GenderName { get; set; }
            public string PhoneNumber { get; set; }
            public string ProfilePicture { get; set; }
            public int NumberOfVisits { get; set; } 
        }

        public class UpdateDTO
        {
            public int ID { get; set; }

            [Required(ErrorMessage = "Name Field is Required")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Birth Date Field is Required")]
            public DateTime BirthDate { get; set; }

            [Required(ErrorMessage = "Gender Field is Required")]
            public ClsEnums.Gender Gender { get; set; }

            [Required(ErrorMessage = "Phone Number Field is Required")]
            [MinLength(11, ErrorMessage = "Phone Number should consist of 11 digit")]
            [MaxLength(11, ErrorMessage = "Phone Number should consist of 11 digit")]
            public string PhoneNumber { get; set; }

            public IFormFile? ImageFile { get; set; }
            public string? ProfilePicture { get; set; }
        }
    }
}
using El_Mooo_Clinic.Models;
using System.ComponentModel.DataAnnotations;

namespace El_Mooo_Clinic.DTOs
{
    public class DtoDoctor
    {
        public class CreateDTO
        {
            [Required(ErrorMessage = "Name Field is Required")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Department ID Field is Required")]
            public int DepartmentID { get; set; }

            [Required(ErrorMessage = "Examination Cost Field is Required")]
            public decimal ExaminationCost { get; set; }

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
            public string Email { get; set; }
            public string DepartmentName { get; set; }
            public int DepartmentID { get; set; }
            public DateTime BirthDate { get; set; }
            public ClsEnums.Gender Gender { get; set; }
            public decimal ExaminationCost { get; set; }
            public int Age { get; set; }
            public string GenderName { get; set; }
            public string PhoneNumber { get; set; }
            public string ProfilePicture { get; set; }
            public List<DtoDoctorSchedule.ReadDTO> Schedules { get; set; } = new();
            public List<DtoAppointment> Appointments { get; set; } = new();

        }

        public class UpdateDTO
        {
            public int ID { get; set; }
            [Required(ErrorMessage = "Name Field is Required")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Department ID Field is Required")]
            public int DepartmentID { get; set; }

            [Required(ErrorMessage = "Birth Date Field is Required")]
            public DateTime BirthDate { get; set; }

            [Required(ErrorMessage = "Gender Field is Required")]
            public ClsEnums.Gender Gender { get; set; }

            [Required(ErrorMessage = "Examination Cost Field is Required")]
            public decimal ExaminationCost { get; set; }

            [Required(ErrorMessage = "Phone Number Field is Required")]
            [MinLength(11, ErrorMessage = "Phone Number should consist of 11 digit")]
            [MaxLength(11, ErrorMessage = "Phone Number should consist of 11 digit")]
            public string PhoneNumber { get; set; }
            public IFormFile? ImageFile { get; set; }
            public string? ProfilePicture { get; set; }
        }
    }
}

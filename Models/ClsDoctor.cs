using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static El_Mooo_Clinic.Models.ClsEnums;

namespace El_Mooo_Clinic.Models
{
    public class ClsDoctor
    {
        [Key]
        public int ID { get; set; }

        //--------------------------------------------------

        [Required(ErrorMessage = "Name Field is Required")]
        public string Name { get; set; }

        //--------------------------------------------------

        [Required(ErrorMessage = "Gender Field is Required")]
        public Gender Gender { get; set; }
        
        //--------------------------------------------------

        [Required(ErrorMessage = "Examination Cost Field is Required")]
        public double ExaminationCost { get; set; }
        
        //--------------------------------------------------

        [Required(ErrorMessage = "Birth Date Field is Required")]
        public DateTime BirthDate { get; set; }

        //--------------------------------------------------
        [NotMapped]
        public Double Age 
        {
            get
            {
                return DateTime.Today.Year - BirthDate.Year;
            } 
        } 

        //--------------------------------------------------

        [Required(ErrorMessage = "Phone Number Field is Required")]
        [MinLength(11, ErrorMessage = "Phone Number should consist of 11 digit")]
        [MaxLength(11, ErrorMessage = "Phone Number should consist of 11 digit")]
        public string PhoneNumber { get; set; }

        //--------------------------------------------------

        public string ProfilePicture { get; set; } 

        //--------------------------------------------------

        [Required(ErrorMessage = "Department ID Field is Required")]
        public int DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public ClsDepartment Department { get; set; }

        //--------------------------------------------------

        public List<ClsDoctorSchedule> Schedules { get; set; } = new List<ClsDoctorSchedule>();
        public List<ClsAppointments> Appointments { get; set; } = new List<ClsAppointments>();


    }
}

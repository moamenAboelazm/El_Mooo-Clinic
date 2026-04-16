using System.ComponentModel.DataAnnotations;
using static El_Mooo_Clinic.Models.ClsEnums;

namespace El_Mooo_Clinic.Models
{
    public class ClsPatient
    {
        [Key]
        public int ID { get; set; }
        
        //--------------------------------------------------

        [Required(ErrorMessage ="Name Field is Required")]
        public string Name { get; set; }

        //--------------------------------------------------

        [Required(ErrorMessage = "Gender Field is Required")]
        public Gender Gender { get; set; }

        //--------------------------------------------------

        [Required(ErrorMessage = "Birth Date Field is Required")]
        public DateTime BirthDate { get; set; }

        //--------------------------------------------------

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - BirthDate.Year;
                if (BirthDate.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        //--------------------------------------------------

        [Required(ErrorMessage = "Phone Number Field is Required")]
        [MinLength(11 , ErrorMessage ="Phone Number should consist of 11 digit")]
        [MaxLength(11 , ErrorMessage ="Phone Number should consist of 11 digit")]
        public string PhoneNumber { get; set; }

        //--------------------------------------------------
        
        public string ProfilePicture { get; set; }

        //--------------------------------------------------

        public int NumberOfVisits { get; set; } = 0;

        //--------------------------------------------------

        public List<ClsAppointments> Appointments { get; set; } = new List<ClsAppointments>();

    }
}

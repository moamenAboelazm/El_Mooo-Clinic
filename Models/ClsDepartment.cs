using System.ComponentModel.DataAnnotations;

namespace El_Mooo_Clinic.Models
{
    public class ClsDepartment
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name Field is Required")]
        public string Name { get; set; }

        List<ClsDoctor> InvolvedDoctors { get; set; } = new List<ClsDoctor>();

    }
}

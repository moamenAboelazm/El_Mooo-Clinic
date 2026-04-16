using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El_Mooo_Clinic.Models
{
    public class ClsMedicalRecords
    {
        [Key]
        public int ID { get; set; }

        //--------------------------------------------------

        [Required]
        public int AppointmentID { get; set; }
        [ForeignKey("AppointmentID")]
        public ClsAppointments Appointment { get; set; }

        public DateTime AppointmentDate { get; set; }

        //--------------------------------------------------
        [MaxLength(100)]
        public string? Diagnosis { get; set; }

        //--------------------------------------------------

        [MaxLength(100)]
        public string? Prescription { get; set; }

    }
}

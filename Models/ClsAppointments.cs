using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static El_Mooo_Clinic.Models.ClsEnums;

namespace El_Mooo_Clinic.Models
{
    public class ClsAppointments
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public ClsPatient Patient { get; set; }

        [Required]
        public int DoctorID { get; set; }
        [ForeignKey("DoctorID")]
        public ClsDoctor Doctor { get; set; }

        public double Cost { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int DurationInMinutes { get; set; }

        public AppointmentStatus Status { get; set; } = ClsEnums.AppointmentStatus.Booked;

        public ClsMedicalRecords MedicalRecord { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace El_Mooo_Clinic.Models
{
    public class ClsDoctorSchedule
    {
        [Key]
        public int ID { get; set; }

        //--------------------------------------------------

        [Required]
        public int DoctorID { get; set; }
        [ForeignKey("DoctorID")]
        public ClsDoctor Doctor { get; set; }

        //--------------------------------------------------

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        //--------------------------------------------------

        [Required]
        public TimeSpan StartTime { get; set; }

        //--------------------------------------------------

        [Required]
        public TimeSpan EndTime { get; set; }

        //--------------------------------------------------

        public bool IsAvailable { get; set; } = true;
    }
}

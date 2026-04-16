using El_Mooo_Clinic.Models;

namespace El_Mooo_Clinic.DTOs
{
    public class DtoAppointment
    {
        public class CreateDTO
        {
            public int PatientID { get; set; }
            public int DoctorID { get; set; }
            public DateTime RequestedDate { get; set; }
            public int DurationInMinutes { get; set; }
        }

        public class ReadDTO
        {
            public int ID { get; set; }
            public string PatientName { get; set; }
            public string DoctorName { get; set; }
            public decimal Cost { get; set; }
            public DateTime AppointmentDate { get; set; }
            public string Status { get; set; }
            public int DurationInMinutes { get; set; }
        }

        public class UpdateDTO
        {
            public int ID { get; set; }
            public DateTime RequestedDate { get; set; }
            public ClsEnums.AppointmentStatus Status { get; set; }
        }
    }
}
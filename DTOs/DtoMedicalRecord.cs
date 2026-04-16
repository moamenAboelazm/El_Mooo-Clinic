using El_Mooo_Clinic.Models;

namespace El_Mooo_Clinic.DTOs
{
    public class DtoMedicalRecord
    {
        public class CreateDTO
        {
            public int AppointmentID { get; set; }
            public string Diagnosis { get; set; }
            public string Prescription { get; set; }
            public int Visits { get; set; } = 0;
        }

        public class ReadDTO
        {
            public int ID { get; set; }
            public string PatientName { get; set; }
            public string DoctorName { get; set; }
            public int Visits { get; set; }
            public string Diagnosis { get; set; }
            public string Prescription { get; set; }
            public int AppointmentID { get; set; }
            public DateTime AppointmentDate { get; set; }
        }

        public class UpdateDTO
        {
            public int ID { get; set; }
            public string Diagnosis { get; set; }
            public string Prescription { get; set; }
            public int Visits { get; set; }
        }
    }
}

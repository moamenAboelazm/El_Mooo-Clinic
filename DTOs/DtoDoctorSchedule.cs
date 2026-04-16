namespace El_Mooo_Clinic.DTOs
{
    public class DtoDoctorSchedule
    {
        public class CreateDTO
        {
            public int DoctorID { get; set; }
            public DayOfWeek DayOfWeek { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
        }

        public class ReadDTO
        {
            public int ID { get; set; }
            public int DoctorID { get; set; }
            public string DoctorName { get; set; }
            public string DayName { get; set; } 
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }

        }
    }
}
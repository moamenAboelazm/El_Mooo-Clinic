namespace El_Mooo_Clinic.DTOs
{
    public class DtoDepartment
    {
        public class CreateDTO
        {
            public string Name { get; set; }
        }

        public class ReadDTO
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class UpdateDTO
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
    }
}

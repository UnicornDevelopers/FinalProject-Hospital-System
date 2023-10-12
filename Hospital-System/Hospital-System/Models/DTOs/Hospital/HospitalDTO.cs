namespace Hospital_System.Models.DTOs
{
    public class HospitalDTO
    {

        public int Id { get; set; }
        public string HospitalName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }


        public List<DepartmentDTO> departments { get; set; }
    }
}

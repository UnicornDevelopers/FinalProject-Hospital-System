using Hospital_System.Models.DTOs.Department;

namespace Hospital_System.Models.DTOs.Hospital
{
    public class InHospitalDTO
    {
        public int Id { get; set; }
        public string HospitalName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }

        public List<InDepartmentDTO> departments { get; set; }



    }
}

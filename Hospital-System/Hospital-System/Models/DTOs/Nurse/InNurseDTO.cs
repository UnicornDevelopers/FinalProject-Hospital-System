using static Hospital_System.Models.Nurse;

namespace Hospital_System.Models.DTOs.Nurse
{
    public class InNurseDTO
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public Shift Shift { get; set; }

        public int DepartmentId { get; set; }



    }
}

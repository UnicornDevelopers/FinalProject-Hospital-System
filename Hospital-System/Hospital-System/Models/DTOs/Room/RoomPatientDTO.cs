using Hospital_System.Models.DTOs.Patient;

namespace Hospital_System.Models.DTOs.Room
{
    public class RoomPatientDTO
    {



        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public bool RoomAvailability { get; set; }
        public int NumberOfBeds { get; set; }
        public int DepartmentId { get; set; }

        //Nav
        public List<InPatientDTO>? Patients { get; set; }
    }
}

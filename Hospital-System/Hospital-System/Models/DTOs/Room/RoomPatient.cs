using Hospital_System.Models.DTOs.Department;
namespace Hospital_System.Models.DTOs.Room
{
    public class RoomPatient
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public bool RoomAvailability { get; set; }
        public int NumberOfBeds { get; set; }
        public int DepartmentId { get; set; }
        public OutDepartmentDTO? department { get; set; }
    }
}
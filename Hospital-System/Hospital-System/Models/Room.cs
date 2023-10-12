using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public bool RoomAvailability { get; set; }
        public int NumberOfBeds { get; set; }
        public int DepartmentId { get; set; }
       

        //Nav
        public List<Patient>? Patients { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? department { get; set; }
    }
}

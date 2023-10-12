using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hospital_System.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        public string? ContactNumber { get; set; }
        public string Address { get; set; }
        public int? RoomId { get; set; }

        public string? UserId { get; set; }
       
        //Nav
        [ForeignKey("RoomId")]
        public Room? Rooms { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<MedicalReport> MedicalReports { get; set; }

      

    }
}

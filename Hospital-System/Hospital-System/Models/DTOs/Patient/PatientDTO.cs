using System.ComponentModel.DataAnnotations.Schema;
using Hospital_System.Models.DTOs.AppointmentDTO;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.DTOs.Room;
namespace Hospital_System.Models.DTOs.Patient
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public int? RoomId { get; set; }
        //Nav
        public RoomPatient? Rooms { get; set; }
        public List<OutAppointmentDTO>? Appointments { get; set; }
        public List<OutMedicalReportDTO>? MedicalReports { get; set; }
    }
}
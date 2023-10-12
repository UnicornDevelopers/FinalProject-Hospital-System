using System.ComponentModel.DataAnnotations;

namespace Hospital_System.Models.DTOs.Appointment
{
    public class InAppoinmentDTO
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfAppointment { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }
    }
}

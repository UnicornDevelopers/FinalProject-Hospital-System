using System.ComponentModel.DataAnnotations;

namespace Hospital_System.Models.DTOs.Appointment
{
    public class DoctorAppointmentsDTO
    {

        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfAppointment { get; set; }

        public int PatientId { get; set; }

    }
}

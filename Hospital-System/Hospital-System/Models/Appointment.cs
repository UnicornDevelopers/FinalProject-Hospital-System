using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfAppointment { get; set; }
        


        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        // Nav
        [ForeignKey("DoctorId")]
        public Doctor doctor { get; set; }
        [ForeignKey("PatientId")]
        public Patient patient { get; set; }
    }
}

using Hospital_System.Models.DTOs.Patient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models.DTOs.AppointmentDTO
{
    public class AppointmentDTO
    {

        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfAppointment { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }


        // Nav
        public OutDocDTO? doctor { get; set; }
        public OutPatientDTO? patient { get; set; }


    }
}

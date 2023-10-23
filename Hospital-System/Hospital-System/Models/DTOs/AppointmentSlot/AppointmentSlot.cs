using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Appointment;

namespace Hospital_System.Models.DTOs.AppointmentSlot
{
	public class AppointmentSlotDto
	{

		public int Id { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime DateOfSlot { get; set; }
		public TimeSpan SlotHour { get; set; }
		public List<AppointmentDTO>? Appointments { get; set; }

		public int DoctorId { get; set; }


		[ForeignKey("DoctorId")]
		public DoctorDTO? doctor { get; set; }


	}
}
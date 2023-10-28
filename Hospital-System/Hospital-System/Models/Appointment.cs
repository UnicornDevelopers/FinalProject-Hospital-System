using Hospital_System.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
	public class Appointment
	{
		public int Id { get; set; }

		public bool IsAvailable { get; set; } // Accepted, Cancelled
		public int PatientId { get; set; }
		public int DoctorId { get; set; }
		public int AppointmentSlotId { get; set; }

		// Nav
		[ForeignKey("DoctorId")]
		public Doctor doctor { get; set; }
		[ForeignKey("PatientId")]
		public Patient patient { get; set; }

		[ForeignKey("AppointmentSlotId")]
		public AppointmentSlot appointmentSlot { get; set; }

	}
}
using Hospital_System.Models.DTOs.Appointment;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_System.Models.DTOs.AppointmentSlot
{
	public class TimeSlotViewDto
	{
		public DateTime DateView { get; set; }
		public TimeSpan HourView { get; set; }
		public int DoctorId { get; set; }

	}
}
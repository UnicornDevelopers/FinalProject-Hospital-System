using System;
using Hospital_System.Models.DTOs.Patient;
using System.ComponentModel.DataAnnotations;
using Hospital_System.Models.DTOs.AppointmentSlot;

namespace Hospital_System.Models.DTOs.Appointment
{
	public class AppointmentDTO
	{

		public int Id { get; set; }

		public bool IsAvailable { get; set; } // Accepted, Cancelled

		public int PatientId { get; set; }
		public int DoctorId { get; set; }
		public int AppointmentSlotId { get; set; }


		// Nav
		public OutDocDTO? doctor { get; set; }
		public OutPatientDTO? patient { get; set; }
		public AppointmentSlotDto? appointmentSlot { get; set; }


	}
}
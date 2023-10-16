using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.AppointmentSlot;

namespace Hospital_System.Models.Interfaces
{
	public interface IAppointmentSlot
	{
		Task<List<TimeSlotViewDto>> GetTimeSlotView(int doctorId);

		Task AddAppointment(TimeSlotViewDto timeSlot, string UserId);

		//Task<int> CreateAppointmentSlot(TimeSlotViewDto timeSlot); //return id of AppointmentSlot
		//already did it in AddAppointment 
		Task<List<AppointmentSlotDto>> GetAppointmentSlotByDoctorId(int doctorId);

		Task CancelAppointmentOfPatient(int appointmentId);//check it is is same as delete appointmen
	}
}
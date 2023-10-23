using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Appointment;

namespace Hospital_System.Models.Interfaces
{
	/// <summary>
	/// Represents a service interface for managing appointments in the hospital system.
	/// </summary>
	public interface IAppointment
	{
		///// <summary>
		///// Creates a new appointment based on the provided appointment data.
		///// </summary>
		///// <param name="Appointment">The appointment data to create.</param>
		///// <returns>The created appointment details.</returns>
		//Task<OutAppointmentDTO> CreateAppointment(AppointmentDTO Appointment);

		///// <summary>
		///// Retrieves a list of all appointments.
		///// </summary>
		///// <returns>A list of appointment details.</returns>
		//Task<List<OutAppointmentDTO>> GetAppointments();

		///// <summary>
		///// Retrieves the appointment details for a specific appointment by its ID.
		///// </summary>
		///// <param name="AppointmentID">The ID of the appointment to retrieve.</param>
		///// <returns>The appointment details.</returns>
		//Task<OutAppointmentDTO> GetAppointment(int AppointmentID);

		///// <summary>
		///// Updates an existing appointment based on the provided appointment data.
		///// </summary>
		///// <param name="id">The ID of the appointment to update.</param>
		///// <param name="DoctorDTO">The updated appointment data.</param>
		///// <returns>The updated appointment details.</returns>
		//Task<AppointmentDTO> UpdateAppointment(int id, AppointmentDTO appointmentDTO);

		///// <summary>
		///// Deletes an appointment with the specified ID.
		///// </summary>
		///// <param name="id">The ID of the appointment to delete.</param>
		///// <returns>A task representing the completion of the deletion operation.</returns>
		//Task DeleteAppointment(int id);


		//// Task<IEnumerable<AppointmentDTO>> GetAppointmentsAsync(int doctorId, DateTime date);




		Task<List<OutAppointmentDTO>> GetAppointments();
		Task<OutAppointmentDTO> GetAppointmentById(int id);
		Task<OutAppointmentDTO> CreateAppointment(AppointmentDTO appointmentDto);
		Task<OutAppointmentDTO> UpdateAppointment(int id, AppointmentDTO appointmentDto);
		Task<bool> DeleteAppointmentAsync(int id);
		Task<List<Appointment>> GetAppointmentsForPatient(string userId);

		 Task<List<Appointment>> GetAppointmentsForDoctor(string userId);


    }


}
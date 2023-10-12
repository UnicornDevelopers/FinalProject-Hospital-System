using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.DTOs.Doctor;

namespace Hospital_System.Models.Interfaces
{
    /// <summary>
    /// Represents a service interface for managing doctors in the hospital system.
    /// </summary>
    public interface IDoctor
    {
        /// <summary>
        /// Creates a new doctor based on the provided doctor data.
        /// </summary>
        /// <param name="doctor">The doctor data to create.</param>
        /// <returns>The created doctor details.</returns>
        Task<OutDocDTO> Create(InDoctorDTO doctor);

        /// <summary>
        /// Retrieves a list of all doctors.
        /// </summary>
        /// <returns>A list of doctor details.</returns>
        Task<List<OutDocDTO>> GetDoctors();

        /// <summary>
        /// Retrieves the doctor details for a specific doctor by their ID.
        /// </summary>
        /// <param name="DoctorID">The ID of the doctor to retrieve.</param>
        /// <returns>The doctor details.</returns>
        Task<DoctorDTO> GetDoctor(int DoctorID);

        /// <summary>
        /// Updates an existing doctor based on the provided doctor data.
        /// </summary>
        /// <param name="id">The ID of the doctor to update.</param>
        /// <param name="DoctorDTO">The updated doctor data.</param>
        /// <returns>The updated doctor details.</returns>
        Task<InDoctorDTO> UpdateDoctor(int id, InDoctorDTO DoctorDTO);

        /// <summary>
        /// Deletes a doctor with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the doctor to delete.</param>
        /// <returns>A task representing the completion of the deletion operation.</returns>
        Task Delete(int id);

        // <summary>
        // Retrieves a list of appointments for a specific doctor.
        // </summary>
        // <param name="DoctorID">The ID of the doctor to retrieve appointments for.</param>
        // <returns>A list of appointment details.</returns>
        // Task<DoctorDTO> GetAppointments(int DoctorID);

        Task<List<DoctorAppointmentsDTO>> GetAppointmentsForDoctor(int doctorId);
    }

}

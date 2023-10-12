using Hospital_System.Models.DTOs.Patient;
namespace Hospital_System.Models.Interfaces
{
    /// <summary>
    /// Represents a service interface for managing patients in the system.
    /// </summary>
    public interface IPatient
    {
        /// <summary>
        /// Creates a new patient based on the provided patient data.
        /// </summary>
        /// <param name="Patient">The patient data to create.</param>
        /// <returns>The created patient details.</returns>
        Task<OutPatientDTO> Create(InPatientDTO Patient);

        /// <summary>
        /// Retrieves a list of all patients.
        /// </summary>
        /// <returns>A list of patient details.</returns>
        Task<List<OutPatientDTO>> GetPatients();

        /// <summary>
        /// Retrieves the patient details for a specific patient by their ID.
        /// </summary>
        /// <param name="PatientID">The ID of the patient to retrieve.</param>
        /// <returns>The patient details.</returns>
        Task<PatientDTO> GetPatient(int PatientID);

        /// <summary>
        /// Updates an existing patient based on the provided patient data.
        /// </summary>
        /// <param name="id">The ID of the patient to update.</param>
        /// <param name="DoctorDTO">The updated patient data.</param>
        /// <returns>The updated patient details.</returns>
        Task<OutPatientDTO> UpdatePatient(int id, InPatientDTO DoctorDTO);

        /// <summary>
        /// Deletes a patient with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        /// <returns>A task representing the completion of the deletion operation.</returns>
        Task Delete(int id);
    }

}
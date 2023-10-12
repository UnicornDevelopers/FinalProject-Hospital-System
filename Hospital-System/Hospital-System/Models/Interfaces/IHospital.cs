using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Hospital;

namespace Hospital_System.Models.Interfaces
{
    /// <summary>
    /// Represents a service interface for managing hospitals in the system.
    /// </summary>
    public interface IHospital
    {
        /// <summary>
        /// Creates a new hospital based on the provided hospital data.
        /// </summary>
        /// <param name="hospital">The hospital data to create.</param>
        /// <returns>The created hospital details.</returns>
        Task<OutHospitalDTO> Create(OutHospitalDTO hospital);

        /// <summary>
        /// Retrieves a list of all hospitals.
        /// </summary>
        /// <returns>A list of hospital details.</returns>
        Task<List<OutHospitalDTO>> GetHospitals();

        /// <summary>
        /// Retrieves the hospital details for a specific hospital by its ID.
        /// </summary>
        /// <param name="HospitalID">The ID of the hospital to retrieve.</param>
        /// <returns>The hospital details.</returns>
        Task<InHospitalDTO> GetHospital(int HospitalID);

        /// <summary>
        /// Updates an existing hospital based on the provided hospital data.
        /// </summary>
        /// <param name="id">The ID of the hospital to update.</param>
        /// <param name="hospitalDTO">The updated hospital data.</param>
        /// <returns>The updated hospital details.</returns>
        Task<OutHospitalDTO> UpdateHospital(int id, OutHospitalDTO hospitalDTO);

        /// <summary>
        /// Deletes a hospital with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the hospital to delete.</param>
        /// <returns>A task representing the completion of the deletion operation.</returns>
        Task Delete(int id);

        Task<List<OutDepartmentDTO>> GetDepartmentsInHospital(int HospitalId);
    }

}

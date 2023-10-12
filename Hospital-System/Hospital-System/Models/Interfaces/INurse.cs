using Hospital_System.Models.DTOs.Nurse;

namespace Hospital_System.Models.Interfaces
{
    /// <summary>
    /// Represents a service interface for managing nurses in the system.
    /// </summary>
    public interface INurse
    {
        /// <summary>
        /// Creates a new nurse based on the provided nurse data.
        /// </summary>
        /// <param name="Nurse">The nurse data to create.</param>
        /// <returns>The created nurse details.</returns>
        Task<NurseDTO> Create(InNurseDTO Nurse);

        /// <summary>
        /// Retrieves a list of all nurses.
        /// </summary>
        /// <returns>A list of nurse details.</returns>
        Task<List<InNurseDTO>> GetNurses();

        /// <summary>
        /// Retrieves the nurse details for a specific nurse by their ID.
        /// </summary>
        /// <param name="NurseID">The ID of the nurse to retrieve.</param>
        /// <returns>The nurse details.</returns>
        Task<NurseDTO> GetNurse(int NurseID);

        /// <summary>
        /// Updates an existing nurse based on the provided nurse data.
        /// </summary>
        /// <param name="id">The ID of the nurse to update.</param>
        /// <param name="nurseDto">The updated nurse data.</param>
        /// <returns>The updated nurse details.</returns>
        Task<InNurseDTO> UpdateNurse(int id, InNurseDTO nurseDto);

        /// <summary>
        /// Deletes a nurse with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the nurse to delete.</param>
        /// <returns>A task representing the completion of the deletion operation.</returns>
        Task Delete(int id);
    }

}
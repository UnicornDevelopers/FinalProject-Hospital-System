using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Medicine;

namespace Hospital_System.Models.Interfaces
{
    using Hospital_System.Models.DTOs.Medicine;
    /// <summary>
    /// Represents a service interface for managing medicines in the system.
    /// </summary>
    public interface IMedicine
    {
        /// <summary>
        /// Creates a new medicine based on the provided medicine data.
        /// </summary>
        /// <param name="Medicine">The medicine data to create.</param>
        /// <returns>The created medicine details.</returns>
        Task<InMedicineDTO> CreateMedicine(InMedicineDTO Medicine);

        /// <summary>
        /// Retrieves a list of all medicines.
        /// </summary>
        /// <returns>A list of medicine details.</returns>
        Task<List<OutMedicineDTO>> GetMedicines();

        /// <summary>
        /// Retrieves the medicine details for a specific medicine by its ID.
        /// </summary>
        /// <param name="MedicineID">The ID of the medicine to retrieve.</param>
        /// <returns>The medicine details.</returns>
        Task<InMedicineDTO> GetMedicine(int MedicineID);

        /// <summary>
        /// Updates an existing medicine based on the provided medicine data.
        /// </summary>
        /// <param name="id">The ID of the medicine to update.</param>
        /// <param name="medicineDto">The updated medicine data.</param>
        /// <returns>The updated medicine details.</returns>
        Task<InMedicineDTO> UpdateMedicine(int id, InMedicineDTO medicineDto);

        /// <summary>
        /// Deletes a medicine with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the medicine to delete.</param>
        /// <returns>A task representing the completion of the deletion operation.</returns>
        Task DeleteMedicine(int id);
    }

}

using Hospital_System.Models.DTOs.MedicalReport;

namespace Hospital_System.Models.Interfaces
{
    /// <summary>
    /// Represents a service interface for managing medical reports in the system.
    /// </summary>
    public interface IMedicalReport
    {
        /// <summary>
        /// Creates a new medical report based on the provided medical report data.
        /// </summary>
        /// <param name="MedicalReportDto">The medical report data to create.</param>
        /// <returns>The created medical report details.</returns>
        Task<OutMedicalReportDTO> CreateMedicalReport(InMedicalReportDTO MedicalReportDto);

        /// <summary>
        /// Retrieves a list of all medical reports.
        /// </summary>
        /// <returns>A list of medical report details.</returns>
        Task<List<OutMedicalReportDTO>> GetMedicalReports();

        /// <summary>
        /// Retrieves the medical report details for a specific medical report by its ID.
        /// </summary>
        /// <param name="MedicalReportID">The ID of the medical report to retrieve.</param>
        /// <returns>The medical report details.</returns>
        Task<NewMedicalReportDTO> GetMedicalReport(int MedicalReportID);

        /// <summary>
        /// Updates an existing medical report based on the provided medical report data.
        /// </summary>
        /// <param name="id">The ID of the medical report to update.</param>
        /// <param name="MedicalReportID">The updated medical report data.</param>
        /// <returns>The updated medical report details.</returns>
        Task<OutMedicalReportDTO> UpdateMedicalReport(int id, InMedicalReportDTO MedicalReportID);

        /// <summary>
        /// Deletes a medical report with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the medical report to delete.</param>
        /// <returns>A task representing the completion of the deletion operation.</returns>
        Task DeleteMedicalReport(int id);
    }

}

using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.DTOs.Room;

namespace Hospital_System.Models.Interfaces
{
    /// <summary>
    /// Represents a service interface for managing departments in the hospital system.
    /// </summary>
    public interface IDepartment
    {
        /// <summary>
        /// Creates a new department based on the provided department data.
        /// </summary>
        /// <param name="Department">The department data to create.</param>
        /// <returns>The created department details.</returns>
        Task<InDepartmentDTO> CreateDepartment(InDepartmentDTO Department);

        /// <summary>
        /// Retrieves a list of all departments.
        /// </summary>
        /// <returns>A list of department details.</returns>
        Task<List<OutDepartmentDTO>> GetDepartments();

        /// <summary>
        /// Retrieves the department details for a specific department by its ID.
        /// </summary>
        /// <param name="DepartmentID">The ID of the department to retrieve.</param>
        /// <returns>The department details.</returns>
        Task<GetDeptmartmentDTO> GetDepartment(int DepartmentID);

        /// <summary>
        /// Updates an existing department based on the provided department data.
        /// </summary>
        /// <param name="id">The ID of the department to update.</param>
        /// <param name="DoctorDTO">The updated department data.</param>
        /// <returns>The updated department details.</returns>
        Task<OutDepartmentDTO> UpdateDepartment(int id, OutDepartmentDTO DoctorDTO);

        /// <summary>
        /// Deletes a department with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        /// <returns>A task representing the completion of the deletion operation.</returns>
        Task DeleteDepartment(int id);

        /// <summary>
        /// Retrieves a list of doctors in a specific department.
        /// </summary>
        /// <param name="departmentId">The ID of the department to retrieve doctors from.</param>
        /// <returns>A list of doctor details.</returns>
        Task<List<InDoctorDTO>> GetDoctorsInDepartment(int departmentId);


        Task<List<InNurseDTO>> GetNursesInDepartment(int departmentId);

        Task<List<RoomPatientDTO>> GetRoomsAndPatientsInDepartment(int departmentId);

        Task<List<OutRoomDTO>> GetRoomsInDepartment(int departmentId);
    }

}

using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.DTOs.Room;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;

namespace Hospital_System.Controllers
{
    /// <summary>
    /// Controller responsible for managing department-related operations.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartment _department;
        /// <summary>
        /// Initializes a new instance of the DepartmentsController class.
        /// </summary>
        /// <param name="department">The department service.</param>
        public DepartmentsController(IDepartment department)
        {
            _department = department;
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of all departments.
        /// </summary>
        /// <returns>A list of departments.</returns>
        // GET: api/Departments
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutDepartmentDTO>>> GetDepartments()
        {
            var departments = await _department.GetDepartments();
            return Ok(departments);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to retrieve.</param>
        /// <returns>The retrieved department.</returns>
        // GET: api/Departments/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor, Nurse")]
        public async Task<ActionResult<GetDeptmartmentDTO>> GetDepartment(int id)
        {
            GetDeptmartmentDTO TheDepartment = await _department.GetDepartment(id);

            if (TheDepartment == null)
            {
                return NotFound();
            }

            return TheDepartment;
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to update.</param>
        /// <param name="department">The updated department data.</param>
        /// <returns>The updated department.</returns>
        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, OutDepartmentDTO department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            var updateDepartment = await _department.UpdateDepartment(id, department);
            return Ok(updateDepartment);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="department">The department data to create.</param>
        /// <returns>The created department.</returns>
        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InDepartmentDTO>> PostDepartment(InDepartmentDTO department)
        {
            if (department == null)
            {
                return Problem("Entity set 'Departments'  is null.");
            }
            if (department.Id == null)
            {
                return NotFound();
            }
            var newDepartment = await _department.CreateDepartment(department);

            return Ok(newDepartment);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        /// <returns>A success message.</returns>
        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _department.DeleteDepartment(id);
            return NoContent();
        }


        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of doctors in a specific department.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>A list of doctors in the department.</returns>
        // GET: api/Department/{departmentId}/Doctors
        [HttpGet("{departmentId}/Doctors")]
        public async Task<ActionResult<List<InDoctorDTO>>> GetDoctorsInDepartment(int departmentId)
        {
            var doctors = await _department.GetDoctorsInDepartment(departmentId);
            return Ok(doctors);
        }


        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of Nurses in a specific department.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>A list of doctors in the department.</returns>
        // GET: api/Department/{departmentId}/Doctors
        [HttpGet("{departmentId}/Nurses")]
        public async Task<ActionResult<List<InNurseDTO>>> GetNursesInDepartment(int departmentId)
        {
            var doctors = await _department.GetNursesInDepartment(departmentId);
            return Ok(doctors);
        }


        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of Rooms in a specific department.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>A list of doctors in the department.</returns>
        // GET: api/Department/{departmentId}/Doctors
        [HttpGet("{departmentId}/Rooms/Patients")]
        public async Task<ActionResult<List<InNurseDTO>>> GetRoomsAndPatientsInDepartment(int departmentId)
        {
            var rooms = await _department.GetRoomsAndPatientsInDepartment(departmentId);
            return Ok(rooms);
        }



        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of Rooms and patients in a specific department.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>A list of doctors in the department.</returns>
        // GET: api/Department/{departmentId}/Doctors
        [HttpGet("{departmentId}/Rooms")]
        public async Task<ActionResult<List<RoomPatientDTO>>> GetRoomsInDepartment(int departmentId)
        {
            var rooms = await _department.GetRoomsInDepartment(departmentId);
            return Ok(rooms);
        }
    }
}

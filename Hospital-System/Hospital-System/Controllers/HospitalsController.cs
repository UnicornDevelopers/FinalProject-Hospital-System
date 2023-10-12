using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Hospital;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    /// <summary>
    /// Controller responsible for managing hospital-related operations.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly IHospital _context;

        /// <summary>
        /// Initializes a new instance of the HospitalsController class.
        /// </summary>
        /// <param name="context">The hospital service.</param>
        public HospitalsController(IHospital context)
        {
            _context = context;
        }


        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of all hospitals.
        /// </summary>
        /// <returns>A list of hospitals.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutHospitalDTO>>> GetHospitals()
        {
            return await _context.GetHospitals();
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a hospital by its ID.
        /// </summary>
        /// <param name="id">The ID of the hospital to retrieve.</param>
        /// <returns>The retrieved hospital.</returns>
        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InHospitalDTO>> GetHospital(int id)
        {
            try
            {
                return await _context.GetHospital(id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates a hospital by its ID.
        /// </summary>
        /// <param name="id">The ID of the hospital to update.</param>
        /// <param name="hospital">The updated hospital data.</param>
        /// <returns>The updated hospital.</returns>
        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<OutHospitalDTO>> PutHospital(int id, OutHospitalDTO hospital)
        {
            if (id != hospital.Id)
            {
                return BadRequest();
            }
            try
            {
                return await _context.UpdateHospital(id, hospital);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Creates a new hospital.
        /// </summary>
        /// <param name="hospital">The hospital data to create.</param>
        /// <returns>The created hospital.</returns>
        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OutHospitalDTO>> PostHospital(OutHospitalDTO hospital)
        {
            try
            {
                return await _context.Create(hospital);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Deletes a hospital by its ID.
        /// </summary>
        /// <param name="id">The ID of the hospital to delete.</param>
        /// <returns>A success message.</returns>
        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospital(int id)
        {
            try
            {
                await _context.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of Departments  in a specific Hospital.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>A list of doctors in the department.</returns>
        // GET: api/Department/{departmentId}/Doctors
        [HttpGet("{HospitalID}/Departments")]
        public async Task<ActionResult<List<OutDepartmentDTO>>> GetDepartmentsInHospital(int HospitalID)
        {
            var rooms = await _context.GetDepartmentsInHospital(HospitalID);
            return Ok(rooms);
        }
    }
}

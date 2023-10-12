using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Doctor;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Hospital_System.Models.DTOs.Nurse;

namespace Hospital_System.Controllers
{
    /// <summary>
    /// Controller responsible for managing doctor-related operations.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctor _context;
        /// <summary>
        /// Initializes a new instance of the DoctorsController class.
        /// </summary>
        /// <param name="context">The doctor service.</param>
        public DoctorsController(IDoctor context)
        {
            _context = context;
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of all doctors.
        /// </summary>
        /// <returns>A list of doctors.</returns>
        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutDocDTO>>> GetDoctors()
        {
            return await _context.GetDoctors();
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a doctor by their ID.
        /// </summary>
        /// <param name="id">The ID of the doctor to retrieve.</param>
        /// <returns>The retrieved doctor.</returns>
        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDTO>> GetDoctor(int id)
        {
            try
            {
                return await _context.GetDoctor(id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates a doctor by their ID.
        /// </summary>
        /// <param name="id">The ID of the doctor to update.</param>
        /// <param name="doctor">The updated doctor data.</param>
        /// <returns>The updated doctor.</returns>
        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<InDoctorDTO>> PutDoctor(int id, InDoctorDTO doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }
            try
            {
                return await _context.UpdateDoctor(id, doctor);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Creates a new doctor.
        /// </summary>
        /// <param name="doctor">The doctor data to create.</param>
        /// <returns>The created doctor.</returns>
        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OutDocDTO>> PostDoctor(InDoctorDTO doctor)
        {
            try
            {
                return await _context.Create(doctor);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Deletes a doctor by their ID.
        /// </summary>
        /// <param name="id">The ID of the doctor to delete.</param>
        /// <returns>A success message.</returns>
        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
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
        /// Retrieves a list of Appointments for a specific doctor.
        /// </summary>
        /// <param name="doctorId">The ID of the department.</param>
        /// <returns>A list of doctors in the department.</returns>
        // GET: api/Department/{departmentId}/Doctors
        [HttpGet("{doctorId}/Appointments")]
        public async Task<ActionResult<List<InNurseDTO>>> GetRoomsAndPatientsInDepartment(int doctorId)
        {
            var appointments = await _context.GetAppointmentsForDoctor(doctorId);
            return Ok(appointments);
        }




    }
}

using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata.Ecma335;
namespace Hospital_System.Controllers
{
    /// <summary>
    /// Controller responsible for managing patient-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatient _patient;
        /// <summary>
        /// Initializes a new instance of the PatientsController class.
        /// </summary>
        /// <param name="patient">The patient service.</param>
        public PatientsController(IPatient patient)
        {
            _patient = patient;
        }

        /// <summary>
        /// Retrieves a list of all patients.
        /// </summary>
        /// <returns>A list of patients.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutPatientDTO>>> GetPatients()
        {
            var patients = await _patient.GetPatients();
            return Ok(patients);
        }
        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to retrieve.</param>
        /// <returns>The retrieved patient.</returns>
        // GET: api/Appointments/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<PatientDTO>> GetPatient(int id)
        {
            var patient = await _patient.GetPatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to update.</param>
        /// <param name="UpdatedPatient">The updated patient data.</param>
        /// <returns>The updated patient.</returns>
        // PUT: api/Appointment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, InPatientDTO UpdatedPatient)
        {
            if (UpdatedPatient == null)
            {
                return BadRequest("you need to sent Patient information to be edited");
            }
            var CurrentPatient = await _patient.UpdatePatient(id, UpdatedPatient);
            if (CurrentPatient == null)
            {
                return BadRequest("There are some erorrs in this request or room is not avaliable");
            }
            return Ok(CurrentPatient);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Creates a new patient.
        /// </summary>
        /// <param name="Patient">The patient data to create.</param>
        /// <returns>The created patient.</returns>
        // POST: api/Appointment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OutPatientDTO>> PostPatient(InPatientDTO Patient)
        {
            if (Patient == null)
            {
                return Problem("Entity set 'Patient'  is null.");
            }
            var newPatient = await _patient.Create(Patient);
            if (newPatient == null)
            {
                return Ok("this room is unavilable");
            }
            return Ok(newPatient);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Deletes a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        /// <returns>A success message.</returns>
        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            await _patient.Delete(id);
            return Ok("Patient was removed seccussfully");
        }
    }
}
using Hospital_System.Models;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;

namespace Hospital_System.Controllers
{
    /// <summary>
    /// Controller responsible for managing medical report-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalReportsController : ControllerBase
    {
        private readonly IMedicalReport _medicalReport;

        /// <summary>
        /// Initializes a new instance of the MedicalReportsController class.
        /// </summary>
        /// <param name="medicalReport">The medical report service.</param>
        public MedicalReportsController(IMedicalReport medicalReport)
        {
            _medicalReport = medicalReport;
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of all medical reports.
        /// </summary>
        /// <returns>A list of medical reports.</returns>
        // GET: api/MedicalReports
        [HttpGet]
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<ActionResult<IEnumerable<OutMedicalReportDTO>>> GetMedicalReports()
        {
            var medicalReport = await _medicalReport.GetMedicalReports();
            return Ok(medicalReport);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a medical report by its ID.
        /// </summary>
        /// <param name="id">The ID of the medical report to retrieve.</param>
        /// <returns>The retrieved medical report.</returns>
        // GET: api/MedicalReport/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor, Patient")]
        public async Task<ActionResult<NewMedicalReportDTO>> GetMedicalReport(int id)
        {
            NewMedicalReportDTO TheMedicalReport = await _medicalReport.GetMedicalReport(id);

            if (TheMedicalReport == null)
            {
                return NotFound();
            }

            return TheMedicalReport;
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates a medical report by its ID.
        /// </summary>
        /// <param name="id">The ID of the medical report to update.</param>
        /// <param name="medicalReport">The updated medical report data.</param>
        /// <returns>The updated medical report.</returns>
        // PUT: api/MedicalReport/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> PutMedicalReport(int id, InMedicalReportDTO medicalReport)
        {
            if (id != medicalReport.Id)
            {
                return BadRequest();
            }
            var updateMedicalReport = await _medicalReport.UpdateMedicalReport(id, medicalReport);
            return Ok(updateMedicalReport);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Creates a new medical report.
        /// </summary>
        /// <param name="medicalReport">The medical report data to create.</param>
        /// <returns>The created medical report.</returns>
        // POST: api/MedicalReport
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "Doctor")]
        public async Task<ActionResult<OutMedicalReportDTO>> PostMedicalReport(InMedicalReportDTO medicalReport)
        {
            if (medicalReport == null)
            {
                return Problem("Entity set 'MedicalReport'  is null.");
            }
            if (medicalReport.Id == null)
            {
                return NotFound();
            }
            var newMedicalReport = await _medicalReport.CreateMedicalReport(medicalReport);

            return Ok(newMedicalReport);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Deletes a medical report by its ID.
        /// </summary>
        /// <param name="id">The ID of the medical report to delete.</param>
        /// <returns>A success message.</returns>
        // DELETE: api/MedicalReport/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> DeleteMedicalReport(int id)
        {
            await _medicalReport.DeleteMedicalReport(id);
            return NoContent();
        }
    }
}

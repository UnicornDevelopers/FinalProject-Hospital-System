using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Medicine;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
namespace Hospital_System.Controllers
{
    /// <summary>
    /// Controller responsible for managing medicine-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicine _medicine;
        /// <summary>
        /// Initializes a new instance of the MedicinesController class.
        /// </summary>
        /// <param name="medicine">The medicine service.</param>

        public MedicinesController(IMedicine medicine)
        {
            _medicine = medicine;
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of all medicines.
        /// </summary>
        /// <returns>A list of medicines.</returns>
        // GET: api/Medicines
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult<IEnumerable<OutMedicineDTO>>> GetMedicines()
        {
            var TheMedicine = await _medicine.GetMedicines();
            return Ok(TheMedicine);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a medicine by its ID.
        /// </summary>
        /// <param name="id">The ID of the medicine to retrieve.</param>
        /// <returns>The retrieved medicine.</returns>
        // GET: api/Medicine/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult<InMedicineDTO>> GetMedicine(int id)
        {
            InMedicineDTO TheMedicine = await _medicine.GetMedicine(id);
            if (TheMedicine == null)
            {
                return NotFound();
            }
            return TheMedicine;
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates a medicine by its ID.
        /// </summary>
        /// <param name="id">The ID of the medicine to update.</param>
        /// <param name="medicine">The updated medicine data.</param>
        /// <returns>The updated medicine.</returns>
        // PUT: api/Medicine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> PutMedicine(int id, InMedicineDTO medicine)
        {
            if (id != medicine.Id)
            {
                return BadRequest();
            }
            var updateMedicine = await _medicine.UpdateMedicine(id, medicine);
            return Ok(updateMedicine);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Creates a new medicine.
        /// </summary>
        /// <param name="medicine">The medicine data to create.</param>
        /// <returns>The created medicine.</returns>
        // POST: api/Medicine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult<InMedicineDTO>> PostMedicine(InMedicineDTO medicine)
        {
            if (medicine == null)
            {
                return Problem("Entity set 'Medicine'  is null.");
            }
            if (medicine.Id == null)
            {
                return NotFound();
            }
            var newMedicine = await _medicine.CreateMedicine(medicine);
            return Ok(newMedicine);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Deletes a medicine by its ID.
        /// </summary>
        /// <param name="id">The ID of the medicine to delete.</param>
        /// <returns>A success message.</returns>
        // DELETE: api/Medicine/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            await _medicine.DeleteMedicine(id);
            return NoContent();
        }
    }
}
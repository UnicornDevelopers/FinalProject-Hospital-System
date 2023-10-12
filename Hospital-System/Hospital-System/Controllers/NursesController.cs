using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.DTOs.Nurse;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Hospital_System.Controllers
{
    /// <summary>
    /// Controller responsible for managing nurse-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : ControllerBase
    {
        private readonly INurse _nurse;
        /// <summary>
        /// Initializes a new instance of the NursesController class.
        /// </summary>
        /// <param name="nurse">The nurse service.</param>
        public NursesController(INurse nurse)
        {
            _nurse = nurse;
        }


        //----------------------------------------------------------------------------------------------


        /// <summary>
        /// Retrieves a list of all nurses.
        /// </summary>
        /// <returns>A list of nurses.</returns>
        // GET: api/Nurse
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<InNurseDTO>> GetAllNurses()
        {
            var nurses = await _nurse.GetNurses();
            if (nurses == null)
            {
                return NotFound();
            }
            return Ok(nurses);
        }



        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a nurse by their ID.
        /// </summary>
        /// <param name="id">The ID of the nurse to retrieve.</param>
        /// <returns>The retrieved nurse.</returns>
        // GET: api/Nurse/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<NurseDTO>> GetNurse(int id)
        {
            var nurse = await _nurse.GetNurse(id);
            if (nurse == null)
            {
                return NotFound();
            }

            return Ok(nurse);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates a nurse by their ID.
        /// </summary>
        /// <param name="id">The ID of the nurse to update.</param>
        /// <param name="nurse">The updated nurse data.</param>
        /// <returns>The updated nurse.</returns>
        // PUT: api/Nurse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<InNurseDTO>> PutNurse(int id, InNurseDTO nurse)
        {

            if (id != nurse.Id)
            {
                return BadRequest();
            }
            try
            {
                return await _nurse.UpdateNurse(id, nurse);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Creates a new nurse.
        /// </summary>
        /// <param name="nurse">The nurse data to create.</param>
        /// <returns>The created nurse.</returns>
        // POST: api/Nurse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<NurseDTO>> PostNurse(InNurseDTO nurse)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var addedNurse = await _nurse.Create(nurse);

                return CreatedAtAction(nameof(GetNurse), new { id = nurse.Id }, addedNurse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");

            }

        }




        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Deletes a nurse by their ID.
        /// </summary>
        /// <param name="id">The ID of the nurse to delete.</param>
        /// <returns>A success message.</returns>
        // DELETE: api/Nurse/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNurse(int id)
        {
            try
            {
                var nurse = await _nurse.GetNurse(id);

                if (nurse == null)
                    return NotFound();

                await _nurse.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }







    }
}




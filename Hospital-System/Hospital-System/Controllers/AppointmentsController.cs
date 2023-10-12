using Hospital_System.Models.Interfaces;
using Hospital_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hospital_System.Models.DTOs.AppointmentDTO;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Appointment;

namespace Hospital_System.Controllers
{
    /// <summary>
    /// Controller responsible for managing appointment-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointment _appointment;
        /// <summary>
        /// Initializes a new instance of the AppointmentsController class.
        /// </summary>
        /// <param name="appointment">The appointment service.</param>
        public AppointmentsController(IAppointment appointment)
        {
            _appointment = appointment;
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of all appointments.
        /// </summary>
        /// <returns>A list of appointments.</returns>
        // GET: api/Appointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutAppointmentDTO>>> GetAppointments()
        {
            var appointment = await _appointment.GetAppointments();
            return Ok(appointment);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to retrieve.</param>
        /// <returns>The retrieved appointment.</returns>
        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OutAppointmentDTO>> GetAppointment(int id)
        {
            OutAppointmentDTO TheAppointment = await _appointment.GetAppointment(id);

            if (TheAppointment == null)
            {
                return NotFound();
            }

            return TheAppointment;
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to update.</param>
        /// <param name="appointment">The updated appointment data.</param>
        /// <returns>The updated appointment.</returns>
        // PUT: api/Appointment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<InAppoinmentDTO>> PutAppointment(int id, InAppoinmentDTO appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }
            try
            {
                return Ok(await _appointment.UpdateAppointment(id, appointment));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="appointment">The appointment data to create.</param>
        /// <returns>The created appointment.</returns>
        // POST: api/Appointment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OutAppointmentDTO>> PostAppointment(InAppoinmentDTO appointment)
        {
           
            try
            {
                return await _appointment.CreateAppointment(appointment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        /// <returns>A success message.</returns>
        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointment.DeleteAppointment(id);
            return NoContent();
        }
    }
}

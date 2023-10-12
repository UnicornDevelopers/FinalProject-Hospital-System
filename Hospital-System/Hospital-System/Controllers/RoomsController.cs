using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Room;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hospital_System.Controllers
{  /// <summary>
   /// Controller responsible for managing room-related operations.
   /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Initializes a new instance of the RoomsController class.
    /// </summary>
    /// <param name="room">The room service.</param>
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        public RoomsController(IRoom room)
        {
            _room = room;
        }

        /// <summary>
        /// Retrieves a list of all rooms.
        /// </summary>
        /// <returns>A list of rooms.</returns>
        // GET: api/Room
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor, Nurse")]
        public async Task<ActionResult<IEnumerable<OutRoomDTO>>> GetRooms()
        {
            var room = await _room.GetRooms();
            return Ok(room);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room to retrieve.</param>
        /// <returns>The retrieved room.</returns>
        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor, Nurse")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            RoomDTO TheRoom = await _room.GetRoom(id);

            if (TheRoom == null)
            {
                return NotFound();
            }

            return TheRoom;
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates a room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="room">The updated room data.</param>
        /// <returns>The updated room.</returns>
        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, OutRoomDTO room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }
            try
            {
                return Ok(await _room.UpdateRoom(id, room));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //----------------------------------------------------------------------------------------------


        /// <summary>
        /// Creates a new room.
        /// </summary>
        /// <param name="room">The room data to create.</param>
        /// <returns>The created room.</returns>
        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> PostRoom(OutRoomDTO room)
        {
            if (room == null)
            {
                return Problem("Entity set 'Rooms'  is null.");
            }
            if (room.Id == null)
            {
                return NotFound();
            }
            var newRoom = await _room.CreateRoom(room);

            return Ok(newRoom);
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Deletes a room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>
        /// <returns>No content.</returns>
        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _room.DeleteRoom(id);
            return NoContent();
        }
    }
}

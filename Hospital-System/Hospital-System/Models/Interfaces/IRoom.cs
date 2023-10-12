using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Room;

namespace Hospital_System.Models.Interfaces
{
    /// <summary>
    /// Represents a service interface for managing rooms in the system.
    /// </summary>
    public interface IRoom
    {
        /// <summary>
        /// Creates a new room based on the provided room data.
        /// </summary>
        /// <param name="room">The room data to create.</param>
        /// <returns>The created room details.</returns>
        Task<OutRoomDTO> CreateRoom(OutRoomDTO room);

        /// <summary>
        /// Retrieves a list of all rooms.
        /// </summary>
        /// <returns>A list of room details.</returns>
        Task<List<OutRoomDTO>> GetRooms();

        /// <summary>
        /// Retrieves the room details for a specific room by its ID.
        /// </summary>
        /// <param name="RoomID">The ID of the room to retrieve.</param>
        /// <returns>The room details.</returns>
        Task<RoomDTO> GetRoom(int RoomID);

        /// <summary>
        /// Updates an existing room based on the provided room data.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="roomDto">The updated room data.</param>
        /// <returns>The updated room details.</returns>
        Task<OutRoomDTO> UpdateRoom(int id, OutRoomDTO roomDto);

        /// <summary>
        /// Deletes a room with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>
        /// <returns>A task representing the completion of the deletion operation.</returns>
        Task DeleteRoom(int id);
    }

}

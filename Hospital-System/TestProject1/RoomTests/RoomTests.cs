using System.Threading.Tasks;
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Room;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using Xunit;
namespace Hospital_System.Tests.Services
{
    public class RoomTests : Mock
    {
        [Fact]
        public async Task CreateRoom_ReturnRoomDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var service = new RoomService(_db);
            var newRoom = new OutRoomDTO
            {
                RoomNumber = room.RoomNumber,
                RoomAvailability = room.RoomAvailability,
                NumberOfBeds = room.NumberOfBeds,
                DepartmentId = room.DepartmentId
            };
            // Act
            var createdRoom = await service.CreateRoom(newRoom);
            // Assert
            Assert.NotNull(createdRoom);
            Assert.Equal("101", createdRoom.RoomNumber);
            Assert.Equal(true, createdRoom.RoomAvailability);
            Assert.Equal(5, createdRoom.NumberOfBeds);
            Assert.Equal(1, createdRoom.DepartmentId);
        }
        [Fact]
        public async Task GetRoom_ReturnRoomDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var roomService = new RoomService(_db);
            var retrievedRoom = await roomService.GetRoom(room.Id);
            Assert.NotNull(retrievedRoom);
            Assert.Equal(room.Id, retrievedRoom.Id);
            Assert.Equal(room.RoomNumber, retrievedRoom.RoomNumber);
            Assert.Equal(room.NumberOfBeds, retrievedRoom.NumberOfBeds);
            Assert.Equal(room.RoomAvailability, retrievedRoom.RoomAvailability);
            //   Assert.Equal(room.DepartmentId, retrievedRoom.DepartmentId);
        }
        [Fact]
        public async Task GetRooms_ReturnListOfRoomDTOs()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room1 = await CreateAndSaveTestRoom(department.Id);
            var room2 = await CreateAndSaveTestRoom(department.Id);
            var roomService = new RoomService(_db);
            var rooms = await roomService.GetRooms();
            Assert.NotNull(rooms);
            Assert.NotEmpty(rooms);
            Assert.Equal(2, rooms.Count);
        }
        [Fact]
        public async Task UpdateRoom_ReturnUpdatedRoomDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var roomService = new RoomService(_db);
            var updatedRoomDto = new OutRoomDTO
            {
                Id = room.Id,
                RoomNumber = "102",
                RoomAvailability = false,
                NumberOfBeds = 4,
                DepartmentId = 1
            };
            var updatedRoom = await roomService.UpdateRoom(room.Id, updatedRoomDto);
            Assert.NotNull(updatedRoom);
            Assert.Equal(updatedRoomDto.RoomNumber, updatedRoom.RoomNumber);
            Assert.Equal(updatedRoomDto.RoomAvailability, updatedRoom.RoomAvailability);
        }
        [Fact]
        public async Task DeleteRoom_DeletesRoomFromDatabase()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var roomService = new RoomService(_db);
            await roomService.DeleteRoom(room.Id);
            var deletedRoom = await _db.Rooms.FindAsync(room.Id);
            Assert.Null(deletedRoom);
        }
    }
}
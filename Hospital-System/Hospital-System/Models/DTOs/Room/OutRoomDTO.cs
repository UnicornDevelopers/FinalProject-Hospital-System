namespace Hospital_System.Models.DTOs.Room
{
    public class OutRoomDTO
    {

        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public bool RoomAvailability { get; set; }
        public int NumberOfBeds { get; set; }
        public int DepartmentId { get; set; }

    }
}

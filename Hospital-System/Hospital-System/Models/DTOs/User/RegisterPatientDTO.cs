namespace Hospital_System.Models.DTOs.User
{
    public class RegisterPatientDTO
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }

        //public string? UserId { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        //public string ContactNumber { get; set; }
        public string Address { get; set; }
        public int? RoomId { get; set; }

    }
}

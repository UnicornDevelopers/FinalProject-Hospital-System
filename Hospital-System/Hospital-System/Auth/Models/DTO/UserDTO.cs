namespace Hospital_System.Auth.Models.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public IList<string> Roles { get; set; }
    }
}



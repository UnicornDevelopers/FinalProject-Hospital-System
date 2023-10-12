using System.ComponentModel.DataAnnotations;

namespace Hospital_System.Models.DTOs.User
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}

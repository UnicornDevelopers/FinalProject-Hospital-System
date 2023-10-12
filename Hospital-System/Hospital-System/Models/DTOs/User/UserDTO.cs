using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models.DTOs.User
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        


        public IList<string> Roles { get; set; }

    }
}

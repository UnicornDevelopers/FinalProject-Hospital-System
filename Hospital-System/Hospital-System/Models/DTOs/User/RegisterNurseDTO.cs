using static Hospital_System.Models.Nurse;

namespace Hospital_System.Models.DTOs.User
{
    public class RegisterNurseDTO
    {


        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }
        //public string UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        //public string ContactNumber { get; set; }

        public int DepartmentId { get; set; }

    }
}

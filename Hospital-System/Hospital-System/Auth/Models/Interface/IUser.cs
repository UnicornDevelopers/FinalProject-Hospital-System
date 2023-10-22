using Hospital_System.Auth.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Hospital_System.Auth.Models.Interface
{
    public interface IUser
    {
        //Register Method
        public Task<UserDTO> Register(RegisterUserDTO registerDto, ModelStateDictionary modelstate);
        //login Method

        public Task<UserDTO> Authenticate(string username, string password);
        // Get All users method
        public Task<UserDTO> GetUser(ClaimsPrincipal principal);
        // logout method
        public Task LogOut();
        public Task<List<ApplicationUser>> getAll();
        public Task<string> GetCurrentLoggedInDoctorId();

    }
}

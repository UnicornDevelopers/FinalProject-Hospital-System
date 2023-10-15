using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Hospital_System.Models.DTOs.User;

namespace Hospital_System.Models.Interfaces
{
    /// <summary>
    /// Represents a service interface for user-related operations in the system.
    /// </summary>
    public interface IUser
    {
        //Register Method
        public  Task<UserDTO> Register(RegisterUserDTO registerDto, ModelStateDictionary modelstate);
        //login Method

        public Task<UserDTO> Authenticate(string username, string password);
        // Get All users method
        public Task<UserDTO> GetUser(ClaimsPrincipal principal);
        // logout method
        public Task LogOut();
        public Task<List<ApplicationUser>> getAll();
    }

}

using Hospital_System.Models.DTOs.User;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Hospital_System.Auth.Models;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hospital_System.Models.Services
{
    public class UserServices : IUser
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserServices(
            UserManager<ApplicationUser> manager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = manager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);

                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user)

                };
            }

            return null;
        }

        public async Task<List<ApplicationUser>> getAll()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
            };
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserDTO> Register(RegisterUserDTO registerDto, ModelStateDictionary modelstate)
        {
            var user = new ApplicationUser()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                var roleExists = await _roleManager.RoleExistsAsync(registerDto.Roles);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(registerDto.Roles));
                }

                await _userManager.AddToRoleAsync(user, registerDto.Roles);

                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }

            foreach (var error in result.Errors)
            {
                var errorKey =
                error.Code.Contains("Password") ? nameof(registerDto.Password) :
                error.Code.Contains("Email") ? nameof(registerDto.Email) :
                  error.Code.Contains("UserName") ? nameof(registerDto.Username) :
                  "";

                modelstate.AddModelError(errorKey, error.Description);
            }

            return null;
        }
    }
}

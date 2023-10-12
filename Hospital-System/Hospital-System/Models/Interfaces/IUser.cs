using Hospital_System.Models.DTOs.User;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Hospital_System.Models.Interfaces
{
    /// <summary>
    /// Represents a service interface for user-related operations in the system.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Registers a new user based on the provided user data.
        /// </summary>
        /// <param name="registerUserDTO">The user registration data.</param>
        /// <param name="modelState">The model state dictionary for capturing validation errors.</param>
        /// <returns>The registered user details.</returns>
        Task<UserDTO> Register(RegisterUserDTO registerUserDTO, ModelStateDictionary modelState, ClaimsPrincipal claimsPrincipal);

        /// <summary>
        /// Registers a new doctor based on the provided doctor registration data.
        /// </summary>
        /// <param name="doctorRegistration">The doctor registration data.</param>
        /// <param name="modelState">The model state dictionary for capturing validation errors.</param>
        /// <returns>The registered doctor details.</returns>
        Task<UserDTO> RegisterDoctor(DoctorRegistrationDTO doctorRegistration, ModelStateDictionary modelState, ClaimsPrincipal claimsPrincipal);

        /// <summary>
        /// Registers a new nurse based on the provided nurse registration data.
        /// </summary>
        /// <param name="nurseRegistration">The nurse registration data.</param>
        /// <param name="modelState">The model state dictionary for capturing validation errors.</param>
        /// <returns>The registered nurse details.</returns>
        Task<UserDTO> RegisterNurse(RegisterNurseDTO nurseRegistration, ModelStateDictionary modelState, ClaimsPrincipal claimsPrincipal);

        /// <summary>
        /// Registers a new patient based on the provided patient registration data.
        /// </summary>
        /// <param name="patientRegistration">The patient registration data.</param>
        /// <param name="modelState">The model state dictionary for capturing validation errors.</param>
        /// <returns>The registered patient details.</returns>
        Task<UserDTO> RegisterPatient(RegisterPatientDTO patientRegistration, ModelStateDictionary modelState, ClaimsPrincipal claimsPrincipal);

        /// <summary>
        /// Authenticates a user with the provided username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The authenticated user details, or null if authentication fails.</returns>
        Task<UserDTO> Authenticate(string username, string password);

        /// <summary>
        /// Retrieves user details based on the provided claims principal.
        /// </summary>
        /// <param name="principal">The claims principal associated with the user.</param>
        /// <returns>The user details.</returns>
        Task<UserDTO> GetUser(ClaimsPrincipal principal);



    }

}

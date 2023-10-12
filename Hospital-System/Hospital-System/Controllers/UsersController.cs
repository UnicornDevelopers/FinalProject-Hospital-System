using Hospital_System.Models.DTOs.User;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Hospital_System.Controllers
{
    /// <summary>
    /// Controller responsible for managing user-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsersController : ControllerBase
    {
        private IUser userService;

        /// <summary>
        /// Initializes a new instance of the UsersController class.
        /// </summary>
        /// <param name="service">The user service.</param>
        public UsersController(IUser service)
        {
            userService = service;
        }




        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="data">The registration data.</param>
        /// <returns>The registered user.</returns>
        [AllowAnonymous]
        //[Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO data)
        {
            var user = await userService.Register(data, this.ModelState,User);

            if (ModelState.IsValid)
            {
                return user;

            }

            return BadRequest(new ValidationProblemDetails(ModelState));

        }
        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Registers a new doctor.
        /// </summary>
        /// <param name="data">The doctor registration data.</param>
        /// <returns>The registered doctor.</returns>
        [AllowAnonymous]
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("DoctorRegister")]
        public async Task<ActionResult<UserDTO>> DoctorRegister(DoctorRegistrationDTO data)
        {
            var user = await userService.RegisterDoctor(data, this.ModelState, User);

            if (ModelState.IsValid)
            {

                return user;
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }
        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Registers a new nurse.
        /// </summary>
        /// <param name="data">The nurse registration data.</param>
        /// <returns>The registered nurse.</returns>
        [AllowAnonymous]
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("NurseRegister")]
        public async Task<ActionResult<UserDTO>> NurseRegister(RegisterNurseDTO data)
        {
            var user = await userService.RegisterNurse(data, this.ModelState, User);

            if (ModelState.IsValid)
            {

                return user;
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Registers a new patient.
        /// </summary>
        /// <param name="data">The patient registration data.</param>
        /// <returns>The registered patient.</returns>
        [AllowAnonymous]
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("PatientRegister")]
        public async Task<ActionResult<UserDTO>> PatientRegister(RegisterPatientDTO data)
        {
            var user = await userService.RegisterPatient(data, this.ModelState, User);
            if (user==null)
            {
                return BadRequest("Room is unavailable");
            }
            if (ModelState.IsValid)
            {

                return user;
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }
        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="log">The login data.</param>
        /// <returns>The authenticated user.</returns>
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO log)
        {

            var user = await userService.Authenticate(log.UserName, log.Password);

            if (user == null)
            {
                return Unauthorized();

            }
            return user;
        }



        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves the profile of the currently authenticated user.
        /// </summary>
        /// <returns>The profile of the authenticated user.</returns>
        [Authorize(Roles = "Admin,Doctor,Nurse,Patient")]
        [HttpGet("Profile")]
        public async Task<ActionResult<UserDTO>> Profile()
        {
            return await userService.GetUser(this.User); ;
        }

       

    }
    }

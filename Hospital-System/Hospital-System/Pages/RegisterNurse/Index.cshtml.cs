using Hospital_System.Auth.Models.DTO;
using Hospital_System.Auth.Models.Interface;
using Hospital_System.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models;
using Hospital_System.Models.DTOs.Patient;
using Windows.Networking;
using System.Numerics;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.Services;

namespace Hospital_System.Pages.RegisterNurse
{
	public class IndexModel : PageModel
	{

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmail _email;
		private readonly IConfiguration _configuration;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public readonly IUser _user;
		private readonly INurse _iNurse;
		private readonly IDepartment _departmentService;



		public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmail emailSender, IConfiguration configuration, IUser user, INurse iNurse,IDepartment department)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_email = emailSender;
			_configuration = configuration;
			_user = user;
			_iNurse = iNurse;
			_departmentService = department;
		}

		[BindProperty]
		public RegisterUserDTO registerUser { get; set; }
		public UserDTO user { get; set; }
		public InNurseDTO nurse { get; set; }

		public List<ApplicationUser> applicationUsers { get; set; }
		public List<Department> departments { get; set; }


		public async Task OnGet()
		{

			applicationUsers = await _user.getAll();
			departments = await _departmentService.GetDepartments();


		}


		public async Task<IActionResult> OnPostAsync(RegisterUserDTO registerUser, InNurseDTO nurse)
		{
			if (!ModelState.IsValid)
			{
				departments = await _departmentService.GetDepartments();

				return Page();
			}
			user = await _user.Register(registerUser, this.ModelState);

			await OnGet();


			if (user != null)
			{
				var applicationUser = await _userManager.FindByEmailAsync(registerUser.Email);
				//await _signInManager.SignInAsync(applicationUser, isPersistent: false);

				var nurse2 = new InNurseDTO
				{
					UserId = applicationUser.Id,
					FirstName = nurse.FirstName,
					LastName = nurse.LastName,
					Gender = nurse.Gender,
					ContactNumber = nurse.ContactNumber,
					Shift = nurse.Shift,
					DepartmentId = nurse.DepartmentId

				};

				await _iNurse.Create(nurse2);

				//string subject = "welcome email";
				//string message =
				//	$"Hello {applicationUser.UserName}," +
				//	"Click here to ";

				//await _email.SendEmailAsync(applicationUser.Email, subject, message);
				TempData["SuccessRegister"] = "Nurse registered successfully";

				return Page();
			}
			else
			{
				ViewData["WrongUser"] = "Some thing wrong ";
				departments = await _departmentService.GetDepartments();


				return null;
			}


		}


	}

}
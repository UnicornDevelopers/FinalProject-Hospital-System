
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models;
using Hospital_System.Models.DTOs.Patient;
using System.Numerics;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Auth.Models.DTO;
using Hospital_System.Auth.Models.Interface;
using Hospital_System.Auth.Models;

namespace Hospital_System.Pages.RegisterDoctor
{
	public class IndexModel : PageModel
	{

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmail _email;
		private readonly IConfiguration _configuration;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public readonly IUser _user;
		private readonly IDoctor _iDoctor;
		private readonly IDepartment _departmentService;


		public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmail emailSender, IConfiguration configuration, IUser user, IDoctor iDoctor, IDepartment departmentService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_email = emailSender;
			_configuration = configuration;
			_user = user;
			_iDoctor = iDoctor;
			_departmentService = departmentService;
		}

		[BindProperty]
		public RegisterUserDTO registerUser { get; set; }
		public UserDTO user { get; set; }
		public InDoctorDTO doctor { get; set; }

		public List<ApplicationUser> applicationUsers { get; set; }
		public List<Department> departments { get; set; }


		public async Task OnGet()
		{

			applicationUsers = await _user.getAll();
			departments = await _departmentService.GetDepartments();


		}

		public async Task<IActionResult> OnPostAsync(RegisterUserDTO registerUser, InDoctorDTO doctor)
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

				var doctor2 = new InDoctorDTO
				{
					UserId = applicationUser.Id,
					FirstName = doctor.FirstName,
					LastName = doctor.LastName,
					Gender = doctor.Gender,
					ContactNumber = doctor.ContactNumber,
					Speciality = doctor.Speciality,
					DepartmentId = doctor.DepartmentId
				};

				await _iDoctor.Create(doctor2);

				//string subject = "welcome email";
				//string message =
				//	$"Hello {applicationUser.UserName}," +
				//	"Click here to ";

				//await _email.SendEmailAsync(applicationUser.Email, subject, message);


				TempData["SuccessRegister"] = "Doctor registered successfully";

				return Page();
			}
			else
			{
				departments = await _departmentService.GetDepartments();

				ViewData["WrongUser"] = "Something went wrong";

				// The user could not be registered - return the page to display an error message
				return Page();
			}
		}



	}

}
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

namespace Hospital_System.Pages.Register
{
	public class IndexModel : PageModel
	{

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmail _email;
		private readonly IConfiguration _configuration;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public readonly IUser _user;
		private readonly IPatient _iPatient;


		public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmail emailSender, IConfiguration configuration, IUser user, IPatient iPatient)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_email = emailSender;
			_configuration = configuration;
			_user = user;
			_iPatient = iPatient;
		}

		[BindProperty]
		public RegisterUserDTO registerUser { get; set; }
		public UserDTO user { get; set; }
		public InPatientDTO patient { get; set; }
		//public InPatientDTO patient2 { get; set; }

		public List<ApplicationUser> applicationUsers { get; set; }

		public async Task OnGet()
		{

			applicationUsers = await _user.getAll();

		}


		public async Task<IActionResult> OnPostAsync(RegisterUserDTO registerUser, InPatientDTO patient)
		{

			user = await _user.Register(registerUser, this.ModelState);

			await OnGet();


			if (user != null)
			{
				var applicationUser = await _userManager.FindByEmailAsync(registerUser.Email);
				await _signInManager.SignInAsync(applicationUser, isPersistent: false);

				var patient2 = new InPatientDTO
				{
					UserId = applicationUser.Id,
					FirstName = patient.FirstName,
					LastName = patient.LastName,
					DoB = patient.DoB,
					Gender = patient.Gender,
					ContactNumber = patient.ContactNumber,
					Address = patient.Address,
				};

				await _iPatient.Create(patient2);

				//string subject = "welcome email";
				//string message =
				//	$"Hello {applicationUser.UserName}," +
				//	"Click here to ";

				//await _email.SendEmailAsync(applicationUser.Email, subject, message);

				return RedirectToPage("/Home");
			}
			else
			{
				ViewData["WrongUser"] = "Some thing wrong ";

				return null;
			}


		}


	}

}
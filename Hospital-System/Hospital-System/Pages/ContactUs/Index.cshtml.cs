using Hospital_System.Auth.Models;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital_System.Pages.ContactUs
{
	public class IndexModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmail _email;
		private readonly IConfiguration _configuration;
		public IndexModel(UserManager<ApplicationUser> userManager, IEmail emailSender, IConfiguration configuration)
		{
			_userManager = userManager;
			_email = emailSender;
			_configuration = configuration;
		}
		public ReceiptInput Input { get; set; }

		public async Task<IActionResult> OnPostAsync(ReceiptInput Input)
		{
			
				var Themail = "test_ltuc950@outlook.com";

				ViewData["OrderStatusMessage"] = "Order completed successfully.";
				ApplicationUser user = await _userManager.GetUserAsync(User);


				string subject = "Patient Information: Urgent";
				string message =
					"Dear Team,\n\nI am writing to provide you with some important information about one of our patients who has contacted us. " +
					$"\n\nPlease find attached a summary of {user.UserName}Contact information. " +
					"\n\nThank you for your attention to this matter. " +
					"\n\nThank you for your attention to this matter. " +
					$"\n\nName:{Input.Name}," +
					$"\n\nEmail:{Input.Email}," +
					$"\n\nPhone:{Input.Phone}," +
					$"\n\nSubject:{Input.Subject}," +
					$"\n\nMessage:{Input.Message},"+

					"matter.\n\nBest regards,\n{your_name}";

				await _email.SendEmailAsync(Themail, subject, message);



				string subject2 = "acknowledgment message";
				string message2 =
					$"Dear {user.UserName},\n\nThank you for contacting us. We have received your " +
					"information and will get back to you as soon as possible.\n\nIn the meantime, " +
					"please feel free to visit our website for more information about our services and " +
					"facilities. If you have any further questions or concerns, please do not hesitate to " +
					"contact us.\n\nThank you for choosing our hospital.\n\nBest regards,\nHospital System";

				await _email.SendEmailAsync(Input.Email, subject2, message2);

				return Page();
			
		}


		public class ReceiptInput
		{
			public string Name { get; set; }
			public string Email { get; set; }
			public string Phone { get; set; }
			public string Subject { get; set; }
			public string Message { get; set; }
			public string SubscribeNewsletter { get; set; }

		}
	}
}
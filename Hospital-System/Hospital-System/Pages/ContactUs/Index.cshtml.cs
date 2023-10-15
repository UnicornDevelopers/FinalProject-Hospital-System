//using Hospital_System.Auth.Models;
//using Hospital_System.Models.Interfaces;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace Hospital_System.Pages.ContactUs
//{
//    public class IndexModel : PageModel
//    {
//			private readonly UserManager<ApplicationUser> _userManager;
//			private readonly IEmail _email;
//			private readonly IConfiguration _configuration;
//			public IndexModel(UserManager<ApplicationUser> userManager, IEmail emailSender, IConfiguration configuration)
//			{
//				_userManager = userManager;
//				_email = emailSender;
//				_configuration = configuration;
//			}
//		public ReceiptInput Input { get; set; }

//		public async Task<IActionResult> OnGetAsync(ReceiptInput Input)
//			{
//				bool isCompleted = TempData["IsCompleted"] as bool? ?? false;

//				if (isCompleted)
//				{
//				var Themail = "test_ltuc950@outlook.com";

//				ViewData["OrderStatusMessage"] = "Order completed successfully.";
//					ApplicationUser user = await _userManager.GetUserAsync(User);


//					string subject = "Purchase Summary From Cosmetic Store!";
//					string message =
//						$"Name:{Input.Name}," +
//						$"Email:{Input.Email}," +
//						$"Phone:{Input.Phone}," +
//						$"Subject:{Input.Subject}," +
//						$"Message:{Input.Message},"
//						;

//					await _email.SendEmailAsync(Themail, subject, message);



//				string subject2 = "Purchase Summary From Cosmetic Store!";
//				string message2 =
//					$"Hello {user.UserName}," +
//					$" Below is your recent purchase summary," +
//					$" The Total: ${("F")}\n" +
//					"Click here to shop more: https://e-commerce2.azurewebsites.net/";

//				await _email.SendEmailAsync(Input.Email, subject2, message2);

//				return Page();
//				}
//				else
//				{
//					ViewData["OrderStatusMessage"] = "Order did not complete successfully.";
//				}
//				return Page();
//			}


//		public class ReceiptInput
//        {
//            public string Name { get; set; }
//            public string Email { get; set; }
//            public string Phone { get; set; }
//            public string Subject { get; set; }
//            public string Message { get; set; }
//			public string SubscribeNewsletter { get; set; }

//		}
//		}
//	}
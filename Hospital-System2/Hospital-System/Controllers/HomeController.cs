using Hospital_System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System.Controllers
{
	public class HomeController : Controller
	{
		// Home Page
		private readonly HospitalDbContext _cotext;

		public HomeController(HospitalDbContext cotext)
		{
			_cotext = cotext;
		}
		public IActionResult Index()
		{

			return View();
		}

	}
}

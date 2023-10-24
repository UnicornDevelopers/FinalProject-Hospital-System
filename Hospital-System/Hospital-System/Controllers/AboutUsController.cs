using Hospital_System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System.Controllers
{
    public class AboutUsController : Controller
    {
        // Home Page
        private readonly HospitalDbContext _cotext;

        public AboutUsController(HospitalDbContext cotext)
        {
            _cotext = cotext;
        }
        public IActionResult Index()
        {

            return View();
        }

    }
}
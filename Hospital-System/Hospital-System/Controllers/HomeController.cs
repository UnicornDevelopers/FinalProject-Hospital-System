using Hospital_System.Data;
using Microsoft.AspNetCore.Mvc;
using Hospital_System.ViewModels;

namespace Hospital_System.Controllers
{
    public class HomeController : Controller
    {
        // Home Page
        private readonly HospitalDbContext _context;

        public HomeController(HospitalDbContext cotext)
        {
            _context = cotext;
        }
        public IActionResult Index()
        {
            int totalRoomCount = _context.Rooms.Count();
            int totalDoctorCount = _context.Doctors.Count();
            int totalUsersCount = _context.Users.Count();

            var viewModel = new HomeViewModel
            {
                TotalRoomCount = totalRoomCount,
                TotalDoctorCount = totalDoctorCount,
                TotalUsersCount = totalUsersCount
            };

            return View(viewModel);
        }

    }
}
//using Hospital_System.Models;
//using Hospital_System.Models.DTOs.User;
//using Hospital_System.Models.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;

//namespace Hospital_System.Controllers
//{
//    public class MainController : Controller
//    {

//        private readonly IUser _userService;
//        public MainController(IUser userService )
//        {
//                _userService = userService;
//        }

//        [HttpGet]
//        public IActionResult Index()
//        {

//            return View();
//        }

//        [HttpGet]
//        [Route("Login")]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        //[HttpPost]
//        //[Route("Login")]
//        //public async Task<ActionResult<LoginDTO>> Login(LoginDTO loginDTO)
//        //{
//        //    var user = await _userService.Authenticate(loginDTO.UserName, loginDTO.Password);
//        //    if (user == null)
//        //    {

//        //        return View("Index", loginDTO);

//        //    }
//        //    else
//        //    {



//        //        return Redirect("/Main/Index");
//        //    }
//        //}

//        [HttpPost]
//        [Route("Login")]
//        public async Task<IActionResult> Login(SignInModel signInModel)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _userService.PasswordSignInAsync(signInModel);
//                if (result.Succeeded)
//                {
//                    return RedirectToAction("Index", "Account");
//                }

//                ModelState.AddModelError("", "Invalid information");

//            }




//            return View();
//        }
//        [HttpGet]
//        public IActionResult Register()
//        {

//            return View();
//        }

//        [HttpPost]
//        [Route("Register")]
//        public async Task<IActionResult> Register(RegisterViewModel usermodel)
//        {
//            if (ModelState.IsValid)
//            {


//                var result = await _userService.CreateUserAsync(usermodel);

//                if (!result.Succeeded)
//                {
//                    foreach (var errormessage in result.Errors)
//                    {
//                        ModelState.AddModelError("", errormessage.Description);
//                    }
//                    return View(usermodel);

//                }


//                ModelState.Clear();
//            };


//            return View();





//        }

//        //[HttpPost]
//        //public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO register)
//        //{



//        //    var user = await _userService.Register(register, this.ModelState, User);
//        //    if (ModelState.IsValid)
//        //    {
//        //        return Redirect("/Main/Index");

//        //    }
//        //    else
//        //    {
//        //        return View(register);

//        //    }


//        //}


//        [HttpGet]
//        public IActionResult DoctorRegister()
//        {

//            return View();
//        }

//        [HttpPost]
//        public async Task<ActionResult<UserDTO>> DoctorRegister(DoctorRegistrationDTO register)
//        {



//            var user = await _userService.RegisterDoctor(register, this.ModelState, User);
//            if (ModelState.IsValid)
//            {
//                //await _pharmacist.Authenticate(register.Username, register.Password);
//                return Redirect("/Main/Index");

//            }
//            else
//            {
//                return View(register);

//            }


//        }

//        [Route("Logout")]
//        public async Task<IActionResult> Logout()
//        {

//            await _userService.SignOutAsync();
//            return RedirectToAction("Index", "Account");

//        }
//    }
//}

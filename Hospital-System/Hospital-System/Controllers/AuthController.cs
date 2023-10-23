using Hospital_System.Auth.Models.DTO;
using Hospital_System.Auth.Models.Interface;
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace E_commerce_2.Controllers
{
	public class AuthController : Controller
	{
		private IUser _user;
		private HospitalDbContext _db;
		private IPatient _patient;
		private IDoctor _doctor;
		private INurse _nurse;
		private IMedicalReport _medicalReport;
		public AuthController(IUser user, HospitalDbContext db, IPatient patient,IDoctor doctor,INurse nurse, IMedicalReport medicalReport)
		{
			_user = user;
			_db= db;
			_patient = patient;
			_doctor = doctor;
			_nurse = nurse;
			_medicalReport = medicalReport;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult SingUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult<UserDTO>> SingUp(RegisterUserDTO register)
		{
			var user = await _user.Register(register, this.ModelState);
			if (ModelState.IsValid)
			{
				//await _user.Authenticate(register.Username, register.Password);
				//return Redirect("/Home/Index");
				return RedirectToAction("Index", "Home", user);

			}
			else
			{
				return View("SingUp", register);

			}

		}

		public async Task<ActionResult<UserDTO>> Authenticate(LoginDTO login)
		{
			var user = await _user.Authenticate(login.Username, login.Password);
			if (user == null)
			{
				ViewBag.WrongUser = "user name or password is wrong !! ";
				return View("Index", login);

			}
			else
			{

				TempData["AlertMessage"] = $"Welcom {login.Username} in Fast Market Website :)";
				return RedirectToAction("Index", "Home", login);

			}

		}

		[Route("Logout")]
		public async Task<IActionResult> LogOut()
		{
			await _user.LogOut();
			return RedirectToAction("Index", "Home");
		}


		public void SetCookie(string v1, string v2)
		{
			throw new NotImplementedException();
		}

		public Task<ActionResult<UserDTO>> SignUp(RegisterUserDTO registerDTO, ModelStateDictionary modelState)
		{
			throw new NotImplementedException();
		}


        public async Task<ActionResult<PatientDTO>> Profile()
        {
            var user = await _user.GetUser(this.User);
            var prfileUserPatient = await _db.Patients.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (prfileUserPatient != null)
            {
                return RedirectToAction("PatientProfile", "Auth", prfileUserPatient);
            }

            var prfileUserDoctor = await _db.Doctors.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (prfileUserDoctor != null)
            {
                return RedirectToAction("DoctorProfile", "Auth", prfileUserDoctor);
            }

            var profileUserNurse = await _db.Nurses.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (profileUserNurse != null)
            {
                return RedirectToAction("NurseProfile", "Auth", profileUserNurse);
            }

            return View(user);

        }

        [HttpGet]
        public async Task<ActionResult> PatientProfile(Patient patient)
        {
            var pateintProfile = await _patient.GetPatient(patient.Id);

			var patientMedicalReport = await _db.Patients.Where(i=>i.Id== pateintProfile.Id).OrderBy(p=>p.Id).Select(x=>x.MedicalReports).LastOrDefaultAsync();

			ViewBag.description = patientMedicalReport;

            return View(pateintProfile);

		}



        [HttpGet]
        public async Task<ActionResult> DoctorProfile(Doctor doctor)
        {
            var DoctorProfile = await _doctor.GetDoctor(doctor.Id);
            return View(DoctorProfile);
        }




		public async Task<IActionResult> NurseProfile(Nurse nurse)
		{

            var NurseProfile = await _nurse.GetNurse(nurse.Id);
            return View(NurseProfile);
        }



        public async Task<IActionResult> PatientMedicalReport(int id,Patient patient)
        {
			PatientDTO Patient;
			List<MedicalReport> patientMedicalReport;


            if (id != 0)
			{
				Patient = await _patient.GetPatient(id);
				 patientMedicalReport = await _db.MedicalReports.Where(x => x.PatientId == Patient.Id).ToListAsync();
            }
			else
			{
                Patient = await _patient.GetPatient(patient.Id);
                patientMedicalReport = await _db.MedicalReports.Where(x => x.PatientId == Patient.Id).ToListAsync();
            }
			
			//var medicalReporsts = Patient?.MedicalReports?.ToList();

            return View(patientMedicalReport);
        }
    }
}
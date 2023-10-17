﻿using Hospital_System.Auth.Models.DTO;
using Hospital_System.Auth.Models.Interface;
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Reflection.Metadata.Ecma335;

namespace E_commerce_2.Controllers
{
	public class AuthController : Controller
	{
		private IUser _user;
		private HospitalDbContext _db;
		private IPatient _patient;
		public AuthController(IUser user, HospitalDbContext db, IPatient patient )
		{
			_user = user;
			_db= db;
			_patient = patient;
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
			var prfileUser = await _db.Patients.Include(r=>r.Rooms).FirstOrDefaultAsync(x=>x.UserId==user.Id);

            return View(prfileUser);
		}
	}
}
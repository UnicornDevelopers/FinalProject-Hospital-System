using Hospital_System.Models.Interfaces;
using Hospital_System.Models.DTOs.Appointment;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Hospital_System.Models.DTOs;
using Hospital_System.Models;
using Hospital_System.Models.DTOs.AppointmentSlot;
using Hospital_System.Models.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Hospital_System.Controllers
{
	public class AppointmentController : Controller
	{
		private readonly IAppointment _appointmentService;
		private readonly IDepartment _departmentService;
		private readonly IAppointmentSlot _appointmentSlotService;
		private readonly IDoctor _doctorService;


		public AppointmentController(IAppointment appointmentService, IDepartment departmentService, IAppointmentSlot appointmentSlotService,IDoctor doctorService)
		{
			_appointmentService = appointmentService;
			_departmentService = departmentService;
			_appointmentSlotService = appointmentSlotService;
			_doctorService = doctorService;

        }




		//public async Task<IActionResult> Index()
		//{
		//    var appointments = await _appointmentService.GetAppointments();

		//    var appointmentDTOs = appointments.Select(outAppointment => new AppointmentDTO
		//    {
		//        IsAvailable = outAppointment.IsAvailable
		//    }).ToList();

		//    return View(appointmentDTOs);
		//}



		public async Task<IActionResult> SelectDepartment()
		{
			var departments = await _departmentService.GetDepartments();

			var departmentDTOs = departments.Select(outdepartment => new DepartmentDTO
			{
				Id = outdepartment.Id,
				DepartmentName = outdepartment.DepartmentName,
                Image= outdepartment.Image,

                Description = outdepartment.Description

			}).ToList();

			return View(departmentDTOs);
		}




		[HttpGet]
		public async Task<IActionResult> SelectDoctor(int departmentId)
		{
			var doctors = await _departmentService.GetDoctorsInDepartment(departmentId);
			var department = await _departmentService.GetDepartment(departmentId);


			var appointmentDTO = new AppointmentDTO
			{
				Id = departmentId
			};

			ViewBag.Doctors = doctors;
			ViewBag.DepartmentName = department.DepartmentName; // Pass the department name to the view

			return View(appointmentDTO);
		}


        public async Task<IActionResult> SelectTimeSlot(int doctorId, int pageIndex = 1)
        {
			int pageSize = 6;

            var doctor=await _doctorService.GetDoctor(doctorId);

			TempData["doctorName"] = $"{doctor.FirstName} {doctor.LastName}";
            TempData["doctorId"] = doctor.Id;
            var appointmentSlots = await _appointmentSlotService.GetTimeSlotView(doctorId);

            // Implement pagination using LINQ
            var paginatedSlots = appointmentSlots.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            // Pass the list of available time slots to the view
            TempData["PageIndex"] = pageIndex;
            TempData["TotalPages"] = (int)Math.Ceiling((double)appointmentSlots.Count() / pageSize);

            return View(paginatedSlots);
        }



        [HttpPost]
		public async Task<IActionResult> AddAppointment(int doctorId, DateTime date, TimeSpan time)
		{
			string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			TimeSlotViewDto timeSlot = new TimeSlotViewDto
			{
				DoctorId = doctorId,
				DateView = date,
				HourView = time
			};

			try
			{
				await _appointmentSlotService.AddAppointment(timeSlot, UserId);
				TempData["success"] = "Appointment has booked successfully";

				return RedirectToAction("SelectTimeSlot", new { doctorId = doctorId });
			}
			catch (Exception ex)
			{
				TempData["fail"] = ex.Message;

				return RedirectToAction("SelectTimeSlot", new { doctorId = doctorId });
			}
		}


        [HttpGet]
        public async Task<IActionResult> ViewAppointments()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = await _appointmentService.GetAppointmentsForPatient(userId);
            return View(appointments);
        }
        [HttpPost]
        public async Task<IActionResult> CancelAppointment(int appointmentId, bool isAvailable)
        {
            if (!isAvailable)
            {
                await _appointmentService.DeleteAppointmentAsync(appointmentId);
            }
            return RedirectToAction("ViewAppointments");
        }



    }
}
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



        public AppointmentController(IAppointment appointmentService, IDepartment departmentService, IAppointmentSlot appointmentSlotService)
        {
            _appointmentService = appointmentService;
            _departmentService = departmentService;
            _appointmentSlotService = appointmentSlotService;
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
                DepartmentName = outdepartment.DepartmentName

            }).ToList();

            return View(departmentDTOs);
        }




        [HttpPost]
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


        public async Task<IActionResult> SelectTimeSlot(int doctorId)
        {

            var appointmentSlots = await _appointmentSlotService.GetTimeSlotView(doctorId);

            // Pass the list of available time slots to the view
            return View(appointmentSlots);
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




        //public IActionResult Create()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public async Task<IActionResult> Create(AppointmentDTO appointmentDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var createdAppointment = await _appointmentService.CreateAppointment(appointmentDTO);
        //        if (createdAppointment != null)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }

        //    // If validation fails or time slot is not available, return to the create view with an error message
        //    ModelState.AddModelError(string.Empty, "Appointment creation failed. Please check the selected date and time.");
        //    return View(appointmentDTO);
        //}





        //public IActionResult SelectTime(int doctorId)
        //{
        //    // Generate or retrieve available time slots for the selected department and date.
        //    DateTime selectedDate = DateTime.Today; // You can change this to the selected date.
        //    var timeSlots = GenerateTimeSlots(selectedDate, departmentId);

        //    // Pass the time slots to the view.
        //    return View(timeSlots);
        //}


    }
}

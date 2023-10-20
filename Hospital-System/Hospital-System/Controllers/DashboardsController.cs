﻿using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Hospital;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.DTOs.Room;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hospital_System.Controllers
{
	public class DashboardsController : Controller
	{

		private readonly IDepartment _department;
		private readonly IHospital _hospital;
        private readonly IAppointment _appointment;
        private readonly IPatient _patient;
        private readonly IRoom _room;
        private readonly INurse _nurse;

        private readonly HospitalDbContext _context;

		public DashboardsController(IDepartment department, IHospital hospital, HospitalDbContext context, IAppointment appointment,IPatient patient,IRoom room,INurse nurse)
		{
			_department = department;
			_hospital = hospital;
			_context = context;
			_appointment = appointment;
			_patient = patient;
			_room = room;
            _nurse = nurse;
		}


		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}


		[HttpGet]
		public IActionResult AddDepartment()
		{
			return View();
		}

		

		[HttpPost]
		public async Task<IActionResult> AddDepartment(InDepartmentDTO department, IFormFile file)
		{
			if (ModelState.IsValid && file != null)
			{
				// Upload the image to Azure Blob Storage and get the URI
				await _department.GetFile(file, department);

				// Save the product to the database
				await _department.CreateDepartment(department);

				return RedirectToAction("Index", "Home");
			}

			return View(department);
		}



		[HttpGet]
		public IActionResult AddHospital()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddHospital(OutHospitalDTO hospital)
		{
			await _hospital.Create(hospital);

			return View(hospital);
		}



        [HttpGet]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                // Handle case where department is not found
                return NotFound();
            }

            return View(room);
        }


        [HttpPost, ActionName("DeleteRoom")]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> ConfirmDeleteRoom(int id)
		{
			try
			{
				var room = await _context.Rooms.FindAsync(id);

				if (room == null)
				{
					return NotFound();
				}

				_context.Entry(room).State = EntityState.Deleted;
				await _context.SaveChangesAsync();

				return RedirectToAction("Rooms"); // Redirect to a page displaying the list of rooms after deletion.
			}
			catch (Exception ex)
			{
				// Handle the exception as needed and optionally redirect to an error view.
				return View("ErrorView");
			}
		}


            [HttpGet]
		public async Task<IActionResult> UpdateDepartment(int? id)
		{
			if (id == null || _context.Departments == null)
			{
				return NotFound();
			}

			var department = await _context.Departments.FindAsync(id);
			if (department == null)
			{
				return NotFound();
			}
			return View(department);
		}


		[HttpPost]
		public async Task<IActionResult> UpdateDepartment(int id, OutDepartmentDTO updateDepartmentDTO, IFormFile? file)
		{
			if (file != null)
			{
				await _department.GetFile2(file, updateDepartmentDTO);

			}

			await _department.UpdateDepartment(id, updateDepartmentDTO);
			return RedirectToAction("UpdateDepartment", "Dashboards");
		}


		[HttpGet]
		public async Task<IActionResult> GetDepartments()
		{
			try
			{
				var departments = await _department.GetDepartments();

				if (departments == null || departments.Count == 0)
				{
					ViewData["ErrorMessage"] = "No departments found.";
					return View();
				}

				return View(departments);
			}
			catch (Exception ex)
			{
				ViewData["ErrorMessage"] = $"An error occurred while retrieving departments: {ex.Message}";
				return View();
			}
		}

		[HttpGet]
		public async Task<IActionResult> DeleteDepartment(int id)
		{
			var department = await _context.Departments.FindAsync(id);

			if (department == null)
			{
				// Handle case where department is not found
				return NotFound();
			}

			return View(department);
		}


		[HttpPost, ActionName("DeleteDepartment")]
		public async Task<IActionResult> ConfirmDelete(int id)
		{
			var department = await _context.Departments.FindAsync(id);

			if (department == null)
			{
				// Handle case where department is not found
				return NotFound();
			}

			_context.Departments.Remove(department);
			await _context.SaveChangesAsync();

			return RedirectToAction("GetDepartments");
		}


		[HttpGet]
		public async Task<IActionResult> GetDepartment(int id)
		{
			var department = await _department.GetDepartment(id);

			if (department == null)
			{
				return NotFound();
			}

			return View(department);
		}

		[HttpGet]
		public async Task<IActionResult> GetDoctorsInDepartment(int id)
		{
			var doctors = await _department.GetDoctorsInDepartment(id);
			if (doctors == null)
			{
				return NotFound();
			}
			return View(doctors);
		}


		[HttpGet]
		public async Task<IActionResult> GetNursesInDepartment(int id)
		{
			var nurses = await _department.GetNursesInDepartment(id);
			if (nurses == null)
			{
				return NotFound();
			}
			return View(nurses);
		}


        [HttpGet]
        public async Task<IActionResult> GetRoomsAndPatientsInDepartment(int departmentId)
        {
            // Fetch patients and rooms for the specified departmentId
            var roomsAndPatients = await _department.GetRoomsAndPatientsInDepartment(departmentId);

            if (roomsAndPatients == null)
            {
                return NotFound();
            }

            return View(roomsAndPatients);
        }


        [HttpGet]
		public async Task<IActionResult> GetRoomsInDepartment(int id)
		{
			var rooms = await _department.GetRoomsInDepartment(id);
			if (rooms == null)
			{
				return NotFound();
			}
			return View(rooms);
		}


        [HttpGet]
        public async Task<IActionResult> ViewAppointments()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound();
            }
            var appointments = await _appointment.GetAppointmentsForDoctor(userId);
            return View(appointments);
        }
        [HttpPost]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            // Retrieve the appointment by ID
            var appointment = await _appointment.GetAppointmentById(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }
            if (!appointment.IsAvailable)
            {
                appointment.IsAvailable = true;
                var appointmentDto = new AppointmentDTO
                {
                    Id = appointment.Id,
                    IsAvailable = appointment.IsAvailable,
                    PatientId = appointment.PatientId,
                    DoctorId = appointment.DoctorId,
                    AppointmentSlotId = appointment.AppointmentSlotId,
                };
                await _appointment.UpdateAppointment(appointmentId, appointmentDto);
            }
            return RedirectToAction("ViewAppointments", new { doctorId = appointment.DoctorId });
        }


        [HttpGet]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _room.GetRoom2(id);
            if (room == null)
            {
                return NotFound(); // Room not found
            }

            return View(room); // Assuming you have a view to display room details
        }

        [HttpGet]
        public async Task<IActionResult> EditPatient(int id)
        {
            var patientDTO = await _patient.GetPatient(id); // Replace with your logic to retrieve the DTO
            if (patientDTO == null)
            {
                return NotFound();
            }

            return View(patientDTO); // Assuming you have an EditPatient view for displaying and editing patient details.
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPatient(int id, InPatientDTO patientDTO)
        {
            if (id != patientDTO.Id) // Replace 'Id' with the actual property name for the patient's identifier.
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedPatient = await UpdatePatient(id, patientDTO);

                    if (updatedPatient != null)
                    {
                        return RedirectToAction("PatientDetails", new { id = updatedPatient.Id });
                    }
                    else
                    {
                        return View("ErrorView"); // Redirect to an error view
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception as needed, and optionally redirect to an error view.
                    return View("ErrorView");
                }
            }

            return View(patientDTO);
        }

        // This is your original service method modified to be used within the controller.
        public async Task<OutPatientDTO> UpdatePatient(int id, InPatientDTO patientDTO)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                // Your existing logic for updating the patient.
            }
            return null; // Or the updated patient details
        }


        [HttpGet]
        public async Task<IActionResult> AllPatients()
        {
            var patients = await _patient.GetPatients();
            return View(patients);
        }



        [HttpPost]
        public  IActionResult AllPatients(string patientname)
        {
            HttpContext.Session.SetString("patientname", patientname);
            return RedirectToAction("PatientSearch");
        }


        [HttpGet]
        public async Task<IActionResult> PatientSearch(string patientname)
        {
            var rows = _context.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(patientname))
            {
                rows = rows.Where(x => x.FirstName.Contains(patientname));
            }

            return View(await rows.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> DeleteNurse(int id)
        {
            var nurse = await _context.Nurses.FindAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == nurse.UserId);
            if (user != null)
            {
                // Remove the user
                _context.Users.Remove(user);
            }
            // Remove the nurse
            _context.Nurses.Remove(nurse);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetNursesInDepartment");
        }
        [HttpGet]
        public async Task<IActionResult> EditNurse(int id)
        {
            var nurse = await _nurse.GetNurseDTO(id);
            if (nurse == null)
            {
                return NotFound();
            }
            return View(nurse);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateNurse(int Id, InNurseDTO nurse)
        {
            if (ModelState.IsValid)
            {
                await _nurse.UpdateNurse(Id, nurse);
                TempData["success"] = "Nurse has been updated successfully !";
            }
            else
            {
                TempData["Fail"] = "Something went wrong!";
            }
            return View("EditNurse", nurse);
        }

    }
}
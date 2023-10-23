using Hospital_System.Auth.Models.Interface;
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Hospital;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.DTOs.Room;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.ViewModels;
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
		private readonly IDoctor _doctor;
        private readonly IMedicalReport _medicalReport;
        private readonly IUser _user;
        private readonly IMedicine _medicine;



        private readonly HospitalDbContext _context;

		public DashboardsController(IDepartment department, IHospital hospital, HospitalDbContext context, IAppointment appointment,IPatient patient,IRoom room,INurse nurse,IDoctor doctor,IMedicalReport medicalReport,IUser user,IMedicine medicine)
		{
			_department = department;
			_hospital = hospital;
			_context = context;
			_appointment = appointment;
			_patient = patient;
			_room = room;
            _nurse = nurse;
			_doctor = doctor;
            _medicalReport = medicalReport;
            _user = user;
            _medicine = medicine;
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

            var dep = await _department.GetDepartment(id);
            TempData["DepartmentName"] = dep.DepartmentName;

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
			var dep = await _department.GetDepartment(id);
			TempData["DepartmentName"] = dep.DepartmentName;

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
                    var updatedPatient = await _patient.UpdatePatient(id, patientDTO);

                    if (updatedPatient != null)
                    {
                        //return RedirectToAction("PatientDetails", new { id = updatedPatient.Id });
                        return RedirectToAction(nameof(AllPatients));
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


        [HttpGet]
        public async Task<IActionResult> EditRoom(int id)
        {
            var roomDTO = await _room.GetRoom(id); // Replace with your logic to retrieve the DTO
            if (roomDTO == null)
            {
                return NotFound();
            }

            return View(roomDTO); // Assuming you have an Editroom view for displaying and editing room details.
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoom(int id, OutRoomDTO roomDTO)
        {
            if (id != roomDTO.Id) // Replace 'Id' with the actual property name for the room's identifier.
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedRoom = await _room.UpdateRoom(id, roomDTO);

                    if (updatedRoom != null)
                    {
                        //return RedirectToAction("roomDetails", new { id = updatedroom.Id });
                        return RedirectToAction(nameof(AllRooms));
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

            return View(roomDTO);
        }



        [HttpGet]
        public IActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(OutRoomDTO room)
        {
            await _room.CreateRoom(room);

            return View(room);
        }



        [HttpGet]
        public async Task<IActionResult> AllRooms()
        {
            try
            {
                var rooms = await _room.GetRooms();

                if (rooms == null || rooms.Count == 0)
                {
                    ViewData["ErrorMessage"] = "No room found.";
                    return View();
                }

                return View(rooms);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"An error occurred while retrieving room: {ex.Message}";
                return View();
            }
        }








        //// This is your original service method modified to be used within the controller.
        //public async Task<OutPatientDTO> UpdatePatient(int id, InPatientDTO patientDTO)
        //{
        //    var patient = await _context.Patients.FindAsync(id);
        //    if (patient != null)
        //    {
        //        // Your existing logic for updating the patient.
        //    }
        //    return null; // Or the updated patient details
        //}


        [HttpGet]
        public async Task<IActionResult> AllPatients(int pageIndex = 1)
        {
            int pageSize = 6;

            var patients = await _patient.GetPatients();

            var paginatedpatients = patients.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            // Pass the list of available time slots to the view
            TempData["PageIndex"] = pageIndex;
            TempData["TotalPages"] = (int)Math.Ceiling((double)patients.Count() / pageSize);

            return View(paginatedpatients);
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
            int depId = nurse.DepartmentId;

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == nurse.UserId);
            if (user != null)
            {
                // Remove the user
                _context.Users.Remove(user);
            }
            // Remove the nurse
            _context.Nurses.Remove(nurse);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetNursesInDepartment", new {id= depId });
        }
        [HttpGet]
        public async Task<IActionResult> EditNurse(int id)
        {
            var nurse = await _nurse.GetNurseDTO(id);
            if (nurse == null)
            {
                return NotFound();
            }
            ViewBag.Depatments = await _department.GetDepartmentsDto();

            return View(nurse);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateNurse(int Id, InNurseDTO nurse)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Depatments = await _department.GetDepartmentsDto();

                return View("EditNurse", nurse);

               
            }
			try
            {
                await _nurse.UpdateNurse(Id, nurse);
                TempData["success"] = "Nurse has been updated successfully !";
            }
            catch (Exception ex)
            {
                TempData["Fail"] = ex.Message;
            }
            ViewBag.Depatments = await _department.GetDepartmentsDto();
            return View("EditNurse", nurse);
        }
		[HttpGet]
		public async Task<IActionResult> EditDoctor(int id)
		{
			var doctorDto = await _doctor.GetDoctorView(id);
			if (doctorDto == null)
			{
				return NotFound();
			}
			ViewBag.Depatments = await _department.GetDepartmentsDto();
			return View(doctorDto);
		}

		[HttpPost]
		public async Task<IActionResult> EditDoctor(int Id, InDoctorDTO doctorDto)
		{
			if (!ModelState.IsValid)
			{
                ViewBag.Depatments = await _department.GetDepartmentsDto();

                return View("EditDoctor", doctorDto);

			}
			try
			{
				await _doctor.UpdateDoctor(Id, doctorDto);
				TempData["success"] = "Doctor has been updated successfully !";
				
			}
			catch (Exception ex) 
			{
				TempData["Fail"] = ex.Message;
			}
            ViewBag.Depatments = await _department.GetDepartmentsDto();

            return View("EditDoctor", doctorDto);
		}
		[HttpGet]
		public async Task<IActionResult> DeleteDoctor(int id)
		{
			var doctor = await _context.Doctors.FindAsync(id);
			if (doctor == null)
			{
				return NotFound();
			}
			int depId = doctor.DepartmentId;

			var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == doctor.UserId);
			if (user != null)
			{
				// Remove the user
				_context.Users.Remove(user);
			}
			// Remove the nurse
			_context.Doctors.Remove(doctor);
			await _context.SaveChangesAsync();
			return RedirectToAction("GetDoctorsInDepartment", new { id = depId });
		}



        [HttpGet]
        [ActionName("DeleteMedicalReport")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var medicalReport = await _medicalReport.GetMedicalReport(id);
            return View(medicalReport);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteMedicalReport(int id)
        {
            var medicalReport = await _medicalReport.GetMedicalReport(id);
            var patientId = await _context.Patients.FirstOrDefaultAsync(x => x.Id == medicalReport.PatientId);
            await _medicalReport.DeleteMedicalReport(id);


            return RedirectToAction("PatientMedicalReport", "Auth", patientId);
        }


        //[HttpGet]
        //public async Task<IActionResult> AddMedicalReport(int patientId)
        //{
        //    var doctorId = await _user.GetCurrentLoggedInDoctorId();

        //    if (!int.TryParse(doctorId, out int doctorIdInt))
        //    {
        //        // Handle the case when the doctor ID cannot be parsed as an integer.
        //        return RedirectToAction("Error");
        //    }

        //    var patient = await _context.Patients.FindAsync(patientId);

        //    if (patient == null)
        //    {
        //        // Handle the case when the patient is not found, for example, redirect to an error page.
        //        return RedirectToAction("Error");
        //    }

        //    var newMedicalReport = new InMedicalReportDTO
        //    {
        //        PatientId = patientId, // Set the patientId in the DTO
        //        DoctorId = doctorIdInt, // Set the DoctorId in the DTO as an int
        //                                // Set any other properties you want to pre-populate in the form
        //    };

        //    return View(newMedicalReport);
        //}

        //[HttpGet]
        //public async Task<IActionResult> AddMedicalReport(int patientId)
        //{
        //    // Retrieve patient information or perform any necessary operations.
        //    var patient = await _context.Patients.FindAsync(patientId);

        //    if (patient == null)
        //    {
        //        // Handle the case when the patient is not found, for example, redirect to an error page.
        //        return RedirectToAction("Error");
        //    }

        //    // Initialize a new InMedicalReportDTO with any required data, and pass it to the view.
        //    var newMedicalReport = new InMedicalReportDTO
        //    {
        //        PatientId = patientId, // Set the patientId in the DTO
        //                  // Set any other properties you want to pre-populate in the form
        //    };

        //    return View(newMedicalReport);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddMedicalReport(InMedicalReportDTO newMedicalReportDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var createdMedicalReport = await _medicalReport.CreateMedicalReport(newMedicalReportDTO);

        //            if (createdMedicalReport != null)
        //            {
        //                // Optionally, you can redirect to a page that shows the newly created medical report.
        //                return RedirectToAction("GetRoomsAndPatientsInDepartment", new { id = createdMedicalReport.Id });
        //            }
        //            else
        //            {
        //                // Handle the case when the medical report creation fails.
        //                return RedirectToAction("Error");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle any exceptions that occur during the creation of the medical report.
        //            return RedirectToAction("Error");
        //        }
        //    }

        //    // If ModelState is not valid, return to the same view with validation errors.
        //    return View(newMedicalReportDTO);
        //}

        public async Task<IActionResult> AddMedicalReport(int patientId)
        {
            ViewBag.Mediciens = await _medicine.GetMedicines();

            var model = new MedicalReportViewModel
            {
                PatientId = patientId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicalReport(MedicalReportViewModel reportViewModel, int PatientId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Mediciens = await _medicine.GetMedicines();

                return View("AddMedicalReport", reportViewModel);
            }
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int doctorId = await _doctor.GetDoctorId(userId);

                InMedicalReportDTO inRepor = new InMedicalReportDTO
                {
                    PatientId = reportViewModel.PatientId,
                    DoctorId = doctorId,
                    Description = reportViewModel.Description,
                    ReportDate = reportViewModel.ReportDate,
                };
                var added = await _medicalReport.CreateMedicalReport(inRepor);
                if (reportViewModel.Medicines != null && reportViewModel.Medicines.Count > 0)
                {
                    foreach (var medicine in reportViewModel.Medicines)
                    {
                        await _medicalReport.AddMedicineToReport(added.Id, medicine.MedicineId, medicine.TimesInDay, medicine.MedicinePortion);
                    }
                }

                TempData["success"] = "report added successfully";
                return RedirectToAction("ViewAppointments");
            }
            catch (Exception ex)
            {
                TempData["fail"] = ex.Message;
                return RedirectToAction("ViewAppointments");
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> AddMedicalReport(int patientId)
        //{
        //    if (!ModelState.IsValid)
        //    {

        //    }
        //    return View();
        //}

    }
}
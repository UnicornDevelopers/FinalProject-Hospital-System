using Hospital_System.Data;
using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.DTOs.AppointmentSlot;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Models.Services
{
	public class AppointmentSlotService : IAppointmentSlot
	{
		private readonly HospitalDbContext _context;

		public AppointmentSlotService(HospitalDbContext context)
		{
			_context = context;
		}
		public async Task<List<TimeSlotViewDto>> GetTimeSlotView(int doctorId)
		{
			var today = DateTime.Today;

			var startDate = today;
			var endDate = today.AddDays(5);

			var startHour = new TimeSpan(9, 0, 0); // 9:00 AM
			var endHour = new TimeSpan(15, 0, 0); // 3:00 PM

			// Create a list to store the available time slots
			var availableSlots = new List<TimeSlotViewDto>();

			for (var date = startDate; date <= endDate; date = date.AddDays(1))
			{
				for (var hour = startHour; hour < endHour; hour = hour.Add(TimeSpan.FromHours(1)))
				{
					// Check if there is an AppointmentSlot for this doctor at this date and hour
					var slot = await _context.AppointmentSlots
						.Include(s => s.Appointments)
						.Where(s => s.DoctorId == doctorId && s.DateOfSlot == date && s.SlotHour == hour)
						.FirstOrDefaultAsync();

					if (slot == null || (slot != null && slot.Appointments.Count < 3))
					{
						availableSlots.Add(new TimeSlotViewDto
						{
							DateView = date,
							HourView = hour,
							DoctorId = doctorId
						});
					}
				}
			}

			return availableSlots;
		}

		public async Task AddAppointment(TimeSlotViewDto timeSlot, string UserId)
		{
			var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == UserId);
			if (patient == null)
			{
				throw new Exception("Patient not found");
			}
			int patientId = patient.Id;
			// Check if the patient already has an ongoing appointment with this doctor
			var ongoingAppointment = await _context.Appointments
				.Where(a => a.PatientId == patientId && a.DoctorId == timeSlot.DoctorId && !a.IsAvailable)
				.FirstOrDefaultAsync();

			if (ongoingAppointment != null)
			{

				throw new Exception("The patient already has an ongoing appointment with this doctor.");
			}

			// Check if the AppointmentSlot already exists
			var slot = await _context.AppointmentSlots
				.Include(s => s.Appointments)
				.Where(s => s.DoctorId == timeSlot.DoctorId && s.DateOfSlot == timeSlot.DateView && s.SlotHour == timeSlot.HourView)
				.FirstOrDefaultAsync();

			if (slot == null)
			{
				// If the AppointmentSlot does not exist, create it
				slot = new AppointmentSlot
				{
					DoctorId = timeSlot.DoctorId,
					DateOfSlot = timeSlot.DateView,
					SlotHour = timeSlot.HourView,
					Appointments = new List<Appointment>()
				};

				_context.AppointmentSlots.Add(slot);
				await _context.SaveChangesAsync();
			}

			// Create a new Appointment and add it to the slot
			var appointment = new Appointment
			{
				DoctorId = timeSlot.DoctorId,
				PatientId = patientId,
				AppointmentSlotId = slot.Id,
				IsAvailable = false
			};

			slot.Appointments.Add(appointment);
			await _context.SaveChangesAsync();
		}

		public async Task CancelAppointmentOfPatient(int appointmentId)//check it is is same as delete appointmen
		{
			// Find the appointment with the given id in the given slot
			var appointment = await _context.Appointments
				.Where(a => a.Id == appointmentId)
				.FirstOrDefaultAsync();

			if (appointment == null)
			{
				throw new Exception("The appointment does not exist.");
			}

			_context.Appointments.Remove(appointment);

			await _context.SaveChangesAsync();
		}
		// or this where
		// .Where(a => a.AppointmentSlotId == appointmentSlotId && a.Id == appointmentId)


		public async Task<List<AppointmentSlotDto>> GetAppointmentSlotByDoctorId(int doctorId)
		{
			// Get today's date
			var today = DateTime.Today;

			// Calculate the start and end dates for the time slots
			var startDate = today;
			var endDate = today.AddDays(5);

			// Specify the start and end hours for the time slots
			var startHour = new TimeSpan(9, 0, 0); // 9:00 AM
			var endHour = new TimeSpan(15, 0, 0); // 3:00 PM

			// Find the AppointmentSlots for this doctor between startDate and endDate and between startHour and endHour
			var slots = await _context.AppointmentSlots
				.Include(s => s.Appointments)
				.Where(s => s.DoctorId == doctorId && s.DateOfSlot >= startDate && s.DateOfSlot <= endDate && s.SlotHour >= startHour && s.SlotHour <= endHour)
				.ToListAsync();

			if (slots == null || slots.Count == 0)
			{
				throw new Exception("No appointment slots found for this doctor.");
			}

			// Convert each AppointmentSlot to an AppointmentSlotDto
			var slotDtos = slots.Select(slot => new AppointmentSlotDto
			{
				Id = slot.Id,
				DateOfSlot = slot.DateOfSlot,
				SlotHour = slot.SlotHour,
				DoctorId = slot.DoctorId,
				Appointments = slot.Appointments.Where(a => a.IsAvailable).Select(a => new AppointmentDTO
				{
					Id = a.Id,
					IsAvailable = a.IsAvailable,
					PatientId = a.PatientId,
					DoctorId = a.DoctorId,
					AppointmentSlotId = a.AppointmentSlotId
				}).ToList()
			}).ToList();

			return slotDtos;
		}

	}
}
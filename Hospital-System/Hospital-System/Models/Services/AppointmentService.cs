using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
namespace Hospital_System.Models.Services
{
	/// <summary>
	/// Provides functionality to manage appointments within the hospital system.
	/// </summary>
	public class AppointmentService : IAppointment
	{
		private readonly HospitalDbContext _context;

		public AppointmentService(HospitalDbContext context)
		{
			_context = context;
		}

		public async Task<List<OutAppointmentDTO>> GetAppointments()
		{
			var appointments = _context.Appointments.Select(a => new OutAppointmentDTO
			{
				Id = a.Id,
				IsAvailable = a.IsAvailable,
				PatientId = a.PatientId,
				DoctorId = a.DoctorId,
				AppointmentSlotId = a.AppointmentSlotId,
			}).ToList();

			return appointments;
		}

		public async Task<OutAppointmentDTO> GetAppointmentById(int id)
		{
			var appointment = _context.Appointments
				.Where(a => a.Id == id)
				.Select(a => new OutAppointmentDTO
				{
					Id = a.Id,
					IsAvailable = a.IsAvailable,
					PatientId = a.PatientId,
					DoctorId = a.DoctorId,
					AppointmentSlotId = a.AppointmentSlotId,
				})
				.FirstOrDefault();

			return appointment;
		}

		public async Task<OutAppointmentDTO> CreateAppointment(AppointmentDTO appointmentDto)
		{
			var appointment = new Appointment
			{
				IsAvailable = appointmentDto.IsAvailable,
				PatientId = appointmentDto.PatientId,
				DoctorId = appointmentDto.DoctorId,
				AppointmentSlotId = appointmentDto.AppointmentSlotId,
			};

			_context.Appointments.Add(appointment);
			await _context.SaveChangesAsync();

			appointmentDto.Id = appointment.Id;

			var outAppointemntdto = new OutAppointmentDTO
			{
				IsAvailable = appointment.IsAvailable,
				PatientId = appointment.PatientId,
				DoctorId = appointment.DoctorId,
				AppointmentSlotId = appointment.AppointmentSlotId,

			};



			return outAppointemntdto;
		}

		public async Task<OutAppointmentDTO> UpdateAppointment(int id, AppointmentDTO appointmentDto)
		{
			var existingAppointment = _context.Appointments.FirstOrDefault(a => a.Id == id);

			if (existingAppointment == null)
				return null;

			existingAppointment.IsAvailable = appointmentDto.IsAvailable;
			existingAppointment.PatientId = appointmentDto.PatientId;
			existingAppointment.DoctorId = appointmentDto.DoctorId;
			existingAppointment.AppointmentSlotId = appointmentDto.AppointmentSlotId;

			await _context.SaveChangesAsync();
			return new OutAppointmentDTO
			{
				Id = existingAppointment.Id,
				IsAvailable = existingAppointment.IsAvailable,
				PatientId = existingAppointment.PatientId,
				DoctorId = existingAppointment.DoctorId,
				AppointmentSlotId = existingAppointment.AppointmentSlotId,
			};
		}

		public async Task<bool> DeleteAppointmentAsync(int id)
		{
			var existingAppointment = _context.Appointments.FirstOrDefault(a => a.Id == id);

			if (existingAppointment == null)
				return false;

			_context.Appointments.Remove(existingAppointment);
			await _context.SaveChangesAsync();
			return true;
		}


        public async Task<List<Appointment>> GetAppointmentsForPatient(string userId)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
            if (patient == null)
            {
                throw new Exception("Patient not found");
            }
            int patientId = patient.Id;
            var appointments = await _context.Appointments
            .Where(appointment => appointment.PatientId == patientId)
            .Include(appointment => appointment.appointmentSlot) // related AppointmentSlot
            .Include(appointment => appointment.doctor)
            .ToListAsync();
            return appointments;
        }
        public async Task<List<Appointment>> GetAppointmentsForDoctor(string userId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(p => p.UserId == userId);
            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }
            int doctorId = doctor.Id;
            var appointments = await _context.Appointments
            .Where(appointment => appointment.DoctorId == doctorId)
            .Include(appointment => appointment.appointmentSlot) // related AppointmentSlot
            .Include(appointment => appointment.patient)
            .ToListAsync();
            return appointments;
        }



    }
}
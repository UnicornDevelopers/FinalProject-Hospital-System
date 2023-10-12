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
        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentService"/> class.
        /// </summary>
        /// <param name="context">The hospital database context.</param>
        public AppointmentService(HospitalDbContext context)
        {
            _context = context;
        }
        // CREATE Appointment........................................................................
        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="newAppointmentDTO">The appointment details to create.</param>
        /// <returns>The created appointment.</returns>
        public async Task<OutAppointmentDTO> CreateAppointment(InAppoinmentDTO newAppointmentDTO)
        {
            // Fetch additional information and check it's existing
            var patientEntity = await _context.Patients.FindAsync(newAppointmentDTO.PatientId);
            var doctorEntity = await _context.Doctors
                .Include(d => d.department)
                .FirstOrDefaultAsync(d => d.Id == newAppointmentDTO.DoctorId);
            if (doctorEntity != null && patientEntity != null)
            {
                Appointment appointment = new Appointment
                {
                    DateOfAppointment = newAppointmentDTO.DateOfAppointment,
                    PatientId = newAppointmentDTO.PatientId,
                    DoctorId = newAppointmentDTO.DoctorId
                };
                _context.Entry(appointment).State = EntityState.Added;
                await _context.SaveChangesAsync();
                var outAppointmentDTO = new OutAppointmentDTO
                {
                    Id = appointment.Id,
                    DateOfAppointment = appointment.DateOfAppointment,
                    PatientId = appointment.PatientId,
                    PatientName = $"{patientEntity.FirstName} {patientEntity.LastName}",
                    DoctorId = appointment.DoctorId,
                    DoctorName = $"{doctorEntity.FirstName} {doctorEntity.LastName}",
                    DepartmentName = doctorEntity.department.DepartmentName // Access DepartmentName property
                };
                return outAppointmentDTO;
            }
            else
            {
                throw new InvalidOperationException("Patient id or Doctor Id is wrong");
            }
        }



        // Get Appointments........................................................................
        /// <summary>
        /// Retrieves a list of all appointments.
        /// </summary>
        /// <returns>A list of appointments.</returns>
        public async Task<List<OutAppointmentDTO>> GetAppointments()
        {
            var appointments = await _context.Appointments
                .Include(a => a.patient)
                .Include(a => a.doctor.department)
                .Select(x => new OutAppointmentDTO()
                {
                    Id = x.Id,
                    DateOfAppointment = x.DateOfAppointment,
                    PatientId = x.PatientId,
                    PatientName = $"{x.patient.FirstName} {x.patient.LastName}",
                    DoctorId = x.DoctorId,
                    DoctorName = $"{x.doctor.FirstName} {x.doctor.LastName}",
                    DepartmentName = x.doctor.department.DepartmentName
                }).ToListAsync();
            return appointments;
        }

        // Get Appointment by ID........................................................................
        /// <summary>
        /// Retrieves the details of an appointment by its ID.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        /// <returns>The details of the appointment.</returns>
        public async Task<OutAppointmentDTO> GetAppointment(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.doctor.department)
                .Select(x => new OutAppointmentDTO()
                {
                    Id = x.Id,
                    DateOfAppointment = x.DateOfAppointment,
                    PatientId = x.PatientId,
                    DoctorId = x.DoctorId,
                    PatientName = $"{x.patient.FirstName} {x.patient.LastName}",
                    DoctorName = $"{x.doctor.FirstName} {x.doctor.LastName}",
                    DepartmentName = x.doctor.department.DepartmentName,
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            return appointment;
        }


        // Update Appointment by ID........................................................................
        /// <summary>
        /// Updates the details of an existing appointment.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        /// <param name="updateAppointmentDTO">The updated appointment details.</param>
        /// <returns>The updated appointment details.</returns>
        public async Task<InAppoinmentDTO> UpdateAppointment(int id, InAppoinmentDTO updateAppointmentDTO)
        {
            var existingAppointment = await _context.Appointments.FindAsync(id);

            if (existingAppointment == null)
            {
                throw new ArgumentException($"Appointment with ID {id} not found.");
            }

            var patientExists = await _context.Patients.AnyAsync(p => p.Id == updateAppointmentDTO.PatientId);
            if (!patientExists)
            {
                throw new ArgumentException($"Patient with ID {updateAppointmentDTO.PatientId} not found.");
            }

            var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == updateAppointmentDTO.DoctorId);
            if (!doctorExists)
            {
                throw new ArgumentException($"Doctor with ID {updateAppointmentDTO.DoctorId} not found.");
            }

            // Update the appointment properties
            existingAppointment.DateOfAppointment = updateAppointmentDTO.DateOfAppointment;
            existingAppointment.PatientId = updateAppointmentDTO.PatientId;
            existingAppointment.DoctorId = updateAppointmentDTO.DoctorId;

            await _context.SaveChangesAsync();

            return updateAppointmentDTO;
        }
        // Delete Appointment by ID........................................................................
        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        public async Task DeleteAppointment(int id)
        {
            Appointment appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Entry(appointment).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"The Appointment Id {id} does not exist.");
            }
        }

    }
}
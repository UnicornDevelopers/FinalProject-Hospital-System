
using Hospital_System.Data;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.DTOs.AppointmentDTO;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Hospital_System.Models.Services
{
    /// <summary>
    /// Service class for managing doctors within the system.
    /// </summary>
    public class DoctorService : IDoctor

    {
        private readonly HospitalDbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DoctorService(HospitalDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates a new doctor in the system.
        /// </summary>
        /// <param name="doctor">The doctor information to create.</param>
        /// <returns>The created doctor information.</returns>
        public async Task<OutDocDTO> Create(InDoctorDTO doctor)
        {
            var existingDep = await _context.Departments.FindAsync(doctor.DepartmentId);

            if (existingDep != null)
            {
                Doctor doctorEntity = new Doctor()
                {
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Gender = doctor.Gender,
                    ContactNumber = doctor.ContactNumber,
                    Speciality = doctor.Speciality,
                    DepartmentId = doctor.DepartmentId
                };

                _context.Doctors.Add(doctorEntity);
                await _context.SaveChangesAsync();

                OutDocDTO outputDoc = new OutDocDTO()
                {
                    Id = doctorEntity.Id,
                    FullName = $"{doctor.FirstName} {doctor.LastName}",
                    Gender = doctorEntity.Gender,
                    ContactNumber = doctor.ContactNumber,
                    Speciality = doctor.Speciality,
                    DepartmentName = existingDep.DepartmentName
                };

                return outputDoc;
            }
            else
            {
                throw new InvalidOperationException($"Department with ID {doctor.DepartmentId} not found.");
            }
        }

        /// <summary>
        /// Deletes a Doctor from the system.
        /// </summary>
        /// <param name="id">The ID of the doctor to delete.</param>
        public async Task Delete(int id)
        {
            Doctor existingDoc = await _context.Doctors.FindAsync(id);
            if (existingDoc != null)
            {
                _context.Entry(existingDoc).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }

            else
            {
                throw new InvalidOperationException("The Doctor Id is not exist.");
            }
        }

        /// <summary>
        /// Retrieves information about a specific Doctor.
        /// </summary>
        /// <param name="id">The ID of the Doctor to retrieve.</param>
        /// <returns>The doctor information.</returns>
        public async Task<DoctorDTO> GetDoctor(int DoctorID)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Appointments)
                .Include(d => d.medicalReports)
                .Include(d => d.department)
                .FirstOrDefaultAsync(d => d.Id == DoctorID);

            if (doctor == null)
            {
                throw new InvalidOperationException($"Doctor with ID {DoctorID} not found.");
            }

            var doctorDTO = new DoctorDTO
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Gender = doctor.Gender,
                ContactNumber = doctor.ContactNumber,
                Speciality = doctor.Speciality,
                DepartmentId = doctor.DepartmentId,
                Appointments = doctor.Appointments?.Select(a => new GetAppointmentDTO
                {
                    Id = a.Id,
                    DateOfAppointment = a.DateOfAppointment,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    
                }).ToList(),
                medicalReports = doctor.medicalReports?.Select(r => new InMedicalReportDTO
                {
                    Id = r.Id,
                    ReportDate = r.ReportDate,
                    Description = r.Description,
                    PatientId = r.PatientId,
                    DoctorId = r.DoctorId,
                }).ToList(),
                department = new OutDepartmentDTO
                {
                    Id = doctor.department.Id,
                    DepartmentName = doctor.department.DepartmentName,
                }
            };

            return doctorDTO;
        }


        /// <summary>
        /// Retrieves information for all Doctors 
        /// </summary>
        public async Task<List<OutDocDTO>> GetDoctors()
        {

            var doctors = await _context.Doctors
                .Include(d => d.department)
                .Select(d => new OutDocDTO
                {
                    Id = d.Id,
                    FullName = $"{d.FirstName} {d.LastName}",
                    Gender = d.Gender,
                    ContactNumber = d.ContactNumber,
                    Speciality = d.Speciality,
                    DepartmentName = d.department.DepartmentName
                })
                .ToListAsync();

            return doctors;


        }

        /// <summary>
        /// Updates the information of a specific doctor.
        /// </summary>
        /// <param name="id">The ID of the doctor to update.</param>
        /// <param name="doctorDTO">The updated doctor information.</param>
        /// <returns>The updated doctor information.</returns>
        public async Task<InDoctorDTO> UpdateDoctor(int id, InDoctorDTO doctorDTO)
        {
            var existingDoctor = await _context.Doctors.FindAsync(id);

            if (existingDoctor == null)
            {
                throw new InvalidOperationException($"Doctor with ID {id} not found.");
            }
            var existingDep = await _context.Departments.FindAsync(doctorDTO.DepartmentId);

            if (existingDep == null)
            {
                throw new InvalidOperationException($"invaild Department with ID {doctorDTO.DepartmentId} not found.");
            }

            existingDoctor.FirstName = doctorDTO.FirstName;
            existingDoctor.LastName = doctorDTO.LastName;
            existingDoctor.Gender = doctorDTO.Gender;
            existingDoctor.ContactNumber = doctorDTO.ContactNumber;
            existingDoctor.Speciality = doctorDTO.Speciality;
            existingDoctor.DepartmentId = doctorDTO.DepartmentId;

            await _context.SaveChangesAsync();

            var updatedDoctorDTO = new InDoctorDTO
            {
                Id = existingDoctor.Id,
                FirstName = existingDoctor.FirstName,
                LastName = existingDoctor.LastName,
                Gender = existingDoctor.Gender,
                ContactNumber = existingDoctor.ContactNumber,
                Speciality = existingDoctor.Speciality,
                DepartmentId = existingDoctor.DepartmentId,
            };

            return updatedDoctorDTO;
        }


        /// <summary>
        /// Retrieves the list of Appointments for a specific Doctor.
        /// </summary>
        /// <param name="doctorId">The ID of the department.</param>
        /// <returns>The list of doctors in the department.</returns>
        public async Task<List<DoctorAppointmentsDTO>> GetAppointmentsForDoctor(int doctorId)
        {
            var Appointments = await _context.Appointments
                .Where(d => d.DoctorId == doctorId)
                .Select(d => new DoctorAppointmentsDTO()
                {
                    Id = d.Id,
                   DateOfAppointment = d.DateOfAppointment,
                   PatientId = d.PatientId,
                  
                })
                .ToListAsync();

            return Appointments;
        }


    }
}


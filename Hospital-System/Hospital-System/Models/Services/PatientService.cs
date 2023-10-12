using System;
using System.Linq;
using System.Threading.Tasks;
using Hospital_System.Data;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.AppointmentDTO;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.DTOs.Room;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Models.Services
{
    /// <summary>
    /// Service class for managing patients within the hospital system.
    /// </summary>
    public class PatientService : IPatient
    {
        private readonly HospitalDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PatientService(HospitalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new patient in the system.
        /// </summary>
        /// <param name="Patient">The data transfer object containing patient information.</param>
        /// <returns>The created patient information.</returns>
        public async Task<OutPatientDTO> Create(InPatientDTO Patient)
        {
            if (Patient.RoomId != null)
            {
                var room = await _context.Rooms.Include(p => p.Patients).FirstOrDefaultAsync(a => a.Id == Patient.RoomId);
                if (room == null)
                {
                    throw new Exception("Room does not exist.");
                }
                if (room.NumberOfBeds <= room.Patients.Count)
                {
                    room.RoomAvailability = false;
                }
                else
                {
                    room.RoomAvailability = true;
                }
                var dep = await _context.Departments.FindAsync(room.DepartmentId);
                if (room.RoomAvailability)
                {
                    var patient = new Patient
                    {
                        FirstName = Patient.FirstName,
                        LastName = Patient.LastName,
                        DoB = Patient.DoB,
                        Gender = Patient.Gender,
                        ContactNumber = Patient.ContactNumber,
                        Address = Patient.Address,
                        RoomId = Patient.RoomId
                    };
                    _context.Patients.Add(patient);
                    await _context.SaveChangesAsync();
                    var patientDTO = new OutPatientDTO
                    {
                        Id = patient.Id,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        DoB = patient.DoB,
                        Gender = patient.Gender,
                        ContactNumber = patient.ContactNumber,
                        Address = patient.Address,
                        RoomId = patient.RoomId,
                        DepartmentName = dep!.DepartmentName,
                        RoomNumber = room.RoomNumber
                    };
                    return patientDTO;
                }
            }
            else
            {
                var patient = new Patient
                {
                    FirstName = Patient.FirstName,
                    LastName = Patient.LastName,
                    DoB = Patient.DoB,
                    Gender = Patient.Gender,
                    ContactNumber = Patient.ContactNumber,
                    Address = Patient.Address
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
                var patientDTO = new OutPatientDTO
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DoB = patient.DoB,
                    Gender = patient.Gender,
                    ContactNumber = patient.ContactNumber,
                    Address = patient.Address
                };
                return patientDTO;
            }
            return null;
        }

        /// <summary>
        /// Deletes a patient from the system.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        public async Task Delete(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Entry(patient).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves detailed information about a specific patient.
        /// </summary>
        /// <param name="PatientID">The ID of the patient to retrieve.</param>
        /// <returns>Detailed patient information.</returns>
        public async Task<PatientDTO> GetPatient(int PatientID)
        {
            var Patient = await _context.Patients
                .Include(r => r.Rooms)
                .ThenInclude(d => d.department)
                .Include(p => p.Appointments)
                .Include(p => p.MedicalReports)
                .ThenInclude(mr => mr.doctor)
                .FirstOrDefaultAsync(f => f.Id == PatientID);
            if (Patient == null)
            {
                return null;
            }
            OutDepartmentDTO Department = null;
            RoomPatient Room = null;
            if (Patient.Rooms != null)
            {
                Department = new OutDepartmentDTO
                {
                    Id = Patient.Rooms!.department!.Id,
                    DepartmentName = Patient.Rooms.department.DepartmentName
                };
                Room = new RoomPatient
                {
                    Id = Patient.Rooms.Id,
                    RoomNumber = Patient.Rooms.RoomNumber,
                    RoomAvailability = Patient.Rooms.RoomAvailability,
                    NumberOfBeds = Patient.Rooms.NumberOfBeds,
                    DepartmentId = Patient.Rooms.DepartmentId,
                    department = Department
                };
            }
            var patient = new PatientDTO
            {
                Id = Patient.Id,
                FirstName = Patient.FirstName,
                LastName = Patient.LastName,
                DoB = Patient.DoB,
                Gender = Patient.Gender,
                ContactNumber = Patient.ContactNumber,
                Address = Patient.Address,
                RoomId = Patient.RoomId,
                Rooms = Room,
                Appointments = Patient.Appointments.Select(a => new OutAppointmentDTO()
                {
                    Id = a.Id,
                    DateOfAppointment = a.DateOfAppointment,
                    PatientId = a.PatientId,
                    PatientName = $"{a.patient.FirstName} {a.patient.LastName}",
                    DoctorId = a.DoctorId,
                    DoctorName = $"{a.doctor.FirstName} {a.doctor.LastName}",
                    DepartmentName = a.doctor.department.DepartmentName
                }).ToList(),
                MedicalReports = Patient.MedicalReports.Select(m => new OutMedicalReportDTO()
                {
                    Id = m.Id,
                    ReportDate = m.ReportDate,
                    Description = m.Description,
                    PatientId = m.PatientId,
                    PatientName = $"{m.patient!.FirstName} {m.patient.LastName}",
                    DoctorId = m.DoctorId,
                    DoctorName = $"{m.doctor!.FirstName} {m.doctor.LastName}",
                    DepartmentName = m.doctor.department.DepartmentName
                }).ToList()
            };
            return patient;
        }

        /// <summary>
        /// Retrieves a list of all patients in the system.
        /// </summary>
        /// <returns>A list of patient information.</returns>
        public async Task<List<OutPatientDTO>> GetPatients()
        {
            var patients = await _context.Patients.Include(r => r.Rooms).ThenInclude(d => d.department)
                .Select(x => new OutPatientDTO()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DoB = x.DoB,
                    Gender = x.Gender,
                    ContactNumber = x.ContactNumber,
                    Address = x.Address,
                    RoomId = x.RoomId,
                    RoomNumber = x.Rooms!.RoomNumber,
                    DepartmentName = x.Rooms.department!.DepartmentName
                }).ToListAsync();
            return patients;
        }

        /// <summary>
        /// Updates the information of a specific patient.
        /// </summary>
        /// <param name="id">The ID of the patient to update.</param>
        /// <param name="patientDTO">The updated patient information.</param>
        /// <returns>The updated patient information.</returns>
        public async Task<OutPatientDTO> UpdatePatient(int id, InPatientDTO patientDTO)
        {
            var Patient = await _context.Patients.FindAsync(id);
            if (Patient != null)
            {
                Patient.FirstName = patientDTO.FirstName;
                Patient.LastName = patientDTO.LastName;
                Patient.DoB = patientDTO.DoB;
                Patient.Gender = patientDTO.Gender;
                Patient.ContactNumber = patientDTO.ContactNumber;
                Patient.Address = patientDTO.Address;
                Patient.RoomId = patientDTO.RoomId;

                if (patientDTO.RoomId != null)
                {
                    var room = await _context.Rooms.Include(p => p.Patients).FirstOrDefaultAsync(a => a.Id == patientDTO.RoomId);
                    if (room == null)
                    {
                        throw new Exception("Room does not exist.");
                    }
                    if (room.NumberOfBeds + 1 == room.Patients!.Count)
                    {
                        room.RoomAvailability = false;
                    }
                    else
                    {
                        room.RoomAvailability = true;
                    }
                    var dep = await _context.Departments.FindAsync(room.DepartmentId);
                    if (room.RoomAvailability)
                    {
                        var patient = new OutPatientDTO
                        {
                            FirstName = Patient.FirstName,
                            LastName = Patient.LastName,
                            DoB = Patient.DoB,
                            Gender = Patient.Gender,
                            ContactNumber = Patient.ContactNumber,
                            Address = Patient.Address,
                            RoomId = Patient.RoomId,
                            RoomNumber = room.RoomNumber,
                            DepartmentName = dep.DepartmentName
                        };
                        await _context.SaveChangesAsync();
                        return patient;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    var patient = new OutPatientDTO
                    {
                        FirstName = Patient.FirstName,
                        LastName = Patient.LastName,
                        DoB = Patient.DoB,
                        Gender = Patient.Gender,
                        ContactNumber = Patient.ContactNumber,
                        Address = Patient.Address
                    };
                    await _context.SaveChangesAsync();
                    return patient;
                }
            }
            return null;
        }
    }
}

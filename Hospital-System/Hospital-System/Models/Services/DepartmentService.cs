using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.DTOs.Room;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Hospital_System.Models.Services
{
    /// <summary>
    /// Service class for managing departments within the hospital.
    /// </summary>
    public class DepartmentService : IDepartment
    {
    private readonly HospitalDbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DepartmentService(HospitalDbContext context)
    {
        _context = context;
    }
        // CREATE Department........................................................................

        /// <summary>
        /// Creates a new department in the system.
        /// </summary>
        /// <param name="newDepartmentDTO">The department information to create.</param>
        /// <returns>The created department information.</returns>
        public async Task<InDepartmentDTO> CreateDepartment(InDepartmentDTO newDepartmentDTO)
    {
        Department department = new Department
        {
            DepartmentName = newDepartmentDTO.DepartmentName,
            HospitalID = newDepartmentDTO.HospitalID,
        };
        _context.Entry(department).State = EntityState.Added;
        await _context.SaveChangesAsync();
            newDepartmentDTO.Id = department.Id;

            return newDepartmentDTO;
    }
        // Get Department........................................................................

        /// <summary>
        /// Retrieves information for all departments 
        /// </summary>
       
        public async Task<List<OutDepartmentDTO>> GetDepartments()
        {
            var department = await _context.Departments.Select(x => new OutDepartmentDTO()
            {
                Id = x.Id,
                DepartmentName = x.DepartmentName,


            }).ToListAsync();






              
            return department;
        }

        // Get Department by ID........................................................................
        /// <summary>
        /// Retrieves information about a specific department.
        /// </summary>
        /// <param name="id">The ID of the department to retrieve.</param>
        /// <returns>The department information.</returns>
        public async Task<GetDeptmartmentDTO> GetDepartment(int id)
        {
            var department = await _context.Departments
                .Where(x => x.Id == id)
                .Select(x => new GetDeptmartmentDTO()
                {
                    Id = x.Id,
                    DepartmentName = x.DepartmentName,
                    HospitalID = x.HospitalID,
                    Rooms = x.Rooms.Select(room => new OutRoomDTO
                    {
                        Id = room.Id,
                        RoomNumber = room.RoomNumber,
                        RoomAvailability = room.RoomAvailability,
                        NumberOfBeds = room.NumberOfBeds,
                        DepartmentId =room.DepartmentId,
                    }).ToList(),

                    Doctors = x.Doctors.Select(x => new OutDoctorDTO()
                    {
                        Id = x.Id,
                        FullName = $"{x.FirstName} {x.LastName}",
                        Gender = x.Gender,
                        ContactNumber = x.ContactNumber,
                        Speciality = x.Speciality,
                        
                    }).ToList(),

                    Nurses = x.Nurses.Select(x => new InNurseDTO()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Gender = x.Gender,
                        ContactNumber = x.ContactNumber,
                        DepartmentId = x.DepartmentId
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return department;
        }


        // Update Department by ID........................................................................
        /// <summary>
        /// Updates the information of a specific department.
        /// </summary>
        /// <param name="id">The ID of the department to update.</param>
        /// <param name="updateDepartmentDTO">The updated department information.</param>
        /// <returns>The updated department information.</returns>
        public async Task<OutDepartmentDTO> UpdateDepartment(int id, OutDepartmentDTO updateDepartmentDTO)
        {
            var existingDepartment = await _context.Departments.FindAsync(id);

            if (existingDepartment == null)
            {
                throw new InvalidOperationException($"Department with ID {id} not found.");
            }

            existingDepartment.DepartmentName = updateDepartmentDTO.DepartmentName;

            _context.Entry(existingDepartment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return updateDepartmentDTO;
        }

        // Delete Appointment by ID........................................................................

        /// <summary>
        /// Deletes a department from the system.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        public async Task DeleteDepartment(int id)
        {
            Department department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Entry(department).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Department with ID {id} not found.");
            }
        }

        /// <summary>
        /// Retrieves the list of doctors in a specific department.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>The list of doctors in the department.</returns>
        public async Task<List<InDoctorDTO>> GetDoctorsInDepartment(int departmentId)
        {
            var doctors = await _context.Doctors
                .Where(d => d.DepartmentId == departmentId)
                .Select(d => new InDoctorDTO()
                {
                    Id = d.Id,
                    LastName = d.LastName,
                    FirstName =d.FirstName,
                    FullName = $"{d.FirstName} {d.LastName}",
                    Gender = d.Gender,
                    ContactNumber = d.ContactNumber,
                    Speciality = d.Speciality,
                    DepartmentId = d.DepartmentId
                })
                .ToListAsync();

            return doctors;
        }
        /// <summary>
        /// Retrieves the list of Nurses in a specific department.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>The list of doctors in the department.</returns>
        public async Task<List<InNurseDTO>> GetNursesInDepartment(int departmentId)
        {
            var Nurses = await _context.Nurses
                .Where(d => d.DepartmentId == departmentId)
                .Select(d => new InNurseDTO()
                {
                    Id = d.Id,
                   FirstName=d.FirstName,
                   LastName = d.LastName,
                    Gender = d.Gender,
                    ContactNumber = d.ContactNumber,
                    DepartmentId = d.DepartmentId
                })
                .ToListAsync();

            return Nurses;
        }


        /// <summary>
        /// Retrieves the list of Rooms and Patients in a specific department.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>The list of doctors in the department.</returns>
        public async Task<List<RoomPatientDTO>> GetRoomsAndPatientsInDepartment(int departmentId)
        {
            var rooms = await _context.Rooms
                .Where(d => d.DepartmentId == departmentId)
                .Select(x => new RoomPatientDTO()
            {
                Id = x.Id,
                RoomNumber = x.RoomNumber,
                RoomAvailability = x.RoomAvailability,
                NumberOfBeds = x.NumberOfBeds,
                DepartmentId = x.DepartmentId,
                Patients = x.Patients.Select(x => new InPatientDTO()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DoB = x.DoB,
                    Gender = x.Gender,
                    ContactNumber = x.ContactNumber,
                    Address = x.Address,
                    RoomId = x.RoomId,
                    
                  

                }).ToList()
            }).ToListAsync();
            return rooms;
        }


        /// <summary>
        /// Retrieves the list of Rooms in a specific department.
        /// </summary>
        /// <param name="departmentId">The ID of the department.</param>
        /// <returns>The list of doctors in the department.</returns>
        public async Task<List<OutRoomDTO>> GetRoomsInDepartment(int departmentId)
        {
            var Nurses = await _context.Rooms
                .Where(d => d.DepartmentId == departmentId)
                .Select(d => new OutRoomDTO()
                {
                    Id = d.Id,
                    RoomNumber = d.RoomNumber,
                    NumberOfBeds = d.NumberOfBeds,
                    RoomAvailability = d.RoomAvailability,
                    DepartmentId = d.DepartmentId


                })
                .ToListAsync();

            return Nurses;
        }
    }
}
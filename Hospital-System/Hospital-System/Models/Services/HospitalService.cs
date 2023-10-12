using Hospital_System.Data;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Hospital;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Models.Services
{
    /// <summary>
    /// Service class for managing hospitals within the system.
    /// </summary>
    public class HospitalService : IHospital
    {
        private readonly HospitalDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="HospitalService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public HospitalService(HospitalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new hospital in the system.
        /// </summary>
        /// <param name="hospital">The hospital information to create.</param>
        /// <returns>The created hospital information.</returns>
        public async Task<OutHospitalDTO> Create(OutHospitalDTO hospital)
        {
            Hospital hospitalEntity = new Hospital()
            {
                Id = hospital.Id,
                HospitalName = hospital.HospitalName,
                Address = hospital.Address,
                ContactNumber = hospital.ContactNumber
            };

            _context.Hospitals.Add(hospitalEntity);
            await _context.SaveChangesAsync();
            hospital.Id = hospitalEntity.Id;
            return hospital;
        }

        /// <summary>
        /// Deletes a hospital from the system.
        /// </summary>
        /// <param name="id">The ID of the hospital to delete.</param>
        public async Task Delete(int id)
        {
            Hospital hospital = await _context.Hospitals.FindAsync(id);
            _context.Entry(hospital).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves detailed information about a specific hospital.
        /// </summary>
        /// <param name="HospitalID">The ID of the hospital to retrieve.</param>
        /// <returns>Detailed hospital information.</returns>
        public async Task<InHospitalDTO> GetHospital(int HospitalID)
        {
            var hospital = await _context.Hospitals.Select(
                hospital => new InHospitalDTO
                {
                    Id = hospital.Id,
                    HospitalName = hospital.HospitalName,
                    Address = hospital.Address,
                    ContactNumber = hospital.ContactNumber,
                    departments = hospital.Departments.Select(x => new InDepartmentDTO
                    {
                        Id = x.Id,
                        DepartmentName = x.DepartmentName,
                        HospitalID = x.HospitalID
                    }).ToList(),
                }
            ).FirstOrDefaultAsync(x => x.Id == HospitalID);

            return hospital;
        }

        /// <summary>
        /// Retrieves a list of all hospitals in the system.
        /// </summary>
        /// <returns>A list of hospital information.</returns>
        public async Task<List<OutHospitalDTO>> GetHospitals()
        {
            return await _context.Hospitals.Select(
                hospital => new OutHospitalDTO
                {
                    Id = hospital.Id,
                    HospitalName = hospital.HospitalName,
                    Address = hospital.Address,
                    ContactNumber = hospital.ContactNumber,
                  
                }
            ).ToListAsync();
        }

        /// <summary>
        /// Updates the information of a specific hospital.
        /// </summary>
        /// <param name="id">The ID of the hospital to update.</param>
        /// <param name="hospitalDTO">The updated hospital information.</param>
        /// <returns>The updated hospital information.</returns>
        public async Task<OutHospitalDTO> UpdateHospital(int id, OutHospitalDTO hospitalDTO)
        {
            Hospital hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
                return null;

            hospital.Id = hospitalDTO.Id;
            hospital.HospitalName = hospitalDTO.HospitalName;
            hospital.ContactNumber = hospitalDTO.ContactNumber;
            hospital.Address = hospitalDTO.Address;

            _context.Entry(hospital).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hospitalDTO;
        }


        /// <summary>
        /// Retrieves the list of Departments in a specific Hospital.
        /// </summary>
        /// <param name="HospitalId">The ID of the department.</param>
        /// <returns>The list of doctors in the department.</returns>
        public async Task<List<OutDepartmentDTO>> GetDepartmentsInHospital(int HospitalId)
        {
            var depts = await _context.Departments
                .Where(d => d.HospitalID == HospitalId)
                .Select(d => new OutDepartmentDTO()
                {
                    Id = d.Id,
                   DepartmentName=d.DepartmentName,
                   
                })
                .ToListAsync();

            return depts;
        }
    }
}

using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Medicine;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Hospital_System.Models.Services
{
    /// <summary>
    /// Service class for managing medicines within the hospital system.
    /// </summary>
    public class MedicineService : IMedicine
    {
        private readonly HospitalDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicineService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public MedicineService(HospitalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new medicine in the system.
        /// </summary>
        /// <param name="newMedicineDTO">The data transfer object containing medicine information.</param>
        /// <returns>The created medicine information.</returns>
        public async Task<InMedicineDTO> CreateMedicine(InMedicineDTO newMedicineDTO)
        {
            Medicine medicine = new Medicine
            {
                MedicineName = newMedicineDTO.MedicineName,
                Portion = newMedicineDTO.Portion,
                MedicalReportId = newMedicineDTO.MedicalReportId
            };

            _context.Entry(medicine).State = EntityState.Added;
            await _context.SaveChangesAsync();
            newMedicineDTO.Id = medicine.Id;

            return newMedicineDTO;
        }

        /// <summary>
        /// Retrieves a list of all medicines in the system.
        /// </summary>
        /// <returns>A list of medicine information.</returns>
        public async Task<List<OutMedicineDTO>> GetMedicines()
        {
            var medicines = await _context.Medicines.Select(x => new OutMedicineDTO()
            {
                Id = x.Id,
                MedicineName = x.MedicineName,
                Portion = x.Portion
            }).ToListAsync();

            return medicines;
        }

        /// <summary>
        /// Retrieves detailed information about a specific medicine.
        /// </summary>
        /// <param name="id">The ID of the medicine to retrieve.</param>
        /// <returns>Detailed medicine information.</returns>
        public async Task<InMedicineDTO> GetMedicine(int id)
        {
            var medicine = await _context.Medicines.Select(x => new InMedicineDTO()
            {
                Id = x.Id,
                MedicineName = x.MedicineName,
                Portion = x.Portion,
                MedicalReportId = x.MedicalReportId
            }).FirstOrDefaultAsync(x => x.Id == id);

            return medicine;
        }

        /// <summary>
        /// Updates the information of a specific medicine.
        /// </summary>
        /// <param name="id">The ID of the medicine to update.</param>
        /// <param name="updateMedicineDTO">The updated medicine information.</param>
        /// <returns>The updated medicine information.</returns>
        public async Task<InMedicineDTO> UpdateMedicine(int id, InMedicineDTO updateMedicineDTO)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return null;
            }

            medicine.MedicineName = updateMedicineDTO.MedicineName;
            medicine.Portion = updateMedicineDTO.Portion;

            _context.Entry(medicine).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return updateMedicineDTO;
        }

        /// <summary>
        /// Deletes a medicine from the system.
        /// </summary>
        /// <param name="id">The ID of the medicine to delete.</param>
        public async Task DeleteMedicine(int id)
        {
            Medicine medicine = await _context.Medicines.FindAsync(id);
            if (medicine != null)
            {
                _context.Entry(medicine).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}

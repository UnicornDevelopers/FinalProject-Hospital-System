using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.DTOs.Medicine;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Hospital_System.Models.Services
{
    /// <summary>
    /// Service class for managing medical reports within the hospital system.
    /// </summary>
    public class MedicalReportService : IMedicalReport
    {
        private readonly HospitalDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicalReportService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public MedicalReportService(HospitalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new medical report in the system.
        /// </summary>
        /// <param name="newMedicalReportDTO">The data transfer object containing medical report information.</param>
        /// <returns>The created medical report information.</returns>
        public async Task<OutMedicalReportDTO> CreateMedicalReport(InMedicalReportDTO newMedicalReportDTO)
        {
            var doctor = await _context.Doctors
                .Include(d => d.department)
                .FirstOrDefaultAsync(d => d.Id == newMedicalReportDTO.DoctorId);
            var patient = await _context.Patients.FindAsync(newMedicalReportDTO.PatientId);

            if (patient != null && doctor != null)
            {
                MedicalReport medicalReport = new MedicalReport
                {
                    ReportDate = newMedicalReportDTO.ReportDate,
                    Description = newMedicalReportDTO.Description,
                    PatientId = newMedicalReportDTO.PatientId,
                    DoctorId = newMedicalReportDTO.DoctorId,
                };

                _context.MedicalReports.Add(medicalReport);
                await _context.SaveChangesAsync();

                OutMedicalReportDTO outMedicalReportDTO = new OutMedicalReportDTO
                {
                    Id = medicalReport.Id,
                    ReportDate = medicalReport.ReportDate,
                    Description = medicalReport.Description,
                    PatientId = medicalReport.PatientId,
                    PatientName = $"{patient.FirstName} {patient.LastName}",
                    DoctorId = medicalReport.DoctorId,
                    DoctorName = $"{doctor.FirstName} {doctor.LastName}",
                    DepartmentName = doctor.department != null ? doctor.department.DepartmentName : string.Empty
                };

                return outMedicalReportDTO;
            }
            else
            {
                throw new ArgumentException("Invalid PatientId or Doctor Id");
            }
        }

        /// <summary>
        /// Retrieves a list of all medical reports in the system.
        /// </summary>
        /// <returns>A list of medical report information.</returns>
        public async Task<List<OutMedicalReportDTO>> GetMedicalReports()
        {
            var medicalReports = await _context.MedicalReports
                .Include(m => m.patient)
                .Include(m => m.doctor.department)
                .Select(x => new OutMedicalReportDTO()
                {
                    Id = x.Id,
                    ReportDate = x.ReportDate,
                    Description = x.Description,
                    PatientId = x.PatientId,
                    PatientName = $"{x.patient.FirstName} {x.patient.LastName}",
                    DoctorId = x.DoctorId,
                    DoctorName = $"{x.doctor.FirstName} {x.doctor.LastName}",
                    DepartmentName = x.doctor.department.DepartmentName,
                }).ToListAsync();

            return medicalReports;
        }

        /// <summary>
        /// Retrieves detailed information about a specific medical report.
        /// </summary>
        /// <param name="id">The ID of the medical report to retrieve.</param>
        /// <returns>Detailed medical report information.</returns>
        public async Task<NewMedicalReportDTO> GetMedicalReport(int id)
        {
            var medicalReport = await _context.MedicalReports.Select(x => new NewMedicalReportDTO()
            {
                Id = x.Id,
                ReportDate = x.ReportDate,
                Description = x.Description,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                Medicines = x.Medicines.Select(x => new OutMedicineDTO()
                {
                    Id = x.Id,
                    MedicineName = x.MedicineName,
                    Portion = x.Portion,
                }).ToList(),
            }).FirstOrDefaultAsync(x => x.Id == id);

            return medicalReport;
        }

        /// <summary>
        /// Updates the information of a specific medical report.
        /// </summary>
        /// <param name="id">The ID of the medical report to update.</param>
        /// <param name="updateMedicalReportDTO">The updated medical report information.</param>
        /// <returns>The updated medical report information.</returns>
        public async Task<OutMedicalReportDTO> UpdateMedicalReport(int id, InMedicalReportDTO updateMedicalReportDTO)
        {
            MedicalReport report = await _context.MedicalReports
                .Include(m => m.patient)
                .Include(m => m.doctor.department)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (report == null)
            {
                throw new ArgumentException($"Medical report with ID {id} not found.");
            }

            report.ReportDate = updateMedicalReportDTO.ReportDate;
            report.Description = updateMedicalReportDTO.Description;
            await _context.SaveChangesAsync();

            var outMedicalReportDTO = new OutMedicalReportDTO
            {
                Id = report.Id,
                ReportDate = report.ReportDate,
                Description = report.Description,
                PatientId = report.PatientId,
                PatientName = $"{report.patient.FirstName} {report.patient.LastName}",
                DoctorId = report.DoctorId,
                DoctorName = $"{report.doctor.FirstName} {report.doctor.LastName}",
                DepartmentName = report.doctor.department.DepartmentName
            };

            return outMedicalReportDTO;
        }

        /// <summary>
        /// Deletes a medical report from the system.
        /// </summary>
        /// <param name="id">The ID of the medical report to delete.</param>
        public async Task DeleteMedicalReport(int id)
        {
            MedicalReport medicalReport = await _context.MedicalReports.FindAsync(id);
            if (medicalReport != null)
            {
                _context.Entry(medicalReport).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}

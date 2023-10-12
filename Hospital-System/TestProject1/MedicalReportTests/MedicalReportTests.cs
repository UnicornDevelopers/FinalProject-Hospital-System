using Hospital_System.Models;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.DTOs.Medicine;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace TestProject1.DoctorTests
{
    public class MedicalReportTests : Mock
    {
        [Fact]
        public async Task CreateMedicalReport_ReturnMedicalReportDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var medicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicalReportService = new MedicalReportService(_db);
            var newMedicalReport = new InMedicalReportDTO
            {
                ReportDate = medicalReport.ReportDate,
                Description = medicalReport.Description,
                DoctorId = doctor.Id,
                PatientId = patient.Id,
            };
            var createdMedicalReport = await medicalReportService.CreateMedicalReport(newMedicalReport);
            Assert.NotNull(createdMedicalReport);
            Assert.Equal(new DateTime(2020, 5, 11), createdMedicalReport.ReportDate);
            Assert.Equal("Test Description", createdMedicalReport.Description);
            Assert.Equal(doctor.Id, createdMedicalReport.DoctorId);
            Assert.Equal(patient.Id, createdMedicalReport.PatientId);
        }
        [Fact]
        public async Task GetMedicalReportss_ReturnListOfMedicalReportsDTOs()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var medicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var patient2 = await CreateAndSaveTestPatient(room.Id);
            var medicalReport2 = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicalReportService = new MedicalReportService(_db);
            var retrievedMedicalReportsDto = await medicalReportService.GetMedicalReports();
            Assert.NotNull(retrievedMedicalReportsDto);
            Assert.Equal(2, retrievedMedicalReportsDto.Count);
            Assert.Contains(retrievedMedicalReportsDto, dto => dto.Id == medicalReport.Id);
            Assert.Contains(retrievedMedicalReportsDto, dto => dto.Id == medicalReport2.Id);
        }
        [Fact]
        public async Task GetMedicalReport_ReturnMedicalReportDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var medicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicalReportService = new MedicalReportService(_db);
            var retrievedMedicalReport = await medicalReportService.GetMedicalReport(medicalReport.Id);
            Assert.NotNull(retrievedMedicalReport);
            Assert.Equal(new DateTime(2020, 5, 11), retrievedMedicalReport.ReportDate);
            Assert.Equal("Test Description", retrievedMedicalReport.Description);
            Assert.Equal(doctor.Id, retrievedMedicalReport.DoctorId);
            Assert.Equal(patient.Id, retrievedMedicalReport.PatientId);
        }
        [Fact]
        public async Task UpdateMedicalReport_ReturnUpdatedMedicalReportDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var medicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicalReportService = new MedicalReportService(_db);
            var updatedMedicalReport = new InMedicalReportDTO
            {
                ReportDate = new DateTime(2022, 2, 2),
                Description = "New Description",
                DoctorId = doctor.Id,
                PatientId = patient.Id,
            };
            var updatedMedicalReportDto = await medicalReportService.UpdateMedicalReport(medicalReport.Id, updatedMedicalReport);
            Assert.NotNull(updatedMedicalReportDto);
            Assert.Equal(new DateTime(2022, 2, 2), updatedMedicalReportDto.ReportDate);
            Assert.Equal("New Description", updatedMedicalReportDto.Description);
            Assert.Equal(doctor.Id, updatedMedicalReportDto.DoctorId);
            Assert.Equal(patient.Id, updatedMedicalReportDto.PatientId);
        }
        [Fact]
        public async Task DeleteMedicalReport_ReturnDeletedMedicalReport()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var medicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicalReportService = new MedicalReportService(_db);
            await medicalReportService.DeleteMedicalReport(medicalReport.Id);
            var deletedMedicine = await _db.Medicines.FindAsync(medicalReport.Id);
            Assert.Null(deletedMedicine);
        }
    }
}
using Hospital_System.Models.DTOs.Medicine;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using Xunit;
namespace TestProject1.DoctorTests
{
    public class MedicineTests : Mock
    {
        [Fact]
        public async Task CreateMedicine_ReturnMedicineDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var medicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicine = await CreateAndSaveTestMedicine(medicalReport.Id);
            var medicineService = new MedicineService(_db);
            var newMedicine = new InMedicineDTO
            {
                MedicineName = medicine.MedicineName,
                Portion = medicine.Portion,
                MedicalReportId = medicalReport.Id,
            };
            var createdMedicine = await medicineService.CreateMedicine(newMedicine);
            Assert.NotNull(createdMedicine);
            Assert.Equal("Test MedicineName", createdMedicine.MedicineName);
            Assert.Equal("Test Portion", createdMedicine.Portion);
            Assert.Equal(medicalReport.Id, createdMedicine.MedicalReportId);
        }
        [Fact]
        public async Task GetMedicines_ReturnListOfMedicineDTOs()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var medicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicine = await CreateAndSaveTestMedicine(medicalReport.Id);
            var patient2 = await CreateAndSaveTestPatient(room.Id);
            var medicalReport2 = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicine2 = await CreateAndSaveTestMedicine(medicalReport2.Id);
            var medicineService = new MedicineService(_db);
            var retrievedMedicinesDto = await medicineService.GetMedicines();
            Assert.NotNull(retrievedMedicinesDto);
            Assert.Equal(2, retrievedMedicinesDto.Count);
            Assert.Contains(retrievedMedicinesDto, dto => dto.Id == medicine.Id);
            Assert.Contains(retrievedMedicinesDto, dto => dto.Id == medicine2.Id);
        }
        [Fact]
        public async Task GetMedicine_ReturnMedicineDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var medicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicine = await CreateAndSaveTestMedicine(medicalReport.Id);
            var medicineService = new MedicineService(_db);
            var retrievedMedicine = await medicineService.GetMedicine(medicine.Id);
            Assert.NotNull(retrievedMedicine);
            Assert.Equal(medicine.Id, retrievedMedicine.Id);
            Assert.Equal("Test MedicineName", retrievedMedicine.MedicineName);
            Assert.Equal("Test Portion", retrievedMedicine.Portion);
            Assert.Equal(medicalReport.Id, retrievedMedicine.MedicalReportId);
        }
        [Fact]
        public async Task UpdateMedicine_ReturnUpdatedMedicineDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var medicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicine = await CreateAndSaveTestMedicine(medicalReport.Id);
            var medicineService = new MedicineService(_db);
            var updatedMedicine = new InMedicineDTO
            {
                MedicineName = "New MedicineName",
                Portion = "New Portion",
                MedicalReportId = medicalReport.Id,
            };
            var updatedMedicineDto = await medicineService.UpdateMedicine(medicine.Id, updatedMedicine);
            Assert.NotNull(updatedMedicineDto);
            Assert.Equal("New MedicineName", updatedMedicineDto.MedicineName);
            Assert.Equal("New Portion", updatedMedicineDto.Portion);
            Assert.Equal(medicalReport.Id, updatedMedicineDto.MedicalReportId);
        }
        [Fact]
        public async Task DeleteMedicine_ReturnDeletedMedicine()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var medicalReport = await CreateAndSaveTestMedicalReport(doctor.Id, patient.Id);
            var medicine = await CreateAndSaveTestMedicine(medicalReport.Id);
            var medicineService = new MedicineService(_db);
            await medicineService.DeleteMedicine(medicine.Id);
            var deletedMedicine = await _db.Medicines.FindAsync(medicine.Id);
            Assert.Null(deletedMedicine);
        }
    }
}
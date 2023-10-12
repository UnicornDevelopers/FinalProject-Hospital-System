using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital_System.Models;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Hospital;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using Xunit;
namespace TestProject1.MedRep
{
    public class Dep : Mock
    {

        private IDepartment BuildRepository()
        {
            return new DepartmentService(_db);
        }
        [Fact]
        public async Task CreateDepartment_DepartmentReportDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var departmentService = new DepartmentService(_db);
            var newDepartment = new InDepartmentDTO
            {
                DepartmentName = department.DepartmentName,
                HospitalID = department.Id,
            };
            var createdDepartment = await departmentService.CreateDepartment(newDepartment);
            Assert.NotNull(createdDepartment);
            Assert.Equal("Test Department", createdDepartment.DepartmentName);
            Assert.Equal(department.Id, createdDepartment.HospitalID);
        }
        [Fact]
        public async Task GetDepartment_ReturnListOfDepartmentDTOs()
        {
            var hospital1 = await CreateAndSaveTestHospital();
            var department1 = await CreateAndSaveTestDepartment(hospital1.Id);
            var hospital2 = await CreateAndSaveTestHospital();
            var department2 = await CreateAndSaveTestDepartment(hospital2.Id);
            var departmentService = new DepartmentService(_db);
            var departmentDto = await departmentService.GetDepartments();
            Assert.NotNull(departmentDto);
            Assert.Equal(2, departmentDto.Count);
            Assert.Contains(departmentDto, dto => dto.Id == department1.Id);
            Assert.Contains(departmentDto, dto => dto.Id == department2.Id);
        }
        [Fact]
        public async Task GetDepartment_ReturnDepartmentDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var departmentService = new DepartmentService(_db);
            var retrievedDepartment = await departmentService.GetDepartment(department.Id);
            Assert.NotNull(retrievedDepartment);
            Assert.Equal("Test Department", retrievedDepartment.DepartmentName);
        }
        [Fact]
        public async Task UpdateDepartment_ReturnUpdatedDepartmentDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var departmentService = new DepartmentService(_db);
            var updatedDepartment = new OutDepartmentDTO
            {
                DepartmentName = "New Department",
            };
            var updatedDepartmentDto = await departmentService.UpdateDepartment(department.Id, updatedDepartment);
            Assert.NotNull(updatedDepartmentDto);
            Assert.Equal("New Department", updatedDepartmentDto.DepartmentName);
        }
        [Fact]
        public async Task DeleteDepartment_ReturnDeletedDepartment()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var departmentService = new DepartmentService(_db);
            await departmentService.DeleteDepartment(department.Id);
            var deletedDepartment = await _db.Departments.FindAsync(department.Id);
            Assert.Null(deletedDepartment);
        }



        [Fact]
        public async Task Get_All_Nurses_In_Dept()
        {
            // Arrange
            var departmentService = BuildRepository();
            var hospitalService = new HospitalService(_db);
            var nurseService = new NurseService(_db);
            var inputHospital = new OutHospitalDTO
            {
                HospitalName = "Al Basheer",
                Address = "Amman",
                ContactNumber = "0798765432"
            };
            var hospitalResult = await hospitalService.Create(inputHospital);
            var inputDepartment = new InDepartmentDTO
            {
                DepartmentName = "Bones",
                HospitalID = hospitalResult.Id
            };
            var departmentResult = await departmentService.CreateDepartment(inputDepartment);
            var nurse1 = new InNurseDTO
            {
                FirstName = "Mhamad Belal",
                LastName = "Al Msalma",
                Gender = "Male",
                ContactNumber = "078665678543",
                Shift = Nurse.Shift.Night,
                DepartmentId = departmentResult.Id
            };
            var nurse2 = new InNurseDTO
            {
                FirstName = "Oday",
                LastName = "Oday",
                Gender = "Male",
                ContactNumber = "078665678543",
                Shift = Nurse.Shift.Night,
                DepartmentId = departmentResult.Id
            };
            var nurse3 = new InNurseDTO
            {
                FirstName = "Marwa",
                LastName = "Marwa",
                Gender = "Male",
                ContactNumber = "078665678543",
                Shift = Nurse.Shift.Night,
                DepartmentId = departmentResult.Id
            };
            // Act
            var resultNurse1 = await nurseService.Create(nurse1);
            var resultNurse2 = await nurseService.Create(nurse2);
            var resultNurse3 = await nurseService.Create(nurse3);
            var allNursesInDep = await departmentService.GetNursesInDepartment(departmentResult.Id);
            // Assert
            Assert.NotNull(resultNurse1);
            Assert.NotNull(resultNurse2);
            Assert.NotNull(resultNurse3);
            Assert.Equal(allNursesInDep.Count, 3);
            // Assert other properties
        }

        [Fact]
        public async Task GetRoomsInDepartment_ReturnRoomsInDepartment()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room1 = await CreateAndSaveTestRoom(department.Id);
            var room2 = await CreateAndSaveTestRoom(department.Id);
            var room3 = await CreateAndSaveTestRoom(department.Id);
            var departmentService = new DepartmentService(_db);
            var retrievedDepartment = await departmentService.GetRoomsInDepartment(department.Id);
            Assert.Equal(3, retrievedDepartment.Count);
        }
        [Theory]
        [InlineData(1)] // Parameterize the departmentId you want to test
        public async Task GetRoomsAndPatientsInDepartment_ReturnRoomsRoomsAndPatientsInDepartment(int departmentId)
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room1 = await CreateAndSaveTestRoom(department.Id);
            var room2 = await CreateAndSaveTestRoom(department.Id);
            var room3 = await CreateAndSaveTestRoom(department.Id);
            var patient = await CreateAndSaveTestPatient(room1.Id);
            var patient2 = await CreateAndSaveTestPatient(room1.Id);
            var patient3 = await CreateAndSaveTestPatient(room2.Id);
            var patient4 = await CreateAndSaveTestPatient(room3.Id);
            var patient5 = await CreateAndSaveTestPatient(room3.Id);
            var departmentService = new DepartmentService(_db);
            var retrievedDepartments = await departmentService.GetRoomsAndPatientsInDepartment(department.Id);
            var departmentDto = retrievedDepartments[0];
            Assert.Equal(3, retrievedDepartments.Count);
            var totalPatients = retrievedDepartments.Sum(room => room.Patients.Count);
            Assert.Equal(5, totalPatients);
        }
    }

    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Hospital_System.Models;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using static Hospital_System.Models.Nurse;
namespace TestProject1.NurseTests
{
    public class NurseServiceTests : Mock
    {
        [Fact]
        public async Task CreateNurse_ReturnNurseDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var nurse = await CreateAndSaveTestNurse(department.Id);
            var nurseService = new NurseService(_db);
            var newNurse = new InNurseDTO
            {
                FirstName = nurse.FirstName,
                LastName = nurse.LastName,
                Gender = nurse.Gender,
                ContactNumber = nurse.ContactNumber,
                Shift = nurse.shift,
                DepartmentId = nurse.DepartmentId
            };
            var createdNurse = await nurseService.Create(newNurse);
            Assert.NotNull(createdNurse);
            Assert.Equal("Test", createdNurse.FirstName);
            Assert.Equal("Nurse", createdNurse.LastName);
            Assert.Equal("Female", createdNurse.Gender);
            Assert.Equal("123456789", createdNurse.ContactNumber);
            Assert.Equal(Shift.Morning, createdNurse.Shift);
            Assert.Equal(department.Id, createdNurse.DepartmentId);
        }
        [Fact]
        public async Task GetNurse_ReturnNurseDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var nurse = await CreateAndSaveTestNurse(department.Id);
            var nurseService = new NurseService(_db);
            var retrievedNurse = await nurseService.GetNurse(nurse.Id);
            Assert.NotNull(retrievedNurse);
            Assert.Equal(nurse.Id, retrievedNurse.Id);
            Assert.Equal(nurse.FirstName, retrievedNurse.FirstName);
        }
        [Fact]
        public async Task GetNurses_ReturnListOfNurseDTOs()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var department2 = await CreateAndSaveTestDepartment(hospital.Id);
            await CreateAndSaveTestNurse(department.Id);
            await CreateAndSaveTestNurse(department2.Id);
            var nurseService = new NurseService(_db);
            var nurses = await nurseService.GetNurses();
            Assert.NotNull(nurses);
            Assert.NotEmpty(nurses);
            Assert.Equal(2, nurses.Count);
        }
        [Fact]
        public async Task UpdateNurse_ReturnUpdatedNurseDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var nurse = await CreateAndSaveTestNurse(department.Id);
            var nurseService = new NurseService(_db);
            var updatedNurseDto = new InNurseDTO
            {
                Id = nurse.Id,
                FirstName = nurse.FirstName,
                LastName = nurse.LastName,
                Gender = nurse.Gender,
                ContactNumber = nurse.ContactNumber,
                Shift = Shift.Night,
                DepartmentId = department.Id
            };
            var updatedNurse = await nurseService.UpdateNurse(nurse.Id, updatedNurseDto);
            Assert.NotNull(updatedNurse);
            Assert.Equal(updatedNurseDto.FirstName, updatedNurse.FirstName);
            Assert.Equal(updatedNurseDto.LastName, updatedNurse.LastName);
            Assert.Equal(updatedNurseDto.Gender, updatedNurse.Gender);
            Assert.Equal(updatedNurseDto.ContactNumber, updatedNurse.ContactNumber);
            Assert.Equal(updatedNurseDto.Shift, updatedNurse.Shift);
            Assert.Equal(updatedNurseDto.DepartmentId, updatedNurse.DepartmentId);
        }
        [Fact]
        public async Task DeleteNurse_DeletesNurseFromDatabase()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var nurse = await CreateAndSaveTestNurse(department.Id);
            var nurseService = new NurseService(_db);
            await nurseService.Delete(nurse.Id);
            var deletedNurse = await _db.Nurses.FindAsync(nurse.Id);
            Assert.Null(deletedNurse);
        }
    }
}
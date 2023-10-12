using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using Xunit;
namespace TestProject1.DoctorTests
{
    public class DoctorTests : Mock
    {
        [Fact]
        public async Task CreateDoctor_ReturnDoctorDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var doctorService = new DoctorService(_db);
            var newDoctor = new InDoctorDTO
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Gender = doctor.Gender,
                ContactNumber = doctor.ContactNumber,
                Speciality = doctor.Speciality,
                DepartmentId = department.Id
            };
            var createdDoctor = await doctorService.Create(newDoctor);
            Assert.NotNull(createdDoctor);
            Assert.Equal("Doctor Doctor", createdDoctor.FullName);
            Assert.Equal("Male", createdDoctor.Gender);
            Assert.Equal("123456789", createdDoctor.ContactNumber);
            Assert.Equal("test", createdDoctor.Speciality);
        }
        [Fact]
        public async Task GetDoctors_ReturnListOfDoctorDTOs()
        {
            var hospital = await CreateAndSaveTestHospital();
            var dep1 = await CreateAndSaveTestDepartment(hospital.Id);
            var dep2 = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor1 = await CreateAndSaveTestDoctor(dep1.Id);
            var doctor2 = await CreateAndSaveTestDoctor(dep2.Id);
            var doctorService = new DoctorService(_db);
            var retrievedDoctorsDto = await doctorService.GetDoctors();
            Assert.NotNull(retrievedDoctorsDto);
            Assert.Equal(2, retrievedDoctorsDto.Count);
            Assert.Contains(retrievedDoctorsDto, dto => dto.Id == doctor1.Id);
            Assert.Contains(retrievedDoctorsDto, dto => dto.Id == doctor2.Id);
        }
        [Fact]
        public async Task GetDoctor_ReturnDoctorDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var doctorService = new DoctorService(_db);
            var retrievedDoctor = await doctorService.GetDoctor(doctor.Id);
            Assert.NotNull(retrievedDoctor);
            Assert.Equal(doctor.Id, retrievedDoctor.Id);
            Assert.Equal("Doctor", retrievedDoctor.FirstName);
            Assert.Equal("Doctor", retrievedDoctor.LastName);
            Assert.Equal("Male", retrievedDoctor.Gender);
            Assert.Equal("123456789", retrievedDoctor.ContactNumber);
            Assert.Equal("test", retrievedDoctor.Speciality);
            Assert.Equal(department.Id, retrievedDoctor.DepartmentId);
        }
        [Fact]
        public async Task UpdateDoctor_ReturnUpdatedDoctorDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var doctorService = new DoctorService(_db);
            var updatedDoctor = new InDoctorDTO
            {
                FirstName = "update",
                LastName = "update",
                Gender = "Female",
                ContactNumber = "987654321",
                Speciality = "test",
                DepartmentId = department.Id
            };
            var updatedDoctorDto = await doctorService.UpdateDoctor(doctor.Id, updatedDoctor);
            Assert.NotNull(updatedDoctorDto);
            Assert.Equal("update", updatedDoctorDto.FirstName);
            Assert.Equal("update", updatedDoctorDto.LastName);
            Assert.Equal("Female", updatedDoctorDto.Gender);
            Assert.Equal("987654321", updatedDoctorDto.ContactNumber);
            Assert.Equal("test", updatedDoctorDto.Speciality);
            Assert.Equal(department.Id, updatedDoctorDto.DepartmentId);
        }
        [Fact]
        public async Task DeleteDoctor_ReturnDeletedDoctor()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var doctorService = new DoctorService(_db);
            await doctorService.Delete(doctor.Id);
            var deletedDoctor = await _db.Doctors.FindAsync(doctor.Id);
            Assert.Null(deletedDoctor);
        }

        [Fact]
        public async Task GetAppointmentsForDoctor_ReturnAppointmentsForDoctor()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var appointment1 = await CreateAndSaveTestAppointment(doctor.Id, patient.Id);
            var appointment2 = await CreateAndSaveTestAppointment(doctor.Id, patient.Id);
            var doctorService = new DoctorService(_db);
            var retrievedDoctor = await doctorService.GetAppointmentsForDoctor(doctor.Id);
            Assert.Equal(2, retrievedDoctor.Count);
        }
    }
}

using Hospital_System.Models;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Hospital;
using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.DTOs.Room;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace TestProject1.PatientTests
{
    public class PatientTest : Mock
    {
        private IPatient BuildRepository()
        {
            return new PatientService(_db);
        }
        [Fact]
        public async Task ValidiateCRUDoperation()
        {
            // Arrange
            var patientService = BuildRepository();
            var hospitalService = new HospitalService(_db); // BuildHospitalService() if it exists
            var departmentService = new DepartmentService(_db); // BuildDepartmentService() if it exists
            var roomService = new RoomService(_db);
            var hospital = new OutHospitalDTO
            {
                HospitalName = "Al Bassher",
                Address = "Amman",
                ContactNumber = "097667889"
            };
            var hospitalResult = await hospitalService.Create(hospital);
            var dpartment = new InDepartmentDTO
            {
                DepartmentName = "Bones",
                HospitalID = hospitalResult.Id
            };
            var departmentResult = await departmentService.CreateDepartment(dpartment);
            var room = new OutRoomDTO
            {
                //Id = 1,
                RoomNumber = "332",
                RoomAvailability = true,
                NumberOfBeds = 2,
                DepartmentId = departmentResult.Id
            };
            var roomResult = await roomService.CreateRoom(room);
            var inputPatient = new InPatientDTO
            {
                FirstName = "Belal",
                LastName = "Al Msalma",
                DoB = DateTime.Now,
                Gender = "Male",
                Address = "Amman",
                ContactNumber = "0798765432",
                RoomId = roomResult.Id
            };
            var inputPatient1 = new InPatientDTO
            {
                FirstName = "Belal",
                LastName = "Al Msalma",
                DoB = DateTime.Now,
                Gender = "Male",
                Address = "Amman",
                ContactNumber = "0798765432",
                RoomId = roomResult.Id
            };
            var inputPatient2 = new InPatientDTO
            {
                FirstName = "Belal",
                LastName = "Al Msalma",
                DoB = DateTime.Now,
                Gender = "Male",
                Address = "Amman",
                ContactNumber = "0798765432",
                RoomId = roomResult.Id
            };
            var inputPatient3 = new InPatientDTO
            {
                FirstName = "Belal",
                LastName = "Al Msalma",
                DoB = DateTime.Now,
                Gender = "Male",
                Address = "Amman",
                ContactNumber = "0798765432"
            };
            var inputPatient4 = new InPatientDTO
            {
                FirstName = "Belal",
                LastName = "Al Msalma",
                DoB = DateTime.Now,
                Gender = "Male",
                Address = "Amman",
                ContactNumber = "0798765432",
                RoomId = 2
            };
            // Act
            var result = await patientService.Create(inputPatient);
            var result1 = await patientService.Create(inputPatient1);
            var result2 = await patientService.Create(inputPatient2);
            var result3 = await patientService.Create(inputPatient3);
            var exception = await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await patientService.Create(inputPatient4);
            });
            var GetResult = await patientService.GetPatient(result.Id);
            var getAllPatient = await patientService.GetPatients();
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result1);
            Assert.NotNull(result3);
            Assert.Equal(GetResult.Id, result.Id);
            Assert.Equal(inputPatient.FirstName, result.FirstName);
            Assert.Null(result2);
            Assert.Null(result3.RoomId);
            Assert.Equal(getAllPatient.Count, 3);
            Assert.Equal("Room does not exist.", exception.Message);
            await patientService.Delete(result3.Id);
            var GetResult3 = await patientService.GetPatient(result3.Id);
            var getAllPatientAfterDeleation = await patientService.GetPatients();
            Assert.Null(GetResult3);
            Assert.Equal(getAllPatientAfterDeleation.Count, 2);
            var updatePatient1 = new InPatientDTO
            {
                FirstName = "Mhamad Belal",
                LastName = "Al Msalma",
                DoB = DateTime.Now,
                Gender = "Male",
                Address = "Irbid",
                ContactNumber = "0798765432"
            };
            var updateResult = await patientService.UpdatePatient(result1.Id, updatePatient1);
            Assert.NotEqual(updatePatient1.FirstName, result3.FirstName);
            Assert.Null(updatePatient1.RoomId);
            // Assert other properties
        }
    }
}
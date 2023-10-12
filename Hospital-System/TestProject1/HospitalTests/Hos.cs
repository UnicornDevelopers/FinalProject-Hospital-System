using Hospital_System.Models;
using Hospital_System.Models.DTOs.Hospital;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TestProject1.HospitalTests
{
    public class Hos : Mock
    {
        private IHospital BuildRepository()
        {
            return new HospitalService(_db);
        }
        [Fact]
        public async Task Create_ValidHospital_ReturnsHospital()
        {
            // Arrange
            var hospitalService = BuildRepository();
            var inputHospital = new OutHospitalDTO
            {
                HospitalName = "Al Basheer",
                Address = "Amman",
                ContactNumber = "0798765432"
            };
            // Act
            var result = await hospitalService.Create(inputHospital);
            var GetResult = await hospitalService.GetHospital(inputHospital.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(inputHospital.Id, result.Id);
            Assert.Equal(GetResult.Id, result.Id);
            Assert.Equal(inputHospital.HospitalName, result.HospitalName);
            // Assert other properties
        }
        [Fact]
        public async Task Update_ValidHospital_ReturnsHospital()
        {
            // Arrange
            var hospitalService = BuildRepository();
            var inputHospital = new OutHospitalDTO
            {
                HospitalName = "Al Basheer",
                Address = "Amman",
                ContactNumber = "0798765432"
            };
            // Act
            var result = await hospitalService.Create(inputHospital);
            var inputHospitalUpdate = new OutHospitalDTO
            {
                Id = inputHospital.Id,
                HospitalName = "Al Basheer",
                Address = "Irbid",
                ContactNumber = "0798765431"
            };
            var updateResult = await hospitalService.UpdateHospital(inputHospital.Id, inputHospitalUpdate);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(inputHospital.Id, result.Id);
            Assert.Equal(updateResult.Id, result.Id);
            Assert.NotEqual(updateResult.Address, result.Address);
            // Assert other properties
        }
        [Fact]
        public async Task Delete_ValidHospital()
        {
            // Arrange
            var hospitalService = BuildRepository();
            var inputHospital = new OutHospitalDTO
            {
                HospitalName = "Al Basheer",
                Address = "Amman",
                ContactNumber = "0798765432"
            };
            // Act
            var result = await hospitalService.Create(inputHospital);
            Assert.NotNull(result);
            Assert.Equal(inputHospital.Id, result.Id);
            await hospitalService.Delete(result.Id);
            var GetResult = await hospitalService.GetHospital(result.Id);
            // Assert
            Assert.Null(GetResult);
            // Assert other properties
        }
        [Fact]
        public async Task Get_All_Hospitals()
        {
            var hospitalService = BuildRepository();
            var inputHospital1 = new OutHospitalDTO
            {
                HospitalName = "Al Kabber",
                Address = "Irbid",
                ContactNumber = "0798765432"
            };
            var inputHospital2 = new OutHospitalDTO
            {
                HospitalName = "Al Basheer",
                Address = "Irbid",
                ContactNumber = "0798765432"
            };
            var inputHospital3 = new OutHospitalDTO
            {
                HospitalName = "Al Shagger",
                Address = "Irbid",
                ContactNumber = "0798765432"
            };
            await hospitalService.Create(inputHospital1);
            await hospitalService.Create(inputHospital2);
            await hospitalService.Create(inputHospital3);
            var result = await hospitalService.GetHospitals();
            Assert.NotNull(result);
            Assert.Equal(result.Count, 3);
        }


        [Fact]
        public async Task Get_All_Departments_In_Hopsital()
        {
            var hospitalService = BuildRepository();

            var hopsital = CreateAndSaveTestHospital();
            var dep1 = CreateAndSaveTestDepartment(hopsital.Id);
            var dep2 = CreateAndSaveTestDepartment(hopsital.Id);
            var dep3 = CreateAndSaveTestDepartment(hopsital.Id);
            var dep4 = CreateAndSaveTestDepartment(hopsital.Id);

            var result = await hospitalService.GetDepartmentsInHospital(hopsital.Id);

            Assert.NotNull(result);
            Assert.Equal(result.Count, 4);
        }

    }
}
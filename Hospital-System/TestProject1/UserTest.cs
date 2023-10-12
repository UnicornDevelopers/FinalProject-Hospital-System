using Hospital_System.Controllers;
using Hospital_System.Models.DTOs.User;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Hospital_System.Models;
using Hospital_System.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace TestProject1
{
    public class User : Hospital_System.Tests.Mocks.Mock
    {
        [Fact]
        public async Task Register_User_As_Doctor()
        {
            // Arrange
            var userMock = new Mock<IUser>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(MockBehavior.Strict, null, null, null, null, null, null, null, null);
            var jwtTokenServiceMock = new Mock<JwtTokenService>(null, null);
            var roles = new List<Claim> { new Claim(ClaimTypes.Role, "Doctor") };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(roles));
            var controller = new UsersController(userMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrincipal
                }
            };
            var registerDto = new DoctorRegistrationDTO
            {
                UserName = "TestDoctor",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Password = "P@ssw0rd",
                Roles = new List<string> { "Doctor" } // Adjust the roles as needed
            };
            var expectedResult = new UserDTO
            {
                Id = "UserId",
                UserName = registerDto.UserName,
                Token = "MockedToken",
                Roles = new List<string> { "Doctor" } // Adjust the roles as needed
            };
            userMock.Setup(u => u.RegisterDoctor(It.IsAny<DoctorRegistrationDTO>(), It.IsAny<ModelStateDictionary>(), It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(expectedResult);
            // Act
            var result = await controller.DoctorRegister(registerDto);
            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);
            Assert.Equal(expectedResult.UserName, userDto.UserName);
            Assert.Equal(expectedResult.Roles, userDto.Roles);
        }
        [Fact]
        public async Task Register_User_As_Nurse()
        {
            // Arrange
            var userMock = new Mock<IUser>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(MockBehavior.Strict, null, null, null, null, null, null, null, null);
            var jwtTokenServiceMock = new Mock<JwtTokenService>(null, null);
            var roles = new List<Claim> { new Claim(ClaimTypes.Role, "Nurse") };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(roles));
            var controller = new UsersController(userMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrincipal
                }
            };
            var registerDto = new RegisterNurseDTO
            {
                UserName = "TestNurse",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Password = "P@ssw0rd",
                Roles = new List<string> { "Nurse" } // Adjust the roles as needed
            };
            var expectedResult = new UserDTO
            {
                Id = "UserId",
                UserName = registerDto.UserName,
                Token = "MockedToken",
                Roles = new List<string> { "Nurse" } // Adjust the roles as needed
            };
            userMock.Setup(u => u.RegisterNurse(It.IsAny<RegisterNurseDTO>(), It.IsAny<ModelStateDictionary>(), It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(expectedResult);
            // Act
            var result = await controller.NurseRegister(registerDto);
            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);
            Assert.Equal(expectedResult.UserName, userDto.UserName);
            Assert.Equal(expectedResult.Roles, userDto.Roles);
        }
        [Fact]
        public async Task Register_User_As_Admin()
        {
            // Arrange
            var userMock = new Mock<IUser>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(MockBehavior.Strict, null, null, null, null, null, null, null, null);
            var jwtTokenServiceMock = new Mock<JwtTokenService>(null, null);
            var roles = new List<Claim> { new Claim(ClaimTypes.Role, "Admin") };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(roles));
            var controller = new UsersController(userMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrincipal
                }
            };
            var registerDto = new RegisterUserDTO
            {
                UserName = "TestAdmin",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Password = "P@ssw0rd",
                Roles = new List<string> { "Admin" } // Adjust the roles as needed
            };
            var expectedResult = new UserDTO
            {
                Id = "UserId",
                UserName = registerDto.UserName,
                Token = "MockedToken",
                Roles = new List<string> { "Admin" } // Adjust the roles as needed
            };
            userMock.Setup(u => u.Register(It.IsAny<RegisterUserDTO>(), It.IsAny<ModelStateDictionary>(), It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(expectedResult);
            // Act
            var result = await controller.Register(registerDto);
            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);
            Assert.Equal(expectedResult.UserName, userDto.UserName);
            Assert.Equal(expectedResult.Roles, userDto.Roles);
        }
        [Fact]
        public async Task Register_User_As_Patient()
        {
            // Arrange
            var userMock = new Mock<IUser>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(MockBehavior.Strict, null, null, null, null, null, null, null, null);
            var jwtTokenServiceMock = new Mock<JwtTokenService>(null, null);
            var roles = new List<Claim> { new Claim(ClaimTypes.Role, "Patient") };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(roles));
            var controller = new UsersController(userMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrincipal
                }
            };
            var registerDto = new RegisterPatientDTO
            {
                UserName = "TestPatient",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Password = "P@ssw0rd",
                Roles = new List<string> { "Patient" } // Adjust the roles as needed
            };
            var expectedResult = new UserDTO
            {
                Id = "UserId",
                UserName = registerDto.UserName,
                Token = "MockedToken",
                Roles = new List<string> { "Patient" } // Adjust the roles as needed
            };
            userMock.Setup(u => u.RegisterPatient(It.IsAny<RegisterPatientDTO>(), It.IsAny<ModelStateDictionary>(), It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(expectedResult);
            // Act
            var result = await controller.PatientRegister(registerDto);
            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);
            Assert.Equal(expectedResult.UserName, userDto.UserName);
            Assert.Equal(expectedResult.Roles, userDto.Roles);
        }
        [Fact]
        public async Task SignIn_User_Successfully()
        {
            // Arrange
            var expectedResult = new UserDTO
            {
                Id = "UserId",
                UserName = "TestUser",
                Token = "MockedToken",
                Roles = new List<string> { "Admin" }
            };
            var userMock = SetupUserMock(expectedResult);
            var controller = new UsersController(userMock);
            var loginDto = new LoginDTO
            {
                UserName = "TestUser",
                Password = "P@ssw0rd"
            };
            // Act
            var result = await controller.Login(loginDto);
            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);
            Assert.Equal(expectedResult.UserName, userDto.UserName);
            Assert.Equal(expectedResult.Roles, userDto.Roles);
        }
    }
}
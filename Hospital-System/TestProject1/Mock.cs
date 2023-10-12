using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.User;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using TestProject1.DoctorTests;
using static Hospital_System.Models.Nurse;
namespace Hospital_System.Tests.Mocks
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly HospitalDbContext _db;
        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseSqlite(_connection)
                .Options;
            _db = new HospitalDbContext(options);
            _db.Database.EnsureCreated();
        }
        //Room
        protected async Task<Room> CreateAndSaveTestRoom(int departmentId)
        {
            var room = new Room
            {
                RoomNumber = "101",
                RoomAvailability = true,
                NumberOfBeds = 5,
                DepartmentId = departmentId // Make sure you set the correct department Id
            };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            return room;
        }
        //Hospital
        protected async Task<Hospital> CreateAndSaveTestHospital()
        {
            var hospital = new Hospital { HospitalName = "Test Hospital", Address = "Amman", ContactNumber = "123456789" };
            _db.Hospitals.Add(hospital);
            await _db.SaveChangesAsync();
            return hospital;
        }
        //Department
        protected async Task<Department> CreateAndSaveTestDepartment(int hospitalId)
        {
            var department = new Department { DepartmentName = "Test Department", HospitalID = hospitalId };
            _db.Departments.Add(department);
            await _db.SaveChangesAsync();
            return department;
        }
        //Patient
        protected async Task<Patient> CreateAndSaveTestPatient(int roomId)
        {
            var patient = new Patient { FirstName = "TestFirstName", LastName = "TestLastName", DoB = DateTime.Now, Gender = "GenderTest", ContactNumber = "0789513213312", Address = "Address", RoomId = roomId };
            _db.Patients.Add(patient);
            await _db.SaveChangesAsync();
            return patient;
        }
        //Nurse
        protected async Task<Nurse> CreateAndSaveTestNurse(int departmentId)
        {
            var nurse = new Nurse
            {
                FirstName = "Test",
                LastName = "Nurse",
                Gender = "Female",
                ContactNumber = "123456789",
                shift = Shift.Morning,
                DepartmentId = departmentId
            };
            _db.Nurses.Add(nurse);
            await _db.SaveChangesAsync();
            return nurse;
        }
        //Doctor
        protected async Task<Doctor> CreateAndSaveTestDoctor(int departmentId)
        {
            var doctor = new Doctor
            {
                FirstName = "Doctor",
                LastName = "Doctor",
                Gender = "Male",
                ContactNumber = "123456789",
                Speciality = "test",
                DepartmentId = departmentId
            };
            _db.Doctors.Add(doctor);
            await _db.SaveChangesAsync();
            return doctor;
        }
        protected async Task<MedicalReport> CreateAndSaveTestMedicalReport(int patientId, int doctorId)
        {
            DateTime date = new DateTime(2020, 5, 11);
            var medicalReport = new MedicalReport { ReportDate = new DateTime(2020, 5, 11), Description = "Test Description", PatientId = patientId, DoctorId = doctorId };
            _db.MedicalReports.Add(medicalReport);
            await _db.SaveChangesAsync();
            return medicalReport;
        }
        protected async Task<Medicine> CreateAndSaveTestMedicine(int medicalReportId)
        {
            var medicine = new Medicine { MedicineName = "Test MedicineName", Portion = "Test Portion", MedicalReportId = medicalReportId };
            _db.Medicines.Add(medicine);
            await _db.SaveChangesAsync();
            return medicine;
        }
        //Appointment
        protected async Task<Appointment> CreateAndSaveTestAppointment(int patientId, int doctorId)
        {
            var appointment = new Appointment { DateOfAppointment = new DateTime(2020, 5, 11), PatientId = patientId, DoctorId = doctorId };
            _db.Appointments.Add(appointment);
            await _db.SaveChangesAsync();
            return appointment;
        }
        protected static IUser SetupUserMock(UserDTO expected)
        {
            var userMock = new Mock<IUser>();

            userMock.Setup(u => u.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(expected);

            return userMock.Object;
        }
        // Other methods for creating and saving test data
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}


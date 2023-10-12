using Hospital_System.Models;
using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.Services;
using Hospital_System.Tests.Mocks;
using Xunit;
namespace TestProject1.App
{
    public class App : Mock
    {
        [Fact]
        public async Task CreateAppointmentReturnAppointmentDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var appointment = await CreateAndSaveTestAppointment(doctor.Id, patient.Id);
            var appointmentService = new AppointmentService(_db);
            var newAppointment = new InAppoinmentDTO
            {
                DateOfAppointment = appointment.DateOfAppointment,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId
            };
            var createAppointment = await appointmentService.CreateAppointment(newAppointment);
            Assert.NotNull(createAppointment);
            Assert.Equal(new DateTime(2020, 5, 11), createAppointment.DateOfAppointment);
            Assert.Equal(patient.Id, createAppointment.PatientId);
            Assert.Equal(doctor.Id, createAppointment.DoctorId);
        }
        [Fact]
        public async Task GetAppointmentsReturnListOfAppointmentDTOs()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var department2 = await CreateAndSaveTestDepartment(hospital.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var room2 = await CreateAndSaveTestRoom(department2.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var doctor2 = await CreateAndSaveTestDoctor(department2.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var patient2 = await CreateAndSaveTestPatient(room2.Id);
            var appointment = await CreateAndSaveTestAppointment(doctor.Id, patient.Id);
            var appointment2 = await CreateAndSaveTestAppointment(doctor2.Id, patient2.Id);
            var appointmentService = new AppointmentService(_db);
            var retrievedAppointmentsDto = await appointmentService.GetAppointments();
            Assert.NotNull(retrievedAppointmentsDto);
            Assert.Equal(2, retrievedAppointmentsDto.Count);
            Assert.Contains(retrievedAppointmentsDto, dto => dto.Id == appointment.Id);
            Assert.Contains(retrievedAppointmentsDto, dto => dto.Id == appointment2.Id);
        }
        [Fact]
        public async Task GetAppointmentReturnAppointmentDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var appointment = await CreateAndSaveTestAppointment(doctor.Id, patient.Id);
            var appointmentService = new AppointmentService(_db);
            var retrievedAppointment = await appointmentService.GetAppointment(appointment.Id);
            Assert.NotNull(retrievedAppointment);
            Assert.Equal(new DateTime(2020, 5, 11), retrievedAppointment.DateOfAppointment);
            Assert.Equal(patient.Id, retrievedAppointment.PatientId);
            Assert.Equal(doctor.Id, retrievedAppointment.DoctorId);
        }
        [Fact]
        public async Task UpdateAppointmentReturnUpdatedAppointmentDTO()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var appointment = await CreateAndSaveTestAppointment(doctor.Id, patient.Id);
            var appointmentService = new AppointmentService(_db);
            var updatedAppointment = new InAppoinmentDTO
            {
                DateOfAppointment = new DateTime(2023, 3, 3),
                PatientId = patient.Id,
                DoctorId = doctor.Id
            };
            var updatedAppoinmentDto = await appointmentService.UpdateAppointment(appointment.Id, updatedAppointment);
            Assert.NotNull(updatedAppoinmentDto);
            Assert.Equal(new DateTime(2023, 3, 3), updatedAppoinmentDto.DateOfAppointment);
        }
        [Fact]
        public async Task DeleteDoctorReturnDeletedDoctor()
        {
            var hospital = await CreateAndSaveTestHospital();
            var department = await CreateAndSaveTestDepartment(hospital.Id);
            var room = await CreateAndSaveTestRoom(department.Id);
            var doctor = await CreateAndSaveTestDoctor(department.Id);
            var patient = await CreateAndSaveTestPatient(room.Id);
            var appointment = await CreateAndSaveTestAppointment(doctor.Id, patient.Id);
            var appointmentService = new AppointmentService(_db);
            await appointmentService.DeleteAppointment(appointment.Id);
            var deletedAppointment = await _db.Appointments.FindAsync(appointment.Id);
            Assert.Null(deletedAppointment);
        }
    }
}
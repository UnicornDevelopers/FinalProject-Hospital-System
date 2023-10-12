using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.MedicalReport;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models.DTOs
{
    public class DoctorDTO
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Speciality { get; set; }

        public int DepartmentId { get; set; }



        //Nav
        public List<GetAppointmentDTO>? Appointments { get; set; }
        public List<InMedicalReportDTO>? medicalReports { get; set; }
        public OutDepartmentDTO? department { get; set; }


    }
}


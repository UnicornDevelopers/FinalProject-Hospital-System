using Hospital_System.Models.DTOs.Patient;

namespace Hospital_System.Models.DTOs.MedicalReport
{
    public class InMedicalReportDTO
    {

        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string Description { get; set; }
        public int DoctorId { get; set; }

        public int PatientId { get; set; }

       
    }
}

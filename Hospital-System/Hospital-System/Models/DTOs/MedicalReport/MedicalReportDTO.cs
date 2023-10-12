using System.ComponentModel.DataAnnotations.Schema;
using Hospital_System.Models.DTOs.Medicine;
using Hospital_System.Models.DTOs.Patient;

namespace Hospital_System.Models.DTOs.MedicalReport
{
    public class MedicalReportDTO
    {

        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string Description { get; set; }
        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        //Nav


        public List<OutMedicineDTO>? Medicines { get; set; }
        public OutDocDTO? doctor { get; set; }
        public PatientDTO? patient { get; set; }



    }
}

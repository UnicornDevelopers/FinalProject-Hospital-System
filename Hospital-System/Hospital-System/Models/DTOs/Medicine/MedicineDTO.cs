using Hospital_System.Models.DTOs.MedicalReport;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models.DTOs
{
    public class MedicineDTO
    {

        public int Id { get; set; }
        public string MedicineName { get; set; }
        public string Portion { get; set; }
        public int? MedicalReportId { get; set; }



        //Nav
        public MedicalReportDTO? medicalReport { get; set; }


    }
}

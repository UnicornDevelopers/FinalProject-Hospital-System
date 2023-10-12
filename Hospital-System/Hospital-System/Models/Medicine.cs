using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public string Portion { get; set; }
        public int? MedicalReportId { get; set; }



        //Nav
        [ForeignKey("MedicalReportId")]
        public MedicalReport? medicalReport { get; set; }


    }
}

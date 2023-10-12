using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class MedicalReport
    {
        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string Description { get; set; }
        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        //Nav


        public List<Medicine>? Medicines { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor? doctor { get; set; }
        [ForeignKey("PatientId")]
        public Patient? patient { get; set; }

    }
}

namespace Hospital_System.Models.DTOs.Medicine
{
    public class InMedicineDTO
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public string Portion { get; set; }
        public int? MedicalReportId { get; set; }

    }
}

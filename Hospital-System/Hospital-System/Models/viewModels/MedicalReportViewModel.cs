namespace Hospital_System.Models.ViewModels
{
        public class MedicalReportViewModel
        {
        public DateTime ReportDate { get; set; }
        public string Description { get; set; }
        public int PatientId { get; set; }
        public List<MedicineViewModel>? Medicines { get; set; }
        }

        public class MedicineViewModel
        {
            public int MedicineId { get; set; } // This should be the ID of the selected medicine
            public int TimesInDay { get; set; } // This should be the selected value for times in a day
            public string MedicinePortion { get; set; }
        }
    
}

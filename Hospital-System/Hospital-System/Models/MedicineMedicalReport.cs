using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class MedicineMedicalReport
    {
        public int MedicalReportID { get; set; }

        public int MedicineID { get; set; }

        public string MedicinePortion { get; set; }

        public MedicalAbbreviations TimesInDay { get; set; }


        // Navigation properties
        [ForeignKey("MedicalReportID")]
        public virtual MedicalReport? MedicalReport { get; set; }

        [ForeignKey("MedicineID")]
        public virtual Medicine? Medicine { get; set; }
    }

    public enum MedicalAbbreviations
    {
        QD,  // once a day
        BID, // twice a day
        TID, // three times a day
        QID, // four times daily
        QHS, // every night at bedtime
        Q4H, // every four hours
        Q6H, // every six hours
        Q8H  // every eight hours
    }
}

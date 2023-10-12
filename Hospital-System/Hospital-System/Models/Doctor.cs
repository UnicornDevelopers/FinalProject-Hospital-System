using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class Doctor
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Speciality { get; set; }

        public int DepartmentId { get; set; }
        public string? UserId { get; set; }
      



        //Nav
        public List<Appointment> Appointments { get; set; }
        public List<MedicalReport> medicalReports { get; set; }
        [ForeignKey("DepartmentId")]
        public Department department { get; set; }



    }
}

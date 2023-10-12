using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int HospitalID { get; set; }

        //Nav
        [ForeignKey("HospitalID")]
        public Hospital? Hospital { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Nurse> Nurses { get; set; }



    }
}

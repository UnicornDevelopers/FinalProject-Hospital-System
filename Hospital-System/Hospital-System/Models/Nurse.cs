using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models
{
    public class Nurse
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public Shift shift { get; set; }

        public int DepartmentId { get; set; }
        public string? UserId { get; set; }
       
        //Nav
        [ForeignKey("DepartmentId")]
        public Department department { get; set; }


        public enum Shift
        {
            Morning,
            Night,
            Afternoon,

        }


    }
}

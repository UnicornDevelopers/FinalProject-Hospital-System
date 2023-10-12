namespace Hospital_System.Models.DTOs.Doctor
{
    public class InDoctorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Speciality { get; set; }
        public string FullName { get; set; }

        public int DepartmentId { get; set; }
    }
}

namespace Hospital_System.Models.DTOs
{
    ///// Represents a Data Transfer Object (DTO) for representing basic doctor information.
    ///use it when i want Create new entity of doctor and when Get List of Doctors

    public class OutDocDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }

        public string ContactNumber { get; set; }
        public string Speciality { get; set; }

        public string DepartmentName { get; set; }
    }
}

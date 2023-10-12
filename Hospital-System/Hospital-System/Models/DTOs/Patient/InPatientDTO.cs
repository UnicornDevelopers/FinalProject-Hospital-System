namespace Hospital_System.Models.DTOs.Patient
{
    public class InPatientDTO
    {
        
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DoB { get; set; }
            public string Gender { get; set; }
            public string ContactNumber { get; set; }
            public string Address { get; set; }

            public int? RoomId { get; set; }
     }
    
}

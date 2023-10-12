namespace Hospital_System.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string HospitalName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }



        //Nav

        public List<Department>? Departments { get; set; }
    }
}

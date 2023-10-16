namespace Hospital_System.Models.DTOs.Department
{
	public class InDepartmentDTO
	{
		public int Id { get; set; }
		public string DepartmentName { get; set; }
		public int HospitalID { get; set; }
		public string? Image { get; set; }
		public string? Description { get; set; }

	}
}
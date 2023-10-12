using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Hospital;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.DTOs.Room;

namespace Hospital_System.Models.DTOs.Department
{
    public class GetDeptmartmentDTO
    {


        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int HospitalID { get; set; }

        //Nav
        public List<OutRoomDTO>? Rooms { get; set; }
        public List<OutDoctorDTO>? Doctors { get; set; }
        public List<InNurseDTO>? Nurses { get; set; }
    }
}

using Hospital_System.Models.DTOs.Hospital;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.DTOs.Room;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_System.Models.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int HospitalID { get; set; } 

        //Nav
        public OutHospitalDTO? hospital { get; set; }
        public List<OutRoomDTO>? Rooms { get; set; }
        public List<OutDocDTO>? Doctors { get; set; }
        public List<InNurseDTO>? Nurses { get; set; }



    }
}

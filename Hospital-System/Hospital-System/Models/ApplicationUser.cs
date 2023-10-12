using Hospital_System.Models.DTOs.User;
using Hospital_System.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Hospital_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Gender { get; set; }
        //public DateTime? YoB { get; set; }
        //public string UserId { get; set; }

        [NotMapped]
        public IList<string> Roles { get; set; }


    }
}

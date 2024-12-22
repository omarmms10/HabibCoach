using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HabibCoach.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(250)]
        public string ProfilePicture { get; set; }

        // Foreign key for Programe
        [ForeignKey("Programe")]
        public int? ProgramId { get; set; }

        // Navigation property for Programe
        public Programe Programe { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace HabibCoach.Models
{
    public class Programe
    {
        [Key]
        public int ProgramId { get; set; }

        [Required]
        [MaxLength(100)]
        public string WorkoutProgramTitle { get; set; }

        [Required]
        [MaxLength(50)]
        public string Level { get; set; }

        [MaxLength(500)]
        public string ProgramNote { get; set; }

        [Range(1, 365)]
        public int ProgramDuration { get; set; } // In days

        public ICollection<DailyRoutine> DailyRoutines { get; set; }
    }
}

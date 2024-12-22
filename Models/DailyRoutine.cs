using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HabibCoach.Models
{
    public class DailyRoutine
    {
        public int DailyRoutineId { get; set; }

        [Required]
        [ForeignKey("Program")]
        public int ProgramId { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoutineTitle { get; set; }

        [MaxLength(500)]
        public string RoutineNote { get; set; }

        public Programe Programe { get; set; }
        public ICollection<RoutineExercise> RoutineExercises { get; set; }
    }
}

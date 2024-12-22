using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HabibCoach.Models
{
    public class RoutineExercise
    {
        public int RoutineExerciseId { get; set; }

        [Required]
        [ForeignKey("DailyRoutine")]
        public int DailyRoutineId { get; set; }

        [Required]
        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }

        [Range(0, 600)]
        public int RestTimer { get; set; } // In seconds

        [Range(1, 100)]
        public int SetKg { get; set; } // Weight in kg

        [Range(1, 100)]
        public int Reps { get; set; } // Number of repetitions

        public DailyRoutine DailyRoutine { get; set; }
        public Exercise Exercise { get; set; }
    }
}

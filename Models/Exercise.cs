using System.ComponentModel.DataAnnotations;

namespace HabibCoach.Models
{
    public class Exercise
    {
        public int ExerciseId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Image { get; set; }

        [Required]
        [MaxLength(100)]
        public string ExerciseName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ExerciseType { get; set; }

        [MaxLength(50)]
        public string Equipment { get; set; }

        [Required]
        [MaxLength(100)]
        public string PrimaryMuscleGroup { get; set; }

        [MaxLength(100)]
        public string OtherMuscles { get; set; }

        [MaxLength(1000)]
        public List<Instruction> Instructions { get; set; }


        [MaxLength(250)]
        public string VideoAttachment { get; set; }

        public ICollection<RoutineExercise> RoutineExercises { get; set; }
    }
}

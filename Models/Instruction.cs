namespace HabibCoach.Models
{
    public class Instruction
    {
        public int InstructionId { get; set; }
        public int ExerciseId { get; set; }
        public string Content { get; set; }

        // Navigation property back to Exercise
        public Exercise Exercise { get; set; }
    }
}

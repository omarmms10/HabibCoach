using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HabibCoach.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Programe> Programes { get; set; }
        public DbSet<DailyRoutine> DailyRoutines { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<RoutineExercise> RoutineExercises { get; set; }
        public DbSet<Instruction> Instructions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring many-to-many relationships
            modelBuilder.Entity<RoutineExercise>()
                .HasOne(re => re.DailyRoutine)
                .WithMany(dr => dr.RoutineExercises)
                .HasForeignKey(re => re.DailyRoutineId);

            modelBuilder.Entity<RoutineExercise>()
                .HasOne(re => re.Exercise)
                .WithMany(e => e.RoutineExercises)
                .HasForeignKey(re => re.ExerciseId);

            modelBuilder.Entity<Instruction>()
                .HasOne(i => i.Exercise)
                .WithMany(e => e.Instructions)
                .HasForeignKey(i => i.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

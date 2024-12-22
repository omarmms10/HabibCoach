using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HabibCoach.Models;

namespace HabibCoach.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            return View(await _context.Exercises.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var exercise = await _context.Exercises
                .Include(e => e.Instructions) // Include related instructions
                .FirstOrDefaultAsync(e => e.ExerciseId == id);

            if (exercise == null)
            {
                return NotFound();
            }

            return Json(new
            {
                exerciseName = exercise.ExerciseName,
                equipment = exercise.Equipment,
                primaryMuscleGroup = exercise.PrimaryMuscleGroup,
                exerciseType = exercise.ExerciseType,
                instructions = exercise.Instructions.Select(i => i.Content) // Return as a list of strings
            });
        }



        // GET: Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        
        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseId,Image,ExerciseName,ExerciseType,Equipment,PrimaryMuscleGroup,OtherMuscles,VideoAttachment")] Exercise exercise, List<string> Instructions)
        {
            if (ModelState.IsValid)
            {
                // Add instructions
                exercise.Instructions = Instructions
                    .Where(i => !string.IsNullOrWhiteSpace(i))
                    .Select(i => new Instruction { Content = i })
                    .ToList();

                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }


        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }


        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseId,Image,ExerciseName,ExerciseType,Equipment,PrimaryMuscleGroup,OtherMuscles,VideoAttachment")] Exercise exercise, List<string> Instructions)
        {
            if (id != exercise.ExerciseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingExercise = await _context.Exercises
                        .Include(e => e.Instructions)
                        .FirstOrDefaultAsync(e => e.ExerciseId == id);

                    if (existingExercise == null)
                    {
                        return NotFound();
                    }

                    // Update exercise fields
                    existingExercise.Image = exercise.Image;
                    existingExercise.ExerciseName = exercise.ExerciseName;
                    existingExercise.ExerciseType = exercise.ExerciseType;
                    existingExercise.Equipment = exercise.Equipment;
                    existingExercise.PrimaryMuscleGroup = exercise.PrimaryMuscleGroup;
                    existingExercise.OtherMuscles = exercise.OtherMuscles;
                    existingExercise.VideoAttachment = exercise.VideoAttachment;

                    // Remove old instructions and add new ones
                    _context.Instructions.RemoveRange(existingExercise.Instructions);
                    existingExercise.Instructions = Instructions
                        .Where(i => !string.IsNullOrWhiteSpace(i))
                        .Select(i => new Instruction { Content = i })
                        .ToList();

                    _context.Update(existingExercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.ExerciseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }


        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .Include(e => e.Instructions) // Include related instructions
                .FirstOrDefaultAsync(m => m.ExerciseId == id);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }
    }
}

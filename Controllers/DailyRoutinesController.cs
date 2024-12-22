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
    public class DailyRoutinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DailyRoutinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DailyRoutines
        public async Task<IActionResult> Index()
        {
            return View(await _context.DailyRoutines.ToListAsync());
        }

        // GET: DailyRoutines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyRoutine = await _context.DailyRoutines
                .FirstOrDefaultAsync(m => m.DailyRoutineId == id);
            if (dailyRoutine == null)
            {
                return NotFound();
            }

            return View(dailyRoutine);
        }

        // GET: DailyRoutines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DailyRoutines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DailyRoutineId,ProgramId,RoutineTitle,RoutineNote")] DailyRoutine dailyRoutine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailyRoutine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyRoutine);
        }

        // GET: DailyRoutines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyRoutine = await _context.DailyRoutines.FindAsync(id);
            if (dailyRoutine == null)
            {
                return NotFound();
            }
            return View(dailyRoutine);
        }

        // POST: DailyRoutines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DailyRoutineId,ProgramId,RoutineTitle,RoutineNote")] DailyRoutine dailyRoutine)
        {
            if (id != dailyRoutine.DailyRoutineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyRoutine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyRoutineExists(dailyRoutine.DailyRoutineId))
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
            return View(dailyRoutine);
        }

        // GET: DailyRoutines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyRoutine = await _context.DailyRoutines
                .FirstOrDefaultAsync(m => m.DailyRoutineId == id);
            if (dailyRoutine == null)
            {
                return NotFound();
            }

            return View(dailyRoutine);
        }

        // POST: DailyRoutines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyRoutine = await _context.DailyRoutines.FindAsync(id);
            if (dailyRoutine != null)
            {
                _context.DailyRoutines.Remove(dailyRoutine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyRoutineExists(int id)
        {
            return _context.DailyRoutines.Any(e => e.DailyRoutineId == id);
        }
    }
}

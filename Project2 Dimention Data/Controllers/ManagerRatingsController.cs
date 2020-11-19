using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2_Dimention_Data.Filters;
using Project2_Dimention_Data.Models.Entities;
using Project2_Dimention_Data.Services;

namespace Project2_Dimention_Data.Controllers
{
    [Authentication]
    public class ManagerRatingsController : Controller
    {
        private readonly emp_infoContext _context;

        public ManagerRatingsController(emp_infoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var emp_infoContext = _context.ManagerRatings.Include(m => m.EmpNumberNavigation);
            return View(await emp_infoContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerRating = await _context.ManagerRatings
                .Include(m => m.EmpNumberNavigation)
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (managerRating == null)
            {
                return NotFound();
            }

            return View(managerRating);
        }


        public IActionResult Create()
        {
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpNumber,PerformanceRating,TrainingTimesLastYear,RatingId")] ManagerRating managerRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(managerRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", managerRating.EmpNumber);
            return View(managerRating);
        }

 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerRating = await _context.ManagerRatings.FindAsync(id);
            if (managerRating == null)
            {
                return NotFound();
            }
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", managerRating.EmpNumber);
            return View(managerRating);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpNumber,PerformanceRating,TrainingTimesLastYear,RatingId")] ManagerRating managerRating)
        {
            if (id != managerRating.RatingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(managerRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerRatingExists(managerRating.RatingId))
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
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", managerRating.EmpNumber);
            return View(managerRating);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerRating = await _context.ManagerRatings
                .Include(m => m.EmpNumberNavigation)
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (managerRating == null)
            {
                return NotFound();
            }

            return View(managerRating);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var managerRating = await _context.ManagerRatings.FindAsync(id);
            _context.ManagerRatings.Remove(managerRating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerRatingExists(int id)
        {
            return _context.ManagerRatings.Any(e => e.RatingId == id);
        }
    }
}

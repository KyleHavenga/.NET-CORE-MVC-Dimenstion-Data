using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2_Dimention_Data.Filters;
using Project2_Dimention_Data.Models.Entities;

namespace Project2_Dimention_Data.Controllers
{
    [Authentication]
    public class RatesController : Controller
    {
        private readonly emp_infoContext _context;

        public RatesController(emp_infoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var emp_infoContext = _context.Rates.Include(r => r.EmpNumberNavigation);
            return View(await emp_infoContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate = await _context.Rates
                .Include(r => r.EmpNumberNavigation)
                .FirstOrDefaultAsync(m => m.RatesId == id);
            if (rate == null)
            {
                return NotFound();
            }

            return View(rate);
        }

        public IActionResult Create()
        {
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpNumber,HourlyRate,MonthlyRate,DailyRate,RatesId")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", rate.EmpNumber);
            return View(rate);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate = await _context.Rates.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", rate.EmpNumber);
            return View(rate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpNumber,HourlyRate,MonthlyRate,DailyRate,RatesId")] Rate rate)
        {
            if (id != rate.RatesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RateExists(rate.RatesId))
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
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", rate.EmpNumber);
            return View(rate);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate = await _context.Rates
                .Include(r => r.EmpNumberNavigation)
                .FirstOrDefaultAsync(m => m.RatesId == id);
            if (rate == null)
            {
                return NotFound();
            }

            return View(rate);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rate = await _context.Rates.FindAsync(id);
            _context.Rates.Remove(rate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RateExists(int id)
        {
            return _context.Rates.Any(e => e.RatesId == id);
        }
    }
}

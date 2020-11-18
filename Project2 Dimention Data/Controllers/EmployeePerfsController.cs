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
    public class EmployeePerfsController : Controller
    {
        private readonly emp_infoContext _context;

        public EmployeePerfsController(emp_infoContext context)
        {
            _context = context;
        }

        // GET: EmployeePerfs
        public async Task<IActionResult> Index()
        {
            var emp_infoContext = _context.EmployeePerves.Include(e => e.EmpNumberNavigation);
            return View(await emp_infoContext.ToListAsync());
        }

        // GET: EmployeePerfs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePerf = await _context.EmployeePerves
                .Include(e => e.EmpNumberNavigation)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employeePerf == null)
            {
                return NotFound();
            }

            return View(employeePerf);
        }

        // GET: EmployeePerfs/Create
        public IActionResult Create()
        {
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber");
            return View();
        }

        // POST: EmployeePerfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpNumber,EnvironmentSat,JobSat,RelationshipSat,WorkLifeBalance,EmpId")] EmployeePerf employeePerf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeePerf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", employeePerf.EmpNumber);
            return View(employeePerf);
        }

        // GET: EmployeePerfs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePerf = await _context.EmployeePerves.FindAsync(id);
            if (employeePerf == null)
            {
                return NotFound();
            }
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", employeePerf.EmpNumber);
            return View(employeePerf);
        }

        // POST: EmployeePerfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpNumber,EnvironmentSat,JobSat,RelationshipSat,WorkLifeBalance,EmpId")] EmployeePerf employeePerf)
        {
            if (id != employeePerf.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeePerf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeePerfExists(employeePerf.EmpId))
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
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", employeePerf.EmpNumber);
            return View(employeePerf);
        }

        // GET: EmployeePerfs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePerf = await _context.EmployeePerves
                .Include(e => e.EmpNumberNavigation)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employeePerf == null)
            {
                return NotFound();
            }

            return View(employeePerf);
        }

        // POST: EmployeePerfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeePerf = await _context.EmployeePerves.FindAsync(id);
            _context.EmployeePerves.Remove(employeePerf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeePerfExists(int id)
        {
            return _context.EmployeePerves.Any(e => e.EmpId == id);
        }
    }
}

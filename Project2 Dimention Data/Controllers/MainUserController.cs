using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2_Dimention_Data.Models.Entities;
using Project2_Dimention_Data.Services;
using Project2_Dimention_Data.Filters;

namespace Project2_Dimention_Data.Controllers
{
    [Authentication]
    public class MainUserController : Controller
    {
        private readonly emp_infoContext _context;

        public MainUserController(emp_infoContext context)
        {
            _context = context;
        }

 
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrimaryTables.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryTable = await _context.PrimaryTables
                .FirstOrDefaultAsync(m => m.EmpNumber == id);
            if (primaryTable == null)
            {
                return NotFound();
            }

            return View(primaryTable);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpNumber,MaritalStatus,Age,Over18,NumCompaniesWorked,NumWorkingYears,DistanceFromHome,Education,EducationField,Gender")] PrimaryTable primaryTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(primaryTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(primaryTable);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryTable = await _context.PrimaryTables.FindAsync(id);
            if (primaryTable == null)
            {
                return NotFound();
            }
            return View(primaryTable);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpNumber,MaritalStatus,Age,Over18,NumCompaniesWorked,NumWorkingYears,DistanceFromHome,Education,EducationField,Gender")] PrimaryTable primaryTable)
        {
            if (id != primaryTable.EmpNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(primaryTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrimaryTableExists(primaryTable.EmpNumber))
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
            return View(primaryTable);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryTable = await _context.PrimaryTables
                .FirstOrDefaultAsync(m => m.EmpNumber == id);
            if (primaryTable == null)
            {
                return NotFound();
            }

            return View(primaryTable);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var primaryTable = await _context.PrimaryTables.FindAsync(id);
            _context.PrimaryTables.Remove(primaryTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrimaryTableExists(int id)
        {
            return _context.PrimaryTables.Any(e => e.EmpNumber == id);
        }
    }
}

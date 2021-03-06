﻿using System;
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
    public class JobdetailsController : Controller
    {
        private readonly emp_infoContext _context;

        public JobdetailsController(emp_infoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var emp_infoContext = _context.Jobdetails.Include(j => j.EmpNumberNavigation);
            return View(await emp_infoContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobdetail = await _context.Jobdetails
                .Include(j => j.EmpNumberNavigation)
                .FirstOrDefaultAsync(m => m.JobDetailsId == id);
            if (jobdetail == null)
            {
                return NotFound();
            }

            return View(jobdetail);
        }


        public IActionResult Create()
        {
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpNumber,Attrition,BusinessTravel,Department,EmployeeCount,JobInvolvement,JobLevel,JobRole,Overtime,StandardHours,StockOptionLevel,YearsLastPromotion,YearsCurrentRole,YearsCurrentManager,YearsAtCompany,JobDetailsId")] Jobdetail jobdetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobdetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", jobdetail.EmpNumber);
            return View(jobdetail);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobdetail = await _context.Jobdetails.FindAsync(id);
            if (jobdetail == null)
            {
                return NotFound();
            }
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", jobdetail.EmpNumber);
            return View(jobdetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpNumber,Attrition,BusinessTravel,Department,EmployeeCount,JobInvolvement,JobLevel,JobRole,Overtime,StandardHours,StockOptionLevel,YearsLastPromotion,YearsCurrentRole,YearsCurrentManager,YearsAtCompany,JobDetailsId")] Jobdetail jobdetail)
        {
            if (id != jobdetail.JobDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobdetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobdetailExists(jobdetail.JobDetailsId))
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
            ViewData["EmpNumber"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", jobdetail.EmpNumber);
            return View(jobdetail);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobdetail = await _context.Jobdetails
                .Include(j => j.EmpNumberNavigation)
                .FirstOrDefaultAsync(m => m.JobDetailsId == id);
            if (jobdetail == null)
            {
                return NotFound();
            }

            return View(jobdetail);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobdetail = await _context.Jobdetails.FindAsync(id);
            _context.Jobdetails.Remove(jobdetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobdetailExists(int id)
        {
            return _context.Jobdetails.Any(e => e.JobDetailsId == id);
        }
    }
}

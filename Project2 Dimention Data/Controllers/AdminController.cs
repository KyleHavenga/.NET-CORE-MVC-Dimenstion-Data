﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2_Dimention_Data.Filters;
using Project2_Dimention_Data.Models.Entities;
using Project2_Dimention_Data.Models.ViewModels;
using Project2_Dimention_Data.Services;
using ServiceStack;

namespace Project2_Dimention_Data.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly emp_infoContext _context;
        private readonly Cryptography _cryptography;
        public AdminController(emp_infoContext context, Cryptography cryptography)

        {
            _context = context;
            _cryptography = cryptography;
        }
        [Authentication(userRoles: "Admin")]
        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var emp_infoContext = _context.Logins.Include(l => l.EmpNumNavigation);
            return View(await emp_infoContext.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .Include(l => l.EmpNumNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Admin/Create

        public IActionResult Create()
        {
            ViewData["EmpNum"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserManagementCreate model)
        {
            if (ModelState.IsValid)
            {
                
                var passwordSalt = Guid.NewGuid().ToString();
                var user = new Login()
                {
                    NameUser = model.NameUser,
                    SurnameUser = model.SurnameUser,
                    UserEmail = model.UserEmail,
                    Passwordsalt = passwordSalt,
                    Passwordhash = _cryptography.HashSHA256(model.Passwordhash + passwordSalt),
                    UserRole = "User",
                    Id = random_id(),
                    EmpNum = int.Parse(model.EmpNum),
                };

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        private readonly Random _random = new Random();
        public string random_id()
        {
            Random random = new Random();
            int length = 16;
            var rString = "";
            for (var i = 0; i < length; i++)
            {
                rString += ((char)(random.Next(1, 26) + 64)).ToString().ToLower();
            }
            return rString;
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            ViewData["EmpNum"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", login.EmpNum);
            return View(login);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmpNum,Passwordhash,Passwordsalt,NameUser,SurnameUser,UserEmail,UserRole,Id")] Login login)
        {
            if (id != login.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.Id))
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
            ViewData["EmpNum"] = new SelectList(_context.PrimaryTables, "EmpNumber", "EmpNumber", login.EmpNum);
            return View(login);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .Include(l => l.EmpNumNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var login = await _context.Logins.FindAsync(id);
            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(string id)
        {
            return _context.Logins.Any(e => e.Id == id);
        }
    }
}

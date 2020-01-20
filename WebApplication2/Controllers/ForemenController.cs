using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ForemenController : Controller
    {
        private readonly WebApplication2Context _context;

        public ForemenController(WebApplication2Context context)
        {
            _context = context;
        }

        // GET: Foremen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Foreman.ToListAsync());
        }

        // GET: Foremen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foreman = await _context.Foreman
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foreman == null)
            {
                return NotFound();
            }

            return View(foreman);
        }

        // GET: Foremen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foremen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Foreman foreman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foreman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foreman);
        }

        // GET: Foremen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foreman = await _context.Foreman.FindAsync(id);
            if (foreman == null)
            {
                return NotFound();
            }
            return View(foreman);
        }

        // POST: Foremen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Foreman foreman)
        {
            if (id != foreman.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foreman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForemanExists(foreman.Id))
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
            return View(foreman);
        }

        // GET: Foremen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foreman = await _context.Foreman
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foreman == null)
            {
                return NotFound();
            }

            return View(foreman);
        }

        // POST: Foremen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foreman = await _context.Foreman.FindAsync(id);
            _context.Foreman.Remove(foreman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForemanExists(int id)
        {
            return _context.Foreman.Any(e => e.Id == id);
        }
    }
}

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
    public class WorkplacesController : Controller
    {
        private readonly WebApplication2Context _context;

        public WorkplacesController(WebApplication2Context context)
        {
            _context = context;
        }

        // GET: Workplaces
        public async Task<IActionResult> Index()
        {
            var webApplication2Context = _context.Workplace.Include(w => w.Foreman);
            return View(await webApplication2Context.ToListAsync());
        }

        // GET: Workplaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workplace = await _context.Workplace
                .Include(w => w.Foreman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workplace == null)
            {
                return NotFound();
            }

            return View(workplace);
        }

        // GET: Workplaces/Create
        public IActionResult Create()
        {
            ViewData["ForemanId"] = new SelectList(_context.Foreman, "Id", "Id");
            return View();
        }

        // POST: Workplaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ForemanId")] Workplace workplace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workplace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ForemanId"] = new SelectList(_context.Foreman, "Id", "Id", workplace.ForemanId);
            return View(workplace);
        }

        // GET: Workplaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workplace = await _context.Workplace.FindAsync(id);
            if (workplace == null)
            {
                return NotFound();
            }
            ViewData["ForemanId"] = new SelectList(_context.Foreman, "Id", "Id", workplace.ForemanId);
            return View(workplace);
        }

        // POST: Workplaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ForemanId")] Workplace workplace)
        {
            if (id != workplace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workplace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkplaceExists(workplace.Id))
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
            ViewData["ForemanId"] = new SelectList(_context.Foreman, "Id", "Id", workplace.ForemanId);
            return View(workplace);
        }

        // GET: Workplaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workplace = await _context.Workplace
                .Include(w => w.Foreman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workplace == null)
            {
                return NotFound();
            }

            return View(workplace);
        }

        // POST: Workplaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workplace = await _context.Workplace.FindAsync(id);
            _context.Workplace.Remove(workplace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkplaceExists(int id)
        {
            return _context.Workplace.Any(e => e.Id == id);
        }
    }
}

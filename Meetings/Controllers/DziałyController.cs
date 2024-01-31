using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Meetings.Data;
using Meetings.Models;

namespace Meetings.Controllers
{
    public class DziałyController : Controller
    {
        private readonly AppDbContext _context;

        public DziałyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Działy
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Działy.Include(d => d.IdFiliiNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Działy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var działy = await _context.Działy
                .Include(d => d.IdFiliiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (działy == null)
            {
                return NotFound();
            }

            return View(działy);
        }

        // GET: Działy/Create
        public IActionResult Create()
        {
            ViewData["IdFilii"] = new SelectList(_context.Filie, "Id", "Id");
            return View();
        }

        // POST: Działy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NazwaDzialu,IdFilii")] Dział działy)
        {
            działy.IdFiliiNavigation = _context.Filie.Find(działy.IdFilii);
            if (TryValidateModel(działy))
            {
                _context.Add(działy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFilii"] = new SelectList(_context.Filie, "Id", "Id", działy.IdFilii);
            return View(działy);
        }

        // GET: Działy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var działy = await _context.Działy.FindAsync(id);
            if (działy == null)
            {
                return NotFound();
            }
            ViewData["IdFilii"] = new SelectList(_context.Filie, "Id", "Id", działy.IdFilii);
            return View(działy);
        }

        // POST: Działy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NazwaDzialu,IdFilii")] Dział działy)
        {
            if (id != działy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(działy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DziałyExists(działy.Id))
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
            ViewData["IdFilii"] = new SelectList(_context.Filie, "Id", "Id", działy.IdFilii);
            return View(działy);
        }

        // GET: Działy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var działy = await _context.Działy
                .Include(d => d.IdFiliiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (działy == null)
            {
                return NotFound();
            }

            return View(działy);
        }

        // POST: Działy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var działy = await _context.Działy.FindAsync(id);
            if (działy != null)
            {
                _context.Działy.Remove(działy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DziałyExists(int id)
        {
            return _context.Działy.Any(e => e.Id == id);
        }
    }
}

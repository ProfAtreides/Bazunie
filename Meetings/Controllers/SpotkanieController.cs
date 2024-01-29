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
    public class SpotkanieController : Controller
    {
        private readonly AppDbContext _context;

        public SpotkanieController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Spotkanie
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Spotkania.Include(s => s.IdFiliiNavigation).Include(s => s.IdSaliNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Spotkanie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spotkanie = await _context.Spotkania
                .Include(s => s.IdFiliiNavigation)
                .Include(s => s.IdSaliNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spotkanie == null)
            {
                return NotFound();
            }

            return View(spotkanie);
        }

        // GET: Spotkanie/Create
        public IActionResult Create()
        {
            ViewData["IdFilii"] = new SelectList(_context.Filies, "Id", "Id");
            ViewData["IdSali"] = new SelectList(_context.Salas, "Id", "Id");
            return View();
        }

        // POST: Spotkanie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaxLiczbaUczestnikow,GodzinaSpotkania,DzienTygodnia,IdSali,IdFilii")] Spotkanie spotkanie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spotkanie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFilii"] = new SelectList(_context.Filies, "Id", "Id", spotkanie.IdFilii);
            ViewData["IdSali"] = new SelectList(_context.Salas, "Id", "Id", spotkanie.IdSali);
            return View(spotkanie);
        }

        // GET: Spotkanie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spotkanie = await _context.Spotkania.FindAsync(id);
            if (spotkanie == null)
            {
                return NotFound();
            }
            ViewData["IdFilii"] = new SelectList(_context.Filies, "Id", "Id", spotkanie.IdFilii);
            ViewData["IdSali"] = new SelectList(_context.Salas, "Id", "Id", spotkanie.IdSali);
            return View(spotkanie);
        }

        // POST: Spotkanie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaxLiczbaUczestnikow,GodzinaSpotkania,DzienTygodnia,IdSali,IdFilii")] Spotkanie spotkanie)
        {
            if (id != spotkanie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spotkanie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpotkanieExists(spotkanie.Id))
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
            ViewData["IdFilii"] = new SelectList(_context.Filies, "Id", "Id", spotkanie.IdFilii);
            ViewData["IdSali"] = new SelectList(_context.Salas, "Id", "Id", spotkanie.IdSali);
            return View(spotkanie);
        }

        // GET: Spotkanie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spotkanie = await _context.Spotkania
                .Include(s => s.IdFiliiNavigation)
                .Include(s => s.IdSaliNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spotkanie == null)
            {
                return NotFound();
            }

            return View(spotkanie);
        }

        // POST: Spotkanie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spotkanie = await _context.Spotkania.FindAsync(id);
            if (spotkanie != null)
            {
                _context.Spotkania.Remove(spotkanie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpotkanieExists(int id)
        {
            return _context.Spotkania.Any(e => e.Id == id);
        }
    }
}

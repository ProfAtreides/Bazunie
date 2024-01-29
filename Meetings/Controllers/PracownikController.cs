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
    public class PracownikController : Controller
    {
        private readonly AppDbContext _context;

        public PracownikController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pracownik
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Pracownicies.Include(p => p.IdDzialuNavigation).Include(p => p.IdFiliiNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Pracownik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownik = await _context.Pracownicies
                .Include(p => p.IdDzialuNavigation)
                .Include(p => p.IdFiliiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pracownik == null)
            {
                return NotFound();
            }

            return View(pracownik);
        }

        // GET: Pracownik/Create
        public IActionResult Create()
        {
            ViewData["IdDzialu"] = new SelectList(_context.Działies, "Id", "Id");
            ViewData["IdFilii"] = new SelectList(_context.Filies, "Id", "Id");
            return View();
        }

        // POST: Pracownik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("admin,Id,ImiePracownika,NazwiskoPracownika,Stanowisko,IdFilii,IdDzialu,Haslo")] Pracownik pracownik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pracownik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDzialu"] = new SelectList(_context.Działies, "Id", "Id", pracownik.IdDzialu);
            ViewData["IdFilii"] = new SelectList(_context.Filies, "Id", "Id", pracownik.IdFilii);
            return View(pracownik);
        }

        // GET: Pracownik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownik = await _context.Pracownicies.FindAsync(id);
            if (pracownik == null)
            {
                return NotFound();
            }
            ViewData["IdDzialu"] = new SelectList(_context.Działies, "Id", "Id", pracownik.IdDzialu);
            ViewData["IdFilii"] = new SelectList(_context.Filies, "Id", "Id", pracownik.IdFilii);
            return View(pracownik);
        }

        // POST: Pracownik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("admin,Id,ImiePracownika,NazwiskoPracownika,Stanowisko,IdFilii,IdDzialu,Haslo")] Pracownik pracownik)
        {
            if (id != pracownik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pracownik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PracownikExists(pracownik.Id))
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
            ViewData["IdDzialu"] = new SelectList(_context.Działies, "Id", "Id", pracownik.IdDzialu);
            ViewData["IdFilii"] = new SelectList(_context.Filies, "Id", "Id", pracownik.IdFilii);
            return View(pracownik);
        }

        // GET: Pracownik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownik = await _context.Pracownicies
                .Include(p => p.IdDzialuNavigation)
                .Include(p => p.IdFiliiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pracownik == null)
            {
                return NotFound();
            }

            return View(pracownik);
        }

        // POST: Pracownik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pracownik = await _context.Pracownicies.FindAsync(id);
            if (pracownik != null)
            {
                _context.Pracownicies.Remove(pracownik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PracownikExists(int id)
        {
            return _context.Pracownicies.Any(e => e.Id == id);
        }
    }
}

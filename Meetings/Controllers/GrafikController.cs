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
    public class GrafikController : Controller
    {
        private readonly AppDbContext _context;

        public GrafikController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Grafik
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Grafiks.Include(g => g.IdPracownikaNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Grafik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grafik = await _context.Grafiks
                .Include(g => g.IdPracownikaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grafik == null)
            {
                return NotFound();
            }

            return View(grafik);
        }

        // GET: Grafik/Create
        public IActionResult Create()
        {
            ViewData["IdPracownika"] = new SelectList(_context.Pracownicies, "Id", "Id");
            return View();
        }

        // POST: Grafik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GodzinaRozpoczecia,GodzinaZakonczenia,IloscGodzin,DzienTygodnia,IdPracownika")] Grafik grafik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grafik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPracownika"] = new SelectList(_context.Pracownicies, "Id", "Id", grafik.IdPracownika);
            return View(grafik);
        }

        // GET: Grafik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grafik = await _context.Grafiks.FindAsync(id);
            if (grafik == null)
            {
                return NotFound();
            }
            ViewData["IdPracownika"] = new SelectList(_context.Pracownicies, "Id", "Id", grafik.IdPracownika);
            return View(grafik);
        }

        // POST: Grafik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GodzinaRozpoczecia,GodzinaZakonczenia,IloscGodzin,DzienTygodnia,IdPracownika")] Grafik grafik)
        {
            if (id != grafik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grafik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrafikExists(grafik.Id))
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
            ViewData["IdPracownika"] = new SelectList(_context.Pracownicies, "Id", "Id", grafik.IdPracownika);
            return View(grafik);
        }

        // GET: Grafik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grafik = await _context.Grafiks
                .Include(g => g.IdPracownikaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grafik == null)
            {
                return NotFound();
            }

            return View(grafik);
        }

        // POST: Grafik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grafik = await _context.Grafiks.FindAsync(id);
            if (grafik != null)
            {
                _context.Grafiks.Remove(grafik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrafikExists(int id)
        {
            return _context.Grafiks.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Meetings;
using Meetings.Models;
using Meetings.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Meetings.Controllers
{
    public class PracownicyController : Controller
    {
        private readonly AppDbContext _context;

        public PracownicyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pracownicy
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Pracownicy.Include(p => p.IdDzialuNavigation).Include(p => p.IdFiliiNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Pracownicy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownicy = await _context.Pracownicy
                .Include(p => p.IdDzialuNavigation)
                .Include(p => p.IdFiliiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pracownicy == null)
            {
                return NotFound();
            }

            return View(pracownicy);
        }

        // GET: Pracownicy/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["NazwaDzialu"] = new SelectList(_context.Działy, "Id", "NazwaDzialu");
            ViewData["NazwaFilli"] = new SelectList(_context.Filie, "Id", "NazwaFilii");
            return View();
        }

        // POST: Pracownicy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,ImiePracownika,NazwiskoPracownika,Stanowisko,IdFilii,IdDzialu,Haslo,Admin")] Pracownik pracownik)
        {
            if (ModelState.IsValid)
            {
                pracownik.Haslo =  Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: pracownik.Haslo!,
                    salt: Encoding.UTF8.GetBytes("prosze3;)"),
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
                _context.Add(pracownik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NazwaDzialu"] = new SelectList(_context.Działy, "Id", "NazwaDzialu");
            ViewData["NazwaFilli"] = new SelectList(_context.Filie, "Id", "NazwaFilii");
            return View(pracownik);
        }

        // GET: Pracownicy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownicy = await _context.Pracownicy.FindAsync(id);
            if (pracownicy == null)
            {
                return NotFound();
            }
            ViewData["IdDzialu"] = new SelectList(_context.Działy, "Id", "Id", pracownicy.IdDzialu);
            ViewData["IdFilii"] = new SelectList(_context.Filie, "Id", "Id", pracownicy.IdFilii);
            return View(pracownicy);
        }

        // POST: Pracownicy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImiePracownika,NazwiskoPracownika,Stanowisko,IdFilii,IdDzialu,Haslo,Admin")] Pracownik pracownik)
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
                    if (!PracownicyExists(pracownik.Id))
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
            ViewData["IdDzialu"] = new SelectList(_context.Działy, "Id", "Id", pracownik.IdDzialu);
            ViewData["IdFilii"] = new SelectList(_context.Filie, "Id", "Id", pracownik.IdFilii);
            return View(pracownik);
        }

        // GET: Pracownicy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownicy = await _context.Pracownicy
                .Include(p => p.IdDzialuNavigation)
                .Include(p => p.IdFiliiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pracownicy == null)
            {
                return NotFound();
            }

            return View(pracownicy);
        }

        // POST: Pracownicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pracownicy = await _context.Pracownicy.FindAsync(id);
            if (pracownicy != null)
            {
                _context.Pracownicy.Remove(pracownicy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PracownicyExists(int id)
        {
            return _context.Pracownicy.Any(e => e.Id == id);
        }
    }
}

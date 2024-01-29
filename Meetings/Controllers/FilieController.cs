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
    public class FilieController : Controller
    {
        private readonly AppDbContext _context;

        public FilieController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Filie
        public async Task<IActionResult> Index()
        {
            return View(await _context.Filies.ToListAsync());
        }

        // GET: Filie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filie = await _context.Filies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filie == null)
            {
                return NotFound();
            }

            return View(filie);
        }

        // GET: Filie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NazwaFilii")] Filie filie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filie);
        }

        // GET: Filie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filie = await _context.Filies.FindAsync(id);
            if (filie == null)
            {
                return NotFound();
            }
            return View(filie);
        }

        // POST: Filie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NazwaFilii")] Filie filie)
        {
            if (id != filie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilieExists(filie.Id))
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
            return View(filie);
        }

        // GET: Filie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filie = await _context.Filies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filie == null)
            {
                return NotFound();
            }

            return View(filie);
        }

        // POST: Filie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filie = await _context.Filies.FindAsync(id);
            if (filie != null)
            {
                _context.Filies.Remove(filie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilieExists(int id)
        {
            return _context.Filies.Any(e => e.Id == id);
        }
    }
}

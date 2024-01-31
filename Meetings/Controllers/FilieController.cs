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
            return View(await _context.Filie.ToListAsync());
        }

        // GET: Filie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filie = await _context.Filie
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
        public async Task<IActionResult> Create([Bind("Id,NazwaFilii")] Filia filia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filia);
        }

        // GET: Filie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filie = await _context.Filie.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,NazwaFilii")] Filia filia)
        {
            if (id != filia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilieExists(filia.Id))
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
            return View(filia);
        }

        // GET: Filie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filie = await _context.Filie
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
            var filie = await _context.Filie.FindAsync(id);
            if (filie != null)
            {
                _context.Filie.Remove(filie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilieExists(int id)
        {
            return _context.Filie.Any(e => e.Id == id);
        }
    }
}

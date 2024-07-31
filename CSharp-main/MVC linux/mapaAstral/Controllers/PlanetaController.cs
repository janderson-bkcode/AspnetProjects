using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mapaAstral.Models;
using mapaAstral.data;

namespace mapaAstral.Controllers
{
    public class PlanetaController : Controller
    {
        private readonly MeuDbContext _context;

        public PlanetaController(MeuDbContext context)
        {
            _context = context;
        }

        // GET: Planeta
        public async Task<IActionResult> Index()
        {
            return _context.Planetas != null ?
                        View(await _context.Planetas.ToListAsync()) :
                        Problem("Entity set 'MeuDbContext.Planetas'  is null.");
        }

        // GET: Planeta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Planetas == null)
            {
                return NotFound();
            }

            var planeta = await _context.Planetas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planeta == null)
            {
                return NotFound();
            }

            return View(planeta);
        }

        // GET: Planeta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Planeta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Simbolo,Elemento,Casa")] Planeta planeta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planeta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planeta);
        }

        // GET: Planeta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Planetas == null)
            {
                return NotFound();
            }

            var planeta = await _context.Planetas.FindAsync(id);
            if (planeta == null)
            {
                return NotFound();
            }
            return View(planeta);
        }

        // POST: Planeta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Simbolo,Elemento,Casa")] Planeta planeta)
        {
            if (id != planeta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planeta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanetaExists(planeta.Id))
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
            return View(planeta);
        }

        // GET: Planeta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Planetas == null)
            {
                return NotFound();
            }

            var planeta = await _context.Planetas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planeta == null)
            {
                return NotFound();
            }

            return View(planeta);
        }

        // POST: Planeta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Planetas == null)
            {
                return Problem("Entity set 'MeuDbContext.Planetas'  is null.");
            }
            var planeta = await _context.Planetas.FindAsync(id);
            if (planeta != null)
            {
                _context.Planetas.Remove(planeta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanetaExists(int id)
        {
            return (_context.Planetas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

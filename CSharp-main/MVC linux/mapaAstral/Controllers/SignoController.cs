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
    public class SignoController : Controller
    {
        private readonly MeuDbContext _context;

        public SignoController(MeuDbContext context)
        {
            _context = context;
        }

        // GET: Signo
        public async Task<IActionResult> Index()
        {
              return _context.Signos != null ? 
                          View(await _context.Signos.ToListAsync()) :
                          Problem("Entity set 'MeuDbContext.Signos'  is null.");
        }

        // GET: Signo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Signos == null)
            {
                return NotFound();
            }

            var signo = await _context.Signos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (signo == null)
            {
                return NotFound();
            }

            return View(signo);
        }

        // GET: Signo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Signo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataInicio,DataFim,Elemento,Regente")] Signo signo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(signo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(signo);
        }

        // GET: Signo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Signos == null)
            {
                return NotFound();
            }

            var signo = await _context.Signos.FindAsync(id);
            if (signo == null)
            {
                return NotFound();
            }
            return View(signo);
        }

        // POST: Signo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataInicio,DataFim,Elemento,Regente")] Signo signo)
        {
            if (id != signo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(signo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SignoExists(signo.Id))
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
            return View(signo);
        }

        // GET: Signo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Signos == null)
            {
                return NotFound();
            }

            var signo = await _context.Signos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (signo == null)
            {
                return NotFound();
            }

            return View(signo);
        }

        // POST: Signo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Signos == null)
            {
                return Problem("Entity set 'MeuDbContext.Signos'  is null.");
            }
            var signo = await _context.Signos.FindAsync(id);
            if (signo != null)
            {
                _context.Signos.Remove(signo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SignoExists(int id)
        {
          return (_context.Signos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using mapaAstral.data;
using mapaAstral.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace mapaAstral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapaAstralController : Controller
    {
        private readonly MeuDbContext _context;

        public MapaAstralController(MeuDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signos()
        {
            var signos = _context.Signos.ToList();

            return View(signos);
        }

        public IActionResult CreateSigno()
        {
            List<Elemento> elementos = new List<Elemento>{
               new Elemento{Id = 1, Nome = "Ar"},
               new Elemento{Id = 2, Nome = "Agua"},
               new Elemento{Id = 3, Nome = "Fogo"},
               new Elemento{Id = 4, Nome = "Terra"},

            };
            ViewBag.elementos = new SelectList(elementos,"Id","Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSigno(
            [Bind("Nome,DataInicio,DataFim,Elemento,Regente")] Signo signo
        )
        {
            if (ModelState.IsValid)
            {
                _context.Add(signo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Signos));
            }
            return View(signo);
        }

        public async Task<IActionResult> EditSigno(int? id)
        {
            if (id == null)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSigno(
            int id,
            [Bind("Id,Nome,DataInicio,DataFim,Elemento,Regente")] Signo signo
        )
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
                return RedirectToAction(nameof(Signos));
            }
            return View(signo);
        }

        public async Task<IActionResult> DeleteSigno(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signo = await _context.Signos.FirstOrDefaultAsync(m => m.Id == id);

            if (signo == null)
            {
                return NotFound();
            }

            return View(signo);
        }

        [HttpPost, ActionName("DeleteSigno")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSignoConfirmed(int id)
        {
            var signo = await _context.Signos.FindAsync(id);
            if (signo != null)
            {
                _context.Signos.Remove(signo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Signos));
        }

        private bool SignoExists(int id)
        {
            return _context.Signos.Any(e => e.Id == id);
        }

        public IActionResult Planetas()
        {
            var planetas = _context.Planetas.ToList();

            return View(planetas);
        }

        public IActionResult CreatePlaneta()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlaneta(
            [Bind("Nome,Simbolo,Elemento,Casa")] Planeta planeta
        )
        {
            if (ModelState.IsValid)
            {
                _context.Add(planeta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Planetas));
            }
            return View(planeta);
        }

        public async Task<IActionResult> EditPlaneta(int? id)
        {
            if (id == null)
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
    }
}

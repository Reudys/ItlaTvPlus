using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITLATV.Data;
using ITLATV.Models;

namespace ITLATV.Controllers
{
    public class ProductorasController : Controller
    {
        private readonly ITLATVContext _context;

        public ProductorasController(ITLATVContext context)
        {
            _context = context;
        }

        // GET: Productoras
        public IActionResult Index()
        {
            // Obtenemos todas las productoras
            var productoras = _context.Productora.ToList();

            // Creamos un diccionario para almacenar las series por productora
            var seriesPorProductora = new Dictionary<string, List<Serie>>();

            foreach (var productora in productoras)
            {
                // Filtramos las series por productora
                var series = _context.Serie.Where(s => s.Productora == productora.Name).ToList();
                seriesPorProductora.Add(productora.Name, series);
            }

            // Pasamos el diccionario a la vista
            return View(seriesPorProductora);
        }

        // GET: Productoras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productora = await _context.Productora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productora == null)
            {
                return NotFound();
            }

            return View(productora);
        }

        // GET: Productoras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productoras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Productora productora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productora);
        }

        // GET: Productoras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productora = await _context.Productora.FindAsync(id);
            if (productora == null)
            {
                return NotFound();
            }
            return View(productora);
        }

        // POST: Productoras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Productora productora)
        {
            if (id != productora.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoraExists(productora.Id))
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
            return View(productora);
        }

        // GET: Productoras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productora = await _context.Productora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productora == null)
            {
                return NotFound();
            }

            return View(productora);
        }

        // POST: Productoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productora = await _context.Productora.FindAsync(id);
            if (productora != null)
            {
                _context.Productora.Remove(productora);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoraExists(int id)
        {
            return _context.Productora.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBA_WebApp.Data;
using NBA_WebApp.Models;

namespace NBA_WebApp.Controllers
{
    public class IgracsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IgracsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Igracs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Igrac.ToListAsync());
        }

        // GET: Igracs/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // PoST: Igracs/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String PretraziIgraca)
        {
            return View("Index",await _context.Igrac.Where( j => j.Prezime.Contains(PretraziIgraca)).ToListAsync());
        }

        // GET: Igracs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igrac = await _context.Igrac
                .FirstOrDefaultAsync(m => m.Id == id);
            if (igrac == null)
            {
                return NotFound();
            }

            return View(igrac);
        }

        // GET: Igracs/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Igracs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Ime,Prezime,Pozicija,Nadimak,Broj_dresa")] Igrac igrac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(igrac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(igrac);
        }

        // GET: Igracs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igrac = await _context.Igrac.FindAsync(id);
            if (igrac == null)
            {
                return NotFound();
            }
            return View(igrac);
        }

        // POST: Igracs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Prezime,Pozicija,Nadimak,Broj_dresa")] Igrac igrac)
        {
            if (id != igrac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(igrac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IgracExists(igrac.Id))
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
            return View(igrac);
        }

        // GET: Igracs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igrac = await _context.Igrac
                .FirstOrDefaultAsync(m => m.Id == id);
            if (igrac == null)
            {
                return NotFound();
            }

            return View(igrac);
        }

        // POST: Igracs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var igrac = await _context.Igrac.FindAsync(id);
            _context.Igrac.Remove(igrac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IgracExists(int id)
        {
            return _context.Igrac.Any(e => e.Id == id);
        }
    }
}

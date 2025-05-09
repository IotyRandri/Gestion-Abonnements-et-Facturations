using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projet_.NET_Core_M2_Devops_Mr_Naina;
using Projet_.NET_Core_M2_Devops_Mr_Naina.Models;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Controllers
{
    public class AbonnementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AbonnementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Abonnement
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Abonnement.Include(a => a.TypeAbonnement);

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Abonnement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnement
                .Include(a => a.TypeAbonnement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonnement == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(abonnement);
        }

        // GET: Abonnement/Create
        public IActionResult Create()
        {
            ViewData["TypeAbonnementId"] = new SelectList(_context.TypeAbonnements, "Id", "Titre");

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View();
        }

        // POST: Abonnement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prix,TypeAbonnementId")] Abonnement abonnement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abonnement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeAbonnementId"] = new SelectList(_context.TypeAbonnements, "Id", "Titre", abonnement.TypeAbonnementId);

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(abonnement);
        }

        // GET: Abonnement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnement.FindAsync(id);
            if (abonnement == null)
            {
                return NotFound();
            }
            ViewData["TypeAbonnementId"] = new SelectList(_context.TypeAbonnements, "Id", "Titre", abonnement.TypeAbonnementId);

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(abonnement);
        }

        // POST: Abonnement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prix,TypeAbonnementId")] Abonnement abonnement)
        {
            if (id != abonnement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abonnement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonnementExists(abonnement.Id))
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
            ViewData["TypeAbonnementId"] = new SelectList(_context.TypeAbonnements, "Id", "Titre", abonnement.TypeAbonnementId);

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(abonnement);
        }

        // GET: Abonnement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnement
                .Include(a => a.TypeAbonnement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonnement == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";


            return View(abonnement);
        }

        // POST: Abonnement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abonnement = await _context.Abonnement.FindAsync(id);
            if (abonnement != null)
            {
                _context.Abonnement.Remove(abonnement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonnementExists(int id)
        {
            return _context.Abonnement.Any(e => e.Id == id);
        }
    }
}

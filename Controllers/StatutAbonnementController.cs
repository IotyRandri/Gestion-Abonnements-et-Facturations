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
    public class StatutAbonnementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatutAbonnementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatutAbonnement
        public async Task<IActionResult> Index()
        {
            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(await _context.StatutAbonnement.ToListAsync());
        }

        // GET: StatutAbonnement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statutAbonnement = await _context.StatutAbonnement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statutAbonnement == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(statutAbonnement);
        }

        // GET: StatutAbonnement/Create
        public IActionResult Create()
        {

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View();
        }

        // POST: StatutAbonnement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Intitule")] StatutAbonnement statutAbonnement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statutAbonnement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(statutAbonnement);
        }

        // GET: StatutAbonnement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statutAbonnement = await _context.StatutAbonnement.FindAsync(id);
            if (statutAbonnement == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(statutAbonnement);
        }

        // POST: StatutAbonnement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Intitule")] StatutAbonnement statutAbonnement)
        {
            if (id != statutAbonnement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statutAbonnement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatutAbonnementExists(statutAbonnement.Id))
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

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(statutAbonnement);
        }

        // GET: StatutAbonnement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statutAbonnement = await _context.StatutAbonnement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statutAbonnement == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(statutAbonnement);
        }

        // POST: StatutAbonnement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statutAbonnement = await _context.StatutAbonnement.FindAsync(id);
            if (statutAbonnement != null)
            {
                _context.StatutAbonnement.Remove(statutAbonnement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatutAbonnementExists(int id)
        {
            return _context.StatutAbonnement.Any(e => e.Id == id);
        }
    }
}

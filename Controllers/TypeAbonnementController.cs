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
    public class TypeAbonnementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeAbonnementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeAbonnement
        public async Task<IActionResult> Index()
        {
            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(await _context.TypeAbonnements.ToListAsync());
        }

        // GET: TypeAbonnement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAbonnement = await _context.TypeAbonnements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeAbonnement == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(typeAbonnement);
        }

        // GET: TypeAbonnement/Create
        public IActionResult Create()
        {
            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View();
        }

        // POST: TypeAbonnement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre")] TypeAbonnement typeAbonnement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeAbonnement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(typeAbonnement);
        }

        // GET: TypeAbonnement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAbonnement = await _context.TypeAbonnements.FindAsync(id);
            if (typeAbonnement == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(typeAbonnement);
        }

        // POST: TypeAbonnement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre")] TypeAbonnement typeAbonnement)
        {
            if (id != typeAbonnement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeAbonnement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeAbonnementExists(typeAbonnement.Id))
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

            return View(typeAbonnement);
        }

        // GET: TypeAbonnement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAbonnement = await _context.TypeAbonnements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeAbonnement == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(typeAbonnement);
        }

        // POST: TypeAbonnement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeAbonnement = await _context.TypeAbonnements.FindAsync(id);
            if (typeAbonnement != null)
            {
                _context.TypeAbonnements.Remove(typeAbonnement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeAbonnementExists(int id)
        {
            return _context.TypeAbonnements.Any(e => e.Id == id);
        }
    }
}

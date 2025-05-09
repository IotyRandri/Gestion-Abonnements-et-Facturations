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
    public class CommissionAnnulationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommissionAnnulationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CommissionAnnulation
        public async Task<IActionResult> Index()
        {
            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(await _context.CommissionAnnulation.ToListAsync());
        }

        // GET: CommissionAnnulation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commissionAnnulation = await _context.CommissionAnnulation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commissionAnnulation == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(commissionAnnulation);
        }

        // GET: CommissionAnnulation/Create
        public IActionResult Create()
        {
            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View();
        }

        // POST: CommissionAnnulation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Prixmin,Prixmax,Pourcentage")] CommissionAnnulation commissionAnnulation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commissionAnnulation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(commissionAnnulation);
        }

        // GET: CommissionAnnulation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commissionAnnulation = await _context.CommissionAnnulation.FindAsync(id);
            if (commissionAnnulation == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(commissionAnnulation);
        }

        // POST: CommissionAnnulation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Prixmin,Prixmax,Pourcentage")] CommissionAnnulation commissionAnnulation)
        {
            if (id != commissionAnnulation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commissionAnnulation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommissionAnnulationExists(commissionAnnulation.Id))
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

            return View(commissionAnnulation);
        }

        // GET: CommissionAnnulation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commissionAnnulation = await _context.CommissionAnnulation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commissionAnnulation == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(commissionAnnulation);
        }

        // POST: CommissionAnnulation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commissionAnnulation = await _context.CommissionAnnulation.FindAsync(id);
            if (commissionAnnulation != null)
            {
                _context.CommissionAnnulation.Remove(commissionAnnulation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommissionAnnulationExists(int id)
        {
            return _context.CommissionAnnulation.Any(e => e.Id == id);
        }
    }
}

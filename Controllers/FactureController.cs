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
    public class FactureController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FactureController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Facture
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Facture.Include(f => f.StatutAbonnement).Include(f => f.TypeAbonnement);

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Facture/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Facture
                .Include(f => f.StatutAbonnement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facture == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(facture);
        }

        // GET: Facture/Create
        public IActionResult Create()
        {
            ViewData["StatutAbonnementId"] = new SelectList(_context.StatutAbonnement, "Id", "Intitule");

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View();
        }

        // POST: Facture/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Dateheure,Prixtotal,StatutAbonnementId")] Facture facture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatutAbonnementId"] = new SelectList(_context.StatutAbonnement, "Id", "Intitule", facture.StatutAbonnementId);
            
            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(facture);
        }

        // GET: Facture/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Facture.FindAsync(id);
            if (facture == null)
            {
                return NotFound();
            }
            ViewData["StatutAbonnementId"] = new SelectList(_context.StatutAbonnement, "Id", "Intitule", facture.StatutAbonnementId);

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(facture);
        }

        // POST: Facture/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Dateheure,Prixtotal,StatutAbonnementId")] Facture facture)
        {
            if (id != facture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactureExists(facture.Id))
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
            ViewData["StatutAbonnementId"] = new SelectList(_context.StatutAbonnement, "Id", "Intitule", facture.StatutAbonnementId);

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(facture);
        }

        // GET: Facture/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Facture
                .Include(f => f.StatutAbonnement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facture == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(facture);
        }

        // POST: Facture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facture = await _context.Facture.FindAsync(id);
            if (facture != null)
            {
                _context.Facture.Remove(facture);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FactureExists(int id)
        {
            return _context.Facture.Any(e => e.Id == id);
        }

        // Facture User
        public IActionResult FacturesUser(String filter_typefacture, String filter_typeabonnement)
        {
            // Get UserId from Session
            var userId = HttpContext.Session.GetInt32("UserId");

            // Get Type Facture (Annulé ou Payé)
            var typefacture = _context.StatutAbonnement
                .Where(ta => ta.Intitule.Equals("Payé") || ta.Intitule.Equals("Annulé")) // Get Statut Abonnement Where Titre = Payé or where Titre = Annulé
                .ToList();

            // Get Type Abonnement
            var typeabonnement = _context.TypeAbonnements.ToList();

            // Create SelectList for type facture
            var select_typeFacture = new SelectList(typefacture, "Id", "Intitule");

            // Create SelectList for type abonnement
            var select_typeAbonnement = new SelectList(typeabonnement, "Id", "Titre");

            // Get Facture user
            var facturesUser = _context.Facture
                .Include(f => f.User).Include(f => f.StatutAbonnement).Include(f => f.TypeAbonnement)
                .Where(f => f.UserId.Equals(userId))
                .OrderByDescending(f => f.Dateheure)
                .ToList();

            // Filter Data based on values filter
            // If both filter present
            if (!string.IsNullOrEmpty(filter_typefacture) && !string.IsNullOrEmpty(filter_typeabonnement))
            {
                facturesUser = facturesUser
                    .Where(fa => fa.StatutAbonnementId == int.Parse(filter_typefacture))
                    .Where(fa => fa.TypeAbonnementId == int.Parse(filter_typeabonnement))
                    .ToList();
            // Else if Filter TypeFacture is present and Filter TypeAbonnement is absent
            } else if (!string.IsNullOrEmpty(filter_typefacture) && string.IsNullOrEmpty(filter_typeabonnement))
            {
                facturesUser = facturesUser
                    .Where(fa => fa.StatutAbonnementId == int.Parse(filter_typefacture))
                    .ToList();
            // Else if Filter TypeAbonnement is present and Filter TypeFacture is absent
            } else if (!string.IsNullOrEmpty(filter_typeabonnement) && string.IsNullOrEmpty(filter_typefacture))
            {
                facturesUser = facturesUser
                    .Where(fa => fa.TypeAbonnementId == int.Parse(filter_typeabonnement))
                    .ToList();
            }

            // Return View
            ViewData["facturesUser"] = facturesUser;
            ViewData["filter_typefacture"] = select_typeFacture;
            ViewData["filter_typeabonnement"] = select_typeAbonnement;
            
            // Set Navbar
            ViewData["Navbar"] = "user";

            return View();
        }

        // Details Facture User
        [Route("/Facture/DetailsFacture/{idFacture}")]
        public IActionResult DetailsFactureUser(int idFacture)
        {

            // Get Facture
            var facture = _context.Facture.Find(idFacture);

            // Get Details Facture User
            var detailsFactureUser = _context.AbonnementUsers.Include(a => a.Abonnement).Include(a => a.Abonnement.TypeAbonnement).Include(a => a.StatutAbonnement)
                .Where(a => a.FactureId.Equals(idFacture))
                .ToList();

            if (detailsFactureUser == null)
            {
                return NotFound();
            }

            // Add to view
            ViewData["detailsFactureUser"] = detailsFactureUser;
            ViewData["facture"] = facture;

            // Set Navbar
            ViewData["Navbar"] = "user";

            return View();
        }
    }
}

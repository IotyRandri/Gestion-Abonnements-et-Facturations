using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Projet_.NET_Core_M2_Devops_Mr_Naina;
using Projet_.NET_Core_M2_Devops_Mr_Naina.Models;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Controllers
{
    public class AbonnementUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConverter _converter;
        private readonly IRazorViewEngine _razorViewEngine;

        public AbonnementUsersController(ApplicationDbContext context, IConverter converter, IRazorViewEngine razorViewEngine)
        {
            _context = context;
            _converter = converter;
            _razorViewEngine = razorViewEngine;
        }

        // GET: AbonnementUsers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AbonnementUsers.Include(a => a.Abonnement).Include(a => a.StatutAbonnement).Include(a => a.User);

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AbonnementUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnementUsers = await _context.AbonnementUsers
                .Include(a => a.Abonnement)
                .Include(a => a.StatutAbonnement)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonnementUsers == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";


            return View(abonnementUsers);
        }

        // GET: AbonnementUsers/Create
        public IActionResult Create()
        {
            ViewData["AbonnementId"] = new SelectList(_context.Abonnement, "Id", "Nom");
            ViewData["StatutAbonnementId"] = new SelectList(_context.StatutAbonnement, "Id", "Intitule");
            ViewData["UserId"] = new SelectList(_context.Users.Select(u => new
            {
                u.Id,
                NomComplet = u.Nom + " " + u.Prenom
            }),
                "Id",
                "NomComplet"
            );

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View();
        }

        // POST: AbonnementUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,AbonnementId,NomFacture,StatutAbonnementId,DateHeureDebut,DateHeureFin")] AbonnementUsers abonnementUsers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abonnementUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbonnementId"] = new SelectList(_context.Abonnement, "Id", "Nom", abonnementUsers.AbonnementId);
            ViewData["StatutAbonnementId"] = new SelectList(_context.StatutAbonnement, "Id", "Intitule", abonnementUsers.StatutAbonnementId);
            ViewData["UserId"] = new SelectList(_context.Users.Select(u => new
            {
                u.Id,
                NomComplet = u.Nom + " " + u.Prenom
            }),
                "Id",
                "NomComplet"
            );

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(abonnementUsers);
        }

        // GET: AbonnementUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnementUsers = await _context.AbonnementUsers.FindAsync(id);
            if (abonnementUsers == null)
            {
                return NotFound();
            }
            ViewData["AbonnementId"] = new SelectList(_context.Abonnement, "Id", "Nom", abonnementUsers.AbonnementId);
            ViewData["StatutAbonnementId"] = new SelectList(_context.StatutAbonnement, "Id", "Intitule", abonnementUsers.StatutAbonnementId);
            ViewData["UserId"] = new SelectList(_context.Users.Select(u => new
            {
                u.Id,
                NomComplet = u.Nom + " " + u.Prenom
            }),
                "Id",
                "NomComplet"
            );

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(abonnementUsers);
        }

        // POST: AbonnementUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,AbonnementId,NomFacture,StatutAbonnementId,DateHeureDebut,DateHeureFin")] AbonnementUsers abonnementUsers)
        {
            if (id != abonnementUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abonnementUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonnementUsersExists(abonnementUsers.Id))
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
            ViewData["AbonnementId"] = new SelectList(_context.Abonnement, "Id", "Nom", abonnementUsers.AbonnementId);
            ViewData["StatutAbonnementId"] = new SelectList(_context.StatutAbonnement, "Id", "Intitule", abonnementUsers.StatutAbonnementId);
            ViewData["UserId"] = new SelectList(_context.Users.Select(u => new
            {
                u.Id,
                NomComplet = u.Nom + " " + u.Prenom
            }),
                "Id",
                "NomComplet"
            );

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(abonnementUsers);
        }

        // GET: AbonnementUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnementUsers = await _context.AbonnementUsers
                .Include(a => a.Abonnement)
                .Include(a => a.StatutAbonnement)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonnementUsers == null)
            {
                return NotFound();
            }

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View(abonnementUsers);
        }

        // POST: AbonnementUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abonnementUsers = await _context.AbonnementUsers.FindAsync(id);
            if (abonnementUsers != null)
            {
                _context.AbonnementUsers.Remove(abonnementUsers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonnementUsersExists(int id)
        {
            return _context.AbonnementUsers.Any(e => e.Id == id);
        }

        // AbonnementUsers Page
        /* Page where user adds an abonnement */
        public IActionResult AjoutAbonnement()
        {

            // Get abonnements and create SelectList for view
            var selectlist_abonnements = new SelectList(_context.Abonnement.Select(a => new { a.Id, a.Nom }), "Id", "Nom");

            // Add selectlist to view
            ViewData["abonnements"] = selectlist_abonnements;

            // Set Navbar
            ViewData["Navbar"] = "user";

            return View();
        }

        // Add Abonnement User
        [HttpPost]
        public async Task<IActionResult> AjoutAbonnement(AbonnementUsers abonnementUser)
        {

            // Check if inputs are valid
            if (ModelState.IsValid)
            {

                // Set UserId for new abonnement
                var userId = HttpContext.Session.GetInt32("UserId");

                if (userId == null)
                {
                    return RedirectToAction("Logout", "Home");
                }

                abonnementUser.UserId = (int)userId;

                // Set Default StatutAbonnementId
                var statutAbonnement = await _context.StatutAbonnement.Where(sta => sta.Intitule == "En cours").FirstOrDefaultAsync();


                // Return Error if statut abonnement not found
                if (statutAbonnement == null)
                {
                    // Add Error Message
                    ViewData["ErrorMessage"] = "Statut Abonnement not found!";

                    // Set Navbar
                    ViewData["Navbar"] = "user";
                    return View();
                }

                abonnementUser.StatutAbonnementId = statutAbonnement.Id;

                // Add to database
                _context.AbonnementUsers.Add(abonnementUser);
                await _context.SaveChangesAsync();

                // Add Success Message
                TempData["SuccessMessage"] = "Abonnement added successfully!";

                return RedirectToAction(nameof(AjoutAbonnement));

            }

            // Return errors
            // Get abonnements and create SelectList for view
            var selectlist_abonnements = new SelectList(_context.Abonnement.Select(a => new { a.Id, a.Nom }), "Id", "Nom");

            // Add selectlist to view
            ViewData["abonnements"] = selectlist_abonnements;

            // Set Navbar
            ViewData["Navbar"] = "user";

            return View();
        }


        // Abonnement to come Page
        /* Page where user can check his upcoming abonnement */
        public IActionResult AbonnementAVenir(String filter)
        {

            // Get UserId from Session
            var userId = HttpContext.Session.GetInt32("UserId");

            // Logout if no userId found
            if (userId == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            // Get Type Abonnements
            var typeabonnements = _context.TypeAbonnements.ToList();
            var select_typeAbonnements = new SelectList(typeabonnements, "Id", "Titre");

            // Get user abonnements
            var abonnementsUser = _context.AbonnementUsers.Include(a => a.Abonnement).Include(a => a.StatutAbonnement).Include(a => a.User)
                    .Where(ab_user => ab_user.UserId == (int)userId)
                    .Where(ab_user => ab_user.DateHeureDebut < DateTime.UtcNow)
                    .Where(ab_user => ab_user.StatutAbonnement.Intitule.Equals("En cours"))
                    .OrderByDescending(ab_user => ab_user.DateHeureDebut)
                    .ToList();

            // Get current datetime
            DateTime now = DateTime.UtcNow;


            // Calculate the next date limit from given filter
            foreach (var ab in abonnementsUser)
            {

                // Detach entity to avoid tracking
                _context.Entry(ab).State = EntityState.Detached;

                // Get difference of days/weeks/months/years between DateHeureDebut and DateHeureFin based on filter
                int difference = -1;
                switch (ab.Abonnement.TypeAbonnement.Titre)
                {
                    // Get Next Week's datetime
                    case "Hebdomadaire":
                        difference = GetWeeksDifference(ab.DateHeureDebut, now);

                        // Set difference to 1 if there is no difference for calculating price
                        if (difference == 0) { difference = 1; }
                        
                        ab.DateHeureFin = ab.DateHeureDebut.AddDays(7 * difference);
                        break;

                    // Get Next Month's datetime
                    case "Mensuelle":
                        difference = GetMonthsDifference(ab.DateHeureDebut, now);
                        
                        // Set difference to 1 if there is no difference for calculating price
                        if (difference == 0) { difference = 1; }

                        ab.DateHeureFin = ab.DateHeureDebut.AddMonths(difference);
                        break;

                    // Get Next Year's datetime 
                    case "Annuelle":
                        difference = GetYearsDifference(ab.DateHeureDebut, now);

                        // Set difference to 1 if there is no difference for calculating price
                        if (difference == 0) { difference = 1; }

                        ab.DateHeureFin = ab.DateHeureDebut.AddYears(difference);
                        break;
                }

                // Check if there was no error calculating difference between dates
                if (difference == -1)
                {
                    // Return Error
                    TempData["ErrorMessage"] = "Error in calculating dates!";
                    return RedirectToAction(nameof(AbonnementAVenir));
                }

                // Check if DateHeureFin < now
                if (ab.DateHeureFin < now)
                {
                    difference += 1; // Add 1 difference if current date is > than dateheurefin
                }

                // Calculate Price to Pay for each abonnement based on difference
                ab.PrixEstimation = ab.Abonnement.Prix * difference;

                // Set difference
                ab.Difference = difference;

            }


            // Filter data based on filter
            if (!string.IsNullOrEmpty(filter))
            {

                int typeabonnementId = int.Parse(filter);

                // Filter list based on filter (type abonnement)
                abonnementsUser = abonnementsUser.Where(ab => ab.Abonnement.TypeAbonnementId == typeabonnementId).ToList();

            }

            // Get Total Price to Pay
            var totalprice = abonnementsUser.Sum(a => a.Abonnement.Prix);
            var totalprice_estimation = abonnementsUser.Sum(a => a.PrixEstimation);


            // Add Type Abonnements to view
            ViewData["typeAbonnements"] = select_typeAbonnements;

            // Add user abonnements to view
            ViewData["abonnementsUser"] = abonnementsUser;

            // Add total price to view
            ViewData["prixtotal"] = totalprice.ToString("N");
            ViewData["prixtotalestimation"] = totalprice_estimation.ToString("N");

            // Set Navbar
            ViewData["Navbar"] = "user";

            return View();
        }

        // Paiement Abonnement
        /*  - Paiement ou annulation abonnement user 
            - Export en pdf
         */
        public async Task<IActionResult> PaiementAbonnement(String json_abonnementsuser, String filter, String typepaiement, double prixtotal, double prixtotalestimation)
        {
            // Check value type paiement
            if (string.IsNullOrEmpty(typepaiement))
            {
                // Return Error 
                TempData["ErrorMessage"] = "Invalid Type Paiement";
                return RedirectToAction(nameof(AbonnementAVenir));
            }

            // Check value filter
            if (string.IsNullOrEmpty(filter))
            {
                // Return Error
                TempData["ErrorMessage"] = "Please select a filter!";
                return RedirectToAction(nameof(AbonnementAVenir));
            }

            // Deserialize abonnementsuser
            var abonnementsuser = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AbonnementUsers>>(json_abonnementsuser);

            if (abonnementsuser == null)
            {
                TempData["ErrorMessage"] = "No Data";
                return RedirectToAction(nameof(AbonnementAVenir));
            }

            // Check integrity data and value filter
            foreach (var ab in abonnementsuser)
            {
                if (ab.Abonnement.TypeAbonnementId != int.Parse(filter))
                {
                    // Return Error
                    TempData["ErrorMessage"] = "Incorrect data or filter!";
                    return RedirectToAction(nameof(AbonnementAVenir));
                }
            }

            // Get UserId from session
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            // Get User info from userId from db
            var user = _context.Users.Find(userId);

            // All data valid
            // Paiement
            if (typepaiement.Equals("Payer"))
            {
                String pdfviewname = "/Views/AbonnementUsers/ExportPDF.cshtml";
                String intitule_statut_abonnement = "Payé";
                return await Facture(intitule_statut_abonnement, "Payé", abonnementsuser, user, filter, prixtotal, prixtotalestimation, pdfviewname);
            }

            // Annulation
            else if (typepaiement.Equals("Annuler")) 
            {
                String pdfviewname = "/Views/AbonnementUsers/ExportPDFAnnulation.cshtml";
                String intitule_statut_abonnement = "Annulé";
                return await Facture(intitule_statut_abonnement, "Annulé", abonnementsuser, user, filter, prixtotal, prixtotalestimation, pdfviewname);
            }

            return RedirectToAction(nameof(AbonnementAVenir));
        }

        // Pay/Cancel Abonnement User
        private async Task<IActionResult> Facture(String valeur_statutabonnement, String typefacture, List<AbonnementUsers> abonnementsuser, Users user, String filter, double prixtotal, double prixtotalestimation, String pdfviewname)
        {

            // Update values in database

            // Get current datetime
            DateTime now = DateTime.UtcNow;

            // Get Statut Abonnement where Intitule = Valeur Statut Abonnement
            var statutabonnement = _context.StatutAbonnement.First(sta => sta.Intitule.Equals(valeur_statutabonnement));

            // Check if Statut abonnement exists from valeur
            if (statutabonnement == null)
            {
                // Return Error
                TempData["ErrorMessage"] = "Statut Abonnement Invalid!";
                return RedirectToAction(nameof(AbonnementAVenir));
            }

            // Insert new record in Facture Table
            String nomfacture = user.Username + "_" + statutabonnement.Intitule + "_(" + now + ")";

            Facture facture = null;

            // Annulation
            if (typefacture.Equals("Annulé"))
            {

                // Get Commission Annulation
                var commissionannulation = await _context.CommissionAnnulation.ToListAsync();

                // Calculer prix total annulation
                foreach (var ab in abonnementsuser)
                {
                    foreach (var com in commissionannulation)
                    {
                        if (ab.Abonnement.Prix >= com.Prixmin && ab.Abonnement.Prix <= com.Prixmax)
                        {
                            ab.PrixAnnulation = ab.PrixEstimation - (ab.Abonnement.Prix * com.Pourcentage);
                            ab.PrixCommission = (ab.Abonnement.Prix * com.Pourcentage);
                            break;
                        }
                    }
                }

                // Calculer prix total annulation
                prixtotalestimation = abonnementsuser.Sum(a => a.PrixEstimation);
                double prixtotalannulation = abonnementsuser.Sum(a => a.PrixAnnulation);

                facture = new Facture
                {
                    Nom = nomfacture,
                    UserId = user.Id,
                    Dateheure = now,
                    Prixtotal = prixtotalannulation,
                    StatutAbonnementId = statutabonnement.Id,
                    TypeAbonnementId = int.Parse(filter) // filter = TypeAbonnementId
                };

                // Add to database
                _context.Facture.Add(facture);
                await _context.SaveChangesAsync();

                // Update values for each abonnement
                foreach (var ab in abonnementsuser)
                {

                    // Update values
                    ab.DateHeureFin = now; // Current DateTime
                    ab.StatutAbonnementId = statutabonnement.Id; // Statut Abonnement
                    ab.FactureId = facture.Id; // Set id of facture
                    ab.PrixEstimation = ab.PrixEstimation; // Set PrixEstimation for Details facture
                    ab.PrixCommission = ab.PrixCommission; // Set PrixCommission for Details facture
                    ab.PrixTotal = ab.PrixAnnulation; // Set PrixTotal to PrixAnnulation for Details facture

                    // Reattach entity for db interaction
                    _context.Entry(ab).State = EntityState.Modified;
                }

                // Paiement
            } else if (typefacture.Equals("Payé"))
            {

                facture = new Facture
                {
                    Nom = nomfacture,
                    UserId = user.Id,
                    Dateheure = now,
                    Prixtotal = prixtotalestimation,
                    StatutAbonnementId = statutabonnement.Id,
                    TypeAbonnementId = int.Parse(filter) // filter = TypeAbonnementId
                };

                _context.Facture.Add(facture);
                await _context.SaveChangesAsync();

                // Get Intitule TypeAbonnement from filter
                var typeabonnement = await _context.TypeAbonnements.FindAsync(int.Parse(filter));


                // Update values for each abonnement
                foreach (var ab in abonnementsuser)
                {

                    // Update values
                    ab.DateHeureFin = now; // Current DateTime
                    ab.StatutAbonnementId = statutabonnement.Id; // Statut Abonnement
                    ab.FactureId = facture.Id; // Set id of facture
                    ab.PrixEstimation = ab.PrixEstimation; // Set PrixEstimation for Details facture
                    ab.PrixCommission = 0; // Set PrixCommission = 0 for Details facture (payé = no comission)
                    ab.PrixTotal = ab.PrixEstimation; // Set PrixTotal to PrixAnnulation for Details facture

                    // Reattach entity for db interaction
                    _context.Entry(ab).State = EntityState.Modified;
                }

            }



            // Save to database
            _context.AbonnementUsers.UpdateRange(abonnementsuser);
            await _context.SaveChangesAsync();

            // Export to PDF
            return await ExportPDF(nomfacture, user, facture, prixtotal, prixtotalestimation, pdfviewname);
        }

        // Export Data User Abonnements/ User Facture to PDF
        private async Task<IActionResult> ExportPDF(String filename, Users user, Facture facture, double prixtotal, double prixtotalestimation, String pdfviewname)
        {
            try
            {

                // Get Abonnements User from factureId
                var abonnementUser = await _context.AbonnementUsers
                    .Include(a => a.Abonnement)
                    .Include(a => a.StatutAbonnement)
                    .Include(a => a.User)
                    .Include(a => a.Facture.TypeAbonnement)
                    .Where(ab_user => ab_user.FactureId == facture.Id)
                    .ToListAsync();

                // Add userinfo to View
                ViewData["user"] = user;
                ViewData["prixtotal"] = prixtotal;
                ViewData["prixtotalestimation"] = prixtotalestimation;
                ViewData["facture"] = facture;
                ViewData["abonnementsUser"] = abonnementUser;

                // Render Razor View to String
                /* First parameter is view */
                var htmlcontent = await RenderViewString(pdfviewname);

                // Check if htmlcontent is not null or empty
                if (string.IsNullOrEmpty(htmlcontent))
                {
                    throw new Exception("Generated HTML content is empty or null.");
                }

                // Create PDF from HTML content
                byte[] pdf = GeneratePdf(htmlcontent);

                // Check if PDF is generated
                if (pdf == null || pdf.Length == 0)
                {
                    throw new Exception("Failed to generate PDF.");
                }

                // Set headers to prevent caching
                Response.Headers.Clear(); // Clear any previous headers
                Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                Response.Headers.Append("Pragma", "no-cache");
                Response.Headers.Append("Expires", "0");

                // Return PDF as file with content-disposition to force download
                return new FileContentResult(pdf, "application/pdf")
                {
                    FileDownloadName = filename + ".pdf"
                };

            } catch(Exception ex)
            {
                // Log the error and show a message
                Console.WriteLine("ERROR: ----------------- while generating or downloading PDF.");
                Console.WriteLine("Exception: ");
                Console.WriteLine(ex);
                TempData["ErrorMessage"] = "An error occurred while generating the PDF.";
                return RedirectToAction(nameof(AbonnementAVenir));
            }
            
        }

        // Render Razor View to String
        private async Task<String> RenderViewString(String viewName)
        {
            var controllerContext = ControllerContext;
            var viewData = new ViewDataDictionary(ViewData);

            // Find the Razor view
            var viewResult = _razorViewEngine.GetView(
                executingFilePath: null,
                viewPath: viewName,
                isMainPage: false
            );

            if (!viewResult.Success)
            {
                throw new FileNotFoundException($"The view '{viewName}' was not found.");
            }

            using (var sw = new StringWriter())
            {
                var viewContext = new ViewContext(controllerContext, viewResult.View, viewData, TempData, sw, new HtmlHelperOptions());
                await viewResult.View.RenderAsync(viewContext); // Use Async Rendering
                return sw.ToString();
            }
        }

        // Convert HTML to PDF
        private byte[] GeneratePdf(String htmlcontent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Out = null // This is where the output goes (null means it will return the byte array)

                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        HtmlContent = htmlcontent,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontName = "Arial", FontSize = 11 }
                    }
                }
            };

            return _converter.Convert(doc);
        }

        // Method to get number of days difference between two dates
        private int GetDaysDifference(DateTime startDate, DateTime endDate)
        {
            TimeSpan ts = endDate - startDate;
            return ts.Days;
        }

        // Method to get number of weeks difference between two dates
        private int GetWeeksDifference(DateTime startDate, DateTime endDate)
        {
            return GetDaysDifference(startDate, endDate) / 7;
        }

        // Method to get number of months difference between two dates
        private int GetMonthsDifference(DateTime startDate, DateTime endDate)
        {
            return ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month;
        }

        // Method to get number of years difference between two dates
        private int GetYearsDifference(DateTime startDate, DateTime endDate)
        {
            return endDate.Year - startDate.Year;
        }
    }
}

        

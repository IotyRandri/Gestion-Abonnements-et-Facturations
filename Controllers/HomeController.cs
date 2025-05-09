using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projet_.NET_Core_M2_Devops_Mr_Naina.Models;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        // Login Page
        public IActionResult Index()
        {
            return View();
        }

        // Check Login
        [HttpPost]
        public IActionResult Index(String username, String motdepasse)
        {

            // Check if username is valid
            if (string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("Username", "Username Invalid!");
            }
            
            // Check if motdepasse is valid
            if (string.IsNullOrEmpty(motdepasse))
            {
                ModelState.AddModelError("Motdepasse", "Motdepasse Invalid!");
            }


            // If Inputs are valid
            if ((!string.IsNullOrEmpty(username)) && (!string.IsNullOrEmpty(motdepasse)))
            {

                // Check if user exists
                var _user = _context.Users.FirstOrDefault(u => 
                    (u.Username == username && u.Motdepasse == motdepasse)
                );

                // Redirect to HomeAdmin if user is admin
                if (_user != null && _user.Username.Equals("admin") && _user.Motdepasse.Equals("admin"))
                {
                    return RedirectToAction("HomeAdmin");

                } 
                
                // Redirect to HomeUser if user is user
                else if (_user != null)
                {

                    // Store userId in Session
                    HttpContext.Session.SetInt32("UserId", _user.Id);

                    // Store Username in Session
                    HttpContext.Session.SetString("Username", _user.Username);

                    return RedirectToAction(nameof(HomeUser));
                }

                // Return error if no user found
                else {
                    ViewData["ErrorMessage"] = "No User Found!";
                    return View();
                }

            }

            return View();

        }

        // Home Admin Page
        public IActionResult HomeAdmin()
        {

            // Set Navbar
            ViewData["Navbar"] = "admin";

            return View();
        }

        // Home User Page
        public IActionResult HomeUser(int pageIndex = 1)
        {

            // Get UserId from Session
            var userId = HttpContext.Session.GetInt32("UserId");

            // Redirect to Login if no userId found
            if (userId == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            // Get Username from Session
            var username = HttpContext.Session.GetString("Username");


            // Number of items per page for pagination
            int pageSize = 10;

            // Get Total Number of User Abonnements for pagination
            var totalAbonnements = _context.AbonnementUsers.Count();

            // Get Total Number of Pages for pagination
            var totalPages = (int)Math.Ceiling(totalAbonnements / (double)pageSize);

            /* Get User Data from db (for dahsboard)
                * abonnements 
                * top 10 des abonnements plus cher
                * top 10 des abonnements moins cher
                * moyenne des depenses par mois, année
            */

            // Get User abonnements by datedebut descending
            var abonnementsUser = _context.AbonnementUsers.Include(a => a.Abonnement).Include(a => a.StatutAbonnement).Include(a => a.User)
                .Where(ab_user => ab_user.UserId == (int)userId)
                .OrderByDescending(ab_user => ab_user.DateHeureDebut)
                .ToList();

            // Get id StatutAbonnement where Intitule = Payé
            int StatutAbonnementId = _context.StatutAbonnement.Where(sa => sa.Intitule.Equals("Payé")).First().Id;

            // Get top 10 des abonnements plus cher au total
            var top10AbonnementsCherTotal = abonnementsUser
                .Where(ab => ab.StatutAbonnementId == StatutAbonnementId)
                .GroupBy(a => a.Abonnement)
                .Select(ab => new AbonnementUsersDTO
                {
                    Abonnement = ab.Key,
                    NomAbonnement = ab.Key.Nom,
                    TotalPrix = ab.Sum(a => a.Abonnement.Prix)
                })
                .OrderByDescending(ab => ab.TotalPrix) // Sort by TotalPrix Descending
                .Take(10)
                .ToList() ?? throw new Exception("Top 10 Abonnements Plus Cher is null");

            // Get Labels and data top 10 abonnements plus cher
            var labelstop10Cher = top10AbonnementsCherTotal.Select(ab => ab.NomAbonnement).ToList();
            var datatop10Cher = top10AbonnementsCherTotal.Select(ab => ab.TotalPrix).ToList();

            // Get Top 10 Abonnements moins cher au total
            var top10AbonnementsMoinsCherTotal = abonnementsUser
                .Where(ab => ab.StatutAbonnementId == StatutAbonnementId)
                .GroupBy(a => a.Abonnement)
                .Select(ab => new AbonnementUsersDTO
                {
                    Abonnement = ab.Key,
                    NomAbonnement = ab.Key.Nom,
                    TotalPrix = ab.Sum(a => a.Abonnement.Prix)
                })
                .OrderBy(ab => ab.TotalPrix) // Sort by TotalPrix Ascending
                .Take(10)
                .ToList() ?? throw new Exception("Top 10 Abonnements Moins Cher is null");

            // Get Labels and data top 10 abonnements plus moins
            var labelstop10MoinsCher = top10AbonnementsMoinsCherTotal.Select(ab => ab.NomAbonnement).ToList();
            var datatop10MoinsCher = top10AbonnementsMoinsCherTotal.Select(ab => ab.TotalPrix).ToList();



            // Get Type Abonnement
            var typesAbonnement = _context.TypeAbonnements.ToList();
            var select_typesAbonnement = new SelectList(typesAbonnement, "Id", "Nom");

            // Get Current DateTime
            var currentDate = DateTime.Now;

            // Get Moyenne de depense by type abonnement
            var moyennedepense = abonnementsUser
                .Where(ab => ab.StatutAbonnementId == StatutAbonnementId)
                .Where(a => a.DateHeureDebut.Year == currentDate.Year) // Get Only Data from current Year based on DateHeureDebut value
                .Where(a => a.DateHeureDebut < currentDate)
                .GroupBy(a => new {a.DateHeureDebut.Month }) // Group By Month
                .Select(group => new AbonnementUsersDTO
                {
                    Mois = group.Key.Month,
                    MoyenneDepenseMois = group.Average(ab => ab.Abonnement.Prix)
                })
                .OrderBy(a => a.Mois)
                .ToList() ?? throw new Exception("MoyenneDepense is null");

            // Get Labels Moyenne de depense by type abonnement
            var labelsMoyenneDepense = moyennedepense.Select(ab => ConvertMonth(ab.Mois)).ToList();
            var dataMoyenneDepense = moyennedepense.Select(ab => ab.MoyenneDepenseMois).ToList();

            // Paginate Abonnements Users
            /* Only data to display on table not on graphs */
            abonnementsUser = abonnementsUser
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();

            // Add user abonnements to view
            ViewData["abonnementsUser"] = abonnementsUser;
            
            // Add top 10 des abonnements plus cher to view
            ViewData["topabonnementsCher"] = top10AbonnementsCherTotal;
            ViewData["labels_top10Cher"] = labelstop10Cher;
            ViewData["data_top10Cher"] = datatop10Cher;

            // Add top 10 des abonnements moins cher to view
            ViewData["topabonnementsMoinsCher"] = top10AbonnementsMoinsCherTotal;
            ViewData["labels_top10MoinsCher"] = labelstop10MoinsCher;
            ViewData["data_top10MoinsCher"] = datatop10MoinsCher;

            // Add Moyenne de depense by type abonnement to view
            ViewData["moyenneDepense"] = moyennedepense;
            ViewData["labels_moyenneDepense"] = labelsMoyenneDepense;
            ViewData["data_moyenneDepense"] = dataMoyenneDepense;

            // Add typesAbonnement to view
            ViewData["typesAbonnement"] = select_typesAbonnement;

            // Add pagination parameters to view
            ViewData["pageIndex"] = pageIndex;
            ViewData["totalPages"] = totalPages;

            // Add username to view
            ViewData["username"] = username;

            // Set navbar
            ViewData["Navbar"] = "user";

            return View();
        }

        // Register Page
        public IActionResult Register()
        {

            return View();
        }

        // Check Register
        [HttpPost]
        public IActionResult Register([Bind("Nom,Prenom,DateNaissance,Adresse,Username,Motdepasse")] Users user)
        {

            // Check if Inputs are valid
            if (ModelState.IsValid)
            {
                // Add to db
                _context.Users.Add(user);
                _context.SaveChanges();

                // Set Success Message using TempData
                TempData["SuccessMessage"] = "User added successfully!";

                return RedirectToAction(nameof(Register)); // Redirect to GET action after saving

            }

            return View();
        }

        // Logout
        public IActionResult Logout()
        {
            // Clear Session
            HttpContext.Session.Clear();

            // Redirect to Login Page
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Convert numeric Month Values to String Month Names
        private String ConvertMonth(int value)
        {
            String[] months = new String[] {
                "Janvier", "Fevrier", "Mars", "Avril", "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre", "Novembre", "Decembre"
            };

            return months[value - 1];
        }
    }
}

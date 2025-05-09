using Microsoft.AspNetCore.Mvc;

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public String Salama()
        {
            return "Salama";
        }

        public IActionResult Bonjour(String nom, int count)
        {

            ViewData["nom"] = nom;
            ViewData["count"] = count;
            return View();
        }
    }
}

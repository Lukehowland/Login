using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LukeCodificador.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        { 
            ViewBag.Email = User.Identity.Name;
            return View();
        }

        // Puedes agregar otras acciones en el HomeController si es necesario.
        // Por ejemplo, una acción para mostrar la página de error:
        public IActionResult Error()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LukeCodificador.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        public IActionResult SobreElInge()
        {
            return View();
        }
    }
}
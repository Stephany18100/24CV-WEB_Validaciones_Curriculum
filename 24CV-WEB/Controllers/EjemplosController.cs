using _24CV_WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace _24CV_WEB.Controllers
{
    public class EjemplosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            Persona persona = new Persona();
            persona.Nombre = "Joel";
            persona.Apellidos = "Perez";
            persona.FechaNacimiento = new DateTime(2002, 10, 11);

            return View(persona);
        }
    }
}

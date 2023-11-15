using _24CV_WEB.Models;
using _24CV_WEB.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace _24CV_WEB.Controllers
{
    public class ValidacionesController : Controller
    {
        private readonly ICurriculumService _curriculumService;

        public ValidacionesController(ICurriculumService curriculumService)
        {
            _curriculumService = curriculumService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarInformacion(Curriculum model) {

            string mensaje = "";
            //model.RutaFoto = "FakePath";

            if (ModelState.IsValid)
            {
                var response = _curriculumService.Create(model).Result;

                mensaje = response.Message;
                TempData["msj"] = mensaje;
                return RedirectToAction("Index");
            }
            else
            {
                mensaje = "Datos incorrectos";
                TempData["msj"] = mensaje;

                return View("Index",model);
            }

        }

        public IActionResult Lista()
        {
            return View(_curriculumService.GetAll());
        }

        public IActionResult VPC(int id)
        {
            // Obtén el Curriculum correspondiente según el id
            var curriculum = _curriculumService.GetById(id);

            if (curriculum == null)
            {
                return NotFound();
            }

            return View("VPC", curriculum);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var curriculum = _curriculumService.GetById(id);
            return View(curriculum);
        }

        [HttpPost]
        public IActionResult Editar(Curriculum model)
        {
            _curriculumService.Update(model);
            return RedirectToAction("Lista");
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var curriculum = _curriculumService.GetById(id);
            return View(curriculum);
        }

        [HttpPost]
        public IActionResult ConfirmarEliminar(int id)
        {
            _curriculumService.Delete(id);
            return RedirectToAction("Lista");
        }
    }

}

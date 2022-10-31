using Microsoft.AspNetCore.Mvc;
using Turismo.Models;
using Turismo.Servicios;

namespace Turismo.Controllers
{
    public class CiudadController: Controller
    {
        private readonly IRepositorioCiudad repositorioCiudad;

        public CiudadController(IRepositorioCiudad repositorioCiudad)
        {
            this.repositorioCiudad = repositorioCiudad;
        }

        public async Task<IActionResult> Index()
        {
            var ciudad = await repositorioCiudad.Obtener();

            return View(ciudad);
        }

        public IActionResult Crear()
        {
            return View();       
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Ciudad ciudad)
        {
            if (!ModelState.IsValid)
            {
                return View(ciudad);
            }

            var yaExisteCiudad =
                await repositorioCiudad.Existe(ciudad.Descripcion);

            if (yaExisteCiudad)
            {
                ModelState.AddModelError(nameof(ciudad.Descripcion), $"{ciudad.Descripcion} ya existe");

                return View(ciudad);
            }

            await repositorioCiudad.Crear(ciudad);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteCiudad(string descripcion)
        {
            var yaExisteCiudad =
                await repositorioCiudad.Existe(descripcion);

            if (yaExisteCiudad)
            {
                return Json($"{descripcion} ya existe");
            }

            return Json(true);
        }
    }
}

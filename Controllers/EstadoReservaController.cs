using Microsoft.AspNetCore.Mvc;
using Turismo.Models;
using Turismo.Servicios;

namespace Turismo.Controllers
{
    public class EstadoReservaController : Controller
    {
        private readonly IRepositorioEstadoReserva repositorioEstadoReserva;

        public EstadoReservaController(IRepositorioEstadoReserva repositorioEstadoReserva)
        {
            this.repositorioEstadoReserva = repositorioEstadoReserva;
        }

        public async Task<IActionResult> Index()
        {
            var estadoReserva = await repositorioEstadoReserva.Obtener();

            return View(estadoReserva);
        }

        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(EstadoReserva estadoReserva)
        {
            if (!ModelState.IsValid)
            {
                return View(estadoReserva);
            }

            var yaExisteEstadoReserva =
                await repositorioEstadoReserva.Existe(estadoReserva.Descripcion);

            if (yaExisteEstadoReserva)
            {
                ModelState.AddModelError(nameof(estadoReserva.Descripcion), 
                    $"{estadoReserva.Descripcion} ya existe");

                return View(estadoReserva);
            }

            await repositorioEstadoReserva.Crear(estadoReserva);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteEstadoReserva(string descripcion)
        {
            var yaExisteEstadoReserva =
                await repositorioEstadoReserva.Existe(descripcion);

            if (yaExisteEstadoReserva)
            {
                return Json($"{descripcion} ya existe");
            }

            return Json(true);
        }
    }
}

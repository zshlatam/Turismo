using Microsoft.AspNetCore.Mvc;
using Turismo.Models;
using Turismo.Servicios;

namespace Turismo.Controllers
{
    public class EstadoDepartamentoController: Controller
    {
        private readonly IRepositorioEstadoDepartamento repositorioEstadoDepartamento;

        public EstadoDepartamentoController(IRepositorioEstadoDepartamento repositorioEstadoDepartamento)
        {
            this.repositorioEstadoDepartamento = repositorioEstadoDepartamento;
        }

        public async Task<IActionResult> Index()
        {
            var estadoDepartamento = await repositorioEstadoDepartamento.Obtener();

            return View(estadoDepartamento);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(EstadoDepartamento estadoDepartamento)
        {
            if (!ModelState.IsValid)
            {
                return View(estadoDepartamento);
            }

            var yaExisteEstadoDepartamento =
                await repositorioEstadoDepartamento.Existe(estadoDepartamento.Descripcion);

            if (yaExisteEstadoDepartamento)
            {
                ModelState.AddModelError(nameof(estadoDepartamento.Descripcion), 
                    $"{estadoDepartamento.Descripcion} ya existe");

                return View(estadoDepartamento);
            }

            await repositorioEstadoDepartamento.Crear(estadoDepartamento);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteEstadoDepartamento(string descripcion)
        {
            var yaExisteEstadoDepartamento =
                await repositorioEstadoDepartamento.Existe(descripcion);

            if (yaExisteEstadoDepartamento)
            {
                return Json($"{descripcion} ya existe");
            }

            return Json(true);
        }
    }
}

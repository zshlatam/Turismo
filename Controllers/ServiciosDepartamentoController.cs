using Microsoft.AspNetCore.Mvc;
using Turismo.Models;
using Turismo.Servicios;

namespace Turismo.Controllers
{
    public class ServiciosDepartamentoController: Controller
    {
        private readonly IRepositorioServiciosDepartamento repositorioServiciosDepartamento;

        public ServiciosDepartamentoController(IRepositorioServiciosDepartamento repositorioServiciosDepartamento)
        {
            this.repositorioServiciosDepartamento = repositorioServiciosDepartamento;
        }
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Crear(ServiciosDepartamento serviciosDepartamento)
        {
            if (!ModelState.IsValid)
            {
                return View(serviciosDepartamento);
            }

            repositorioServiciosDepartamento.Crear(serviciosDepartamento);

            return View();
        }

    }
}

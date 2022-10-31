using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Turismo.Models;
using Turismo.Servicios;

namespace Turismo.Controllers
{
    public class MantenimientoController: Controller
    {
        private readonly IRepositorioDepartamento repositorioDepartamento;
        private readonly IRepositorioMantenimiento repositorioMantenimiento;

        public MantenimientoController(IRepositorioDepartamento repositorioDepartamento, 
            IRepositorioMantenimiento repositorioMantenimiento)
        {
            this.repositorioDepartamento = repositorioDepartamento;
            this.repositorioMantenimiento = repositorioMantenimiento;
        }
        public async Task<IActionResult> Index()
        {
            var listaMantenimiento = await repositorioMantenimiento.Buscar();
            return View(listaMantenimiento);
        }
        public async Task<IActionResult> Crear()
        {
            var modelo = new MantenimientoCreacionViewModel();
            modelo.TiposDepartamentos = await ObtenerDepartamentos();
            return View(modelo);
        }
        [HttpPost]
        public async Task<IActionResult> Crear(MantenimientoCreacionViewModel  modelo)
        {

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            await repositorioMantenimiento.Crear(modelo);

            return RedirectToAction("Index");
        }



        private async Task<IEnumerable<SelectListItem>> ObtenerDepartamentos()
        {
            var departamento = await repositorioDepartamento.Buscar();
            return departamento.Select(x => new SelectListItem(x.Descripcion, x.Id.ToString()));
        }
    }
}

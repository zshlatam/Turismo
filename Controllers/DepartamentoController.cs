using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.IO;
using Turismo.Models;
using Turismo.Servicios;

namespace Turismo.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IRepositorioCiudad repositorioCiudad;
        private readonly IRepositorioEstadoDepartamento repositorioEstadoDepartamento;
        private readonly IRepositorioDepartamento repositorioDepartamento;
        private readonly IMapper mapper;

        public DepartamentoController(IRepositorioCiudad repositorioCiudad,
            IRepositorioEstadoDepartamento repositorioEstadoDepartamento,
            IRepositorioDepartamento repositorioDepartamento,
            IMapper mapper)
        {
            this.repositorioCiudad = repositorioCiudad;
            this.repositorioEstadoDepartamento = repositorioEstadoDepartamento;
            this.repositorioDepartamento = repositorioDepartamento;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Crear()
        {   
            var modelo = new DepartamentoCreacionViewModel();
            modelo.TiposCiudades = await ObtenerCiudades();
            modelo.TiposEstadoDepartamento = await ObtenerEstadoDepartamento();
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(DepartamentoCreacionViewModel departamento)
        {
            var tiposCiudades = await repositorioCiudad.ObtenerPorId(departamento.CiudadId);
            var tiposEstadoDepartamento = await repositorioEstadoDepartamento.ObtenerPorId(
                                                departamento.EstadoDepartamentoId);

            if (!ModelState.IsValid)
            {
                departamento.TiposCiudades = await ObtenerCiudades();
                departamento.TiposEstadoDepartamento = await ObtenerEstadoDepartamento();
                return View(departamento);
            }

            await repositorioDepartamento.Crear(departamento);

            return RedirectToAction("Index");
        }

        //sirve para obtener el combo de ciudades
        private async Task<IEnumerable<SelectListItem>> ObtenerCiudades()
        {
            var tiposCiudades = await repositorioCiudad.Obtener();

            return tiposCiudades.Select(x => new SelectListItem(x.Descripcion, x.Id.ToString()));
        }
        private async Task<IEnumerable<SelectListItem>> ObtenerEstadoDepartamento()
        {
            var tiposEstadoDepartamento = await repositorioEstadoDepartamento.Obtener();

            return tiposEstadoDepartamento.Select(x =>
                                                new SelectListItem(x.Descripcion, x.Id.ToString()));
        }


        public async Task<IActionResult> Index()
        {
            var listaDepartamentos = await repositorioDepartamento.Buscar();
            return View(listaDepartamentos);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var departamento = await repositorioDepartamento.ObtenerPorId(id);

            var modelo = mapper.Map<DepartamentoCreacionViewModel>(departamento);

            modelo.TiposCiudades = await ObtenerCiudades();
            modelo.TiposEstadoDepartamento = await ObtenerEstadoDepartamento();

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(DepartamentoCreacionViewModel departamentoEditar)
        {
            var departamento = await repositorioDepartamento.ObtenerPorId(departamentoEditar.Id);

            var TiposCiudades = await repositorioCiudad.ObtenerPorId(departamentoEditar.CiudadId);
            var TiposEstadoDepartamento = await repositorioCiudad.ObtenerPorId(departamentoEditar.EstadoDepartamentoId);

            await repositorioDepartamento.Actualizar(departamentoEditar);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Borrar(int id)
        {
            var departamento = await repositorioDepartamento.ObtenerPorId(id);

            return View(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarDepartamento(int id)
        {
            var departamento = await repositorioDepartamento.ObtenerPorId(id);

            await repositorioDepartamento.Borrar(id);

            return RedirectToAction("Index");
        }

    }
}

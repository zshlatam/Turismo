using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Turismo.Models;
using Turismo.Servicios;

namespace Turismo.Controllers
{
    public class ReservaController: Controller
    {
        private readonly IRepositorioDepartamento repositorioDepartamento;
        private readonly IRepositorioUsuario repositorioUsuario;
        private readonly IRepositorioReserva repositorioReserva;
        private readonly IRepositorioEstadoReserva repositorioEstadoReserva;
        private readonly IRepositorioEstadoDepartamento repositorioEstadoDepartamento;
        private readonly IMapper mapper;

        public ReservaController(IRepositorioDepartamento repositorioDepartamento,
            IRepositorioUsuario repositorioUsuario, IRepositorioReserva repositorioReserva,
            IRepositorioEstadoReserva repositorioEstadoReserva,
            IRepositorioEstadoDepartamento repositorioEstadoDepartamento, IMapper mapper)
        {
            this.repositorioDepartamento = repositorioDepartamento;
            this.repositorioUsuario = repositorioUsuario;
            this.repositorioReserva = repositorioReserva;
            this.repositorioEstadoReserva = repositorioEstadoReserva;
            this.repositorioEstadoDepartamento = repositorioEstadoDepartamento;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var listado = await repositorioReserva.Buscar();
            return View(listado);
        }

        public async Task<IActionResult> Crear()
        {

            if (User.Identity.IsAuthenticated)
            {
                var claims = User.Claims.ToList();
                var usuarioReal = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                var id = usuarioReal.Value;
                ViewBag.MiId = id;
            }
            var departamentoId = repositorioDepartamento.ObtenerIdDepartamento();
            var modelo = new ReservaCreacionViewModel();
            modelo.Departamentos = await ObtenerDepartamentos();
            return View(modelo);

        }
        [HttpPost]
        public async Task<IActionResult> Crear(ReservaCreacionViewModel modelo)
        {

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            await repositorioReserva.Crear(modelo);

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerDepartamentos()
        {
            var departamentos = await repositorioDepartamento.Buscar();
            return departamentos.Select(x => new SelectListItem(x.Descripcion, x.Id.ToString()));
        }
        //private async Task<IEnumerable<SelectListItem>> ObtenerUsuarios()
        //{
        //    var usuarios = await repositorioUsuario.Buscar();
        //    return usuarios.Select(x => new SelectListItem(x.Descripcion, x.Id.ToString()));
        //}
        private async Task<IEnumerable<SelectListItem>> ObtenerEstados()
        {
            var estados = await repositorioEstadoReserva.Obtener();
            return estados.Select(x => new SelectListItem(x.Descripcion, x.Id.ToString()));
        }

        public async Task<IActionResult> CancelarReserva(int id)
        {
            await repositorioReserva.Cancelar(id);

            return RedirectToAction("Index");

        }
    }
}

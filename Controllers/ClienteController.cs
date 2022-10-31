using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Turismo.Servicios;

namespace Turismo.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IRepositorioCiudad repositorioCiudad;
        private readonly IRepositorioEstadoDepartamento repositorioEstadoDepartamento;
        private readonly IRepositorioDepartamento repositorioDepartamento;
        private readonly IRepositorioReserva repositorioReserva;

        public ClienteController(IRepositorioCiudad repositorioCiudad,
            IRepositorioEstadoDepartamento repositorioEstadoDepartamento,
            IRepositorioDepartamento repositorioDepartamento ,IRepositorioReserva
            repositorioReserva)
        {
            this.repositorioCiudad = repositorioCiudad;
            this.repositorioEstadoDepartamento = repositorioEstadoDepartamento;
            this.repositorioDepartamento = repositorioDepartamento;
            this.repositorioReserva = repositorioReserva;
        }

        public  IActionResult Index()
        {
            return View();
        }

        

    }
}

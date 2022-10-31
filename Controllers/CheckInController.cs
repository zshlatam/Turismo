using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Turismo.Models;
using Turismo.Servicios;

namespace Turismo.Controllers
{
    public class CheckInController: Controller
    {
        private readonly IRepositorioCheckIn repositorioCheckIn;
        private readonly IRepositorioReserva repositorioReserva;
        private readonly IMapper mapper;

        public CheckInController(IRepositorioCheckIn repositorioCheckIn,IRepositorioReserva repositorioReserva,
            IMapper mapper)
        {
            this.repositorioCheckIn = repositorioCheckIn;
            this.repositorioReserva = repositorioReserva;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            //var reservas = await repositorioReserva.ObtenerIdReserva();
            var modelo = new CheckInCreacionViewModel();
            
            return View(modelo);

        }
        [HttpPost]
        public async Task<IActionResult> Crear(CheckInCreacionViewModel modelo)
        {

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            
            await repositorioCheckIn.Crear(modelo);
            ViewBag.model = modelo;
            
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> index(CheckIn checkIn)
        {
            var check= await repositorioCheckIn.ObtenerPorId(checkIn.Id);

            var modelo = new CheckInCreacionViewModel()
            {
                Id = check.Id,
                Descripcion = check.Descripcion,
                Fecha = check.Fecha,

                ReservaId = check.ReservaId,
                FechaReserva = check.FechaReserva,
                FechaPedidoReserva = check.FechaPedidoReserva,
                Email = check.Email,
                departamento = check.departamento,
                Direccion = check.Direccion
            };
            return View(modelo);

            

            
        }

        //[HttpPost]
        //public async IActionResult CrearCheckIn(int id)
        //{
        //    var fechaActual = DateTime.Today;

        //    await repositorioCheckIn.Crear(id, fechaActual);
        //    return View();
        //}
    }
}

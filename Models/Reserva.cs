using System.ComponentModel.DataAnnotations;

namespace Turismo.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        [Display(Name = "Fecha a reservar")]
        [DataType(DataType.Date)]
        public DateTime FechaReserva { get; set; } = DateTime.Today;
        [Display(Name = "Fecha de de termino de la reserva")]
        [DataType(DataType.Date)]
        public DateTime FechaTerminoReserva { get; set; } = DateTime.Today;
        [DataType(DataType.Date)]
        [Display(Name = "Fecha actual")]
        public DateTime FechaPedidoReserva { get; set; } = DateTime.Today;
        [Display(Name = "Departamento a reservar")]
        public int DepartamentoId { get; set; }
        [Display(Name = "Estado de reserva")] 
        public int EstadoReservaId { get; set; } = 1;
        public int UsuarioId { get; set; }
        public string Departamento { get; set; }
        public string EstadoReserva { get; set; }
    }
}

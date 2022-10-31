using System.ComponentModel.DataAnnotations;

namespace Turismo.Models
{
    public class CheckIn
    {
        public int Id { get; set; }
        public string Descripcion  { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Today;
        [Display(Name = "Número de reserva")]
        public int ReservaId { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaPedidoReserva{ get; set; }
        public string Email { get; set; }
        public string departamento { get; set; }
        public string Direccion { get; set; }


    }
}

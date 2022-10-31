using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Turismo.Validaciones;

namespace Turismo.Models
{
    public class EstadoReserva
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es requerido")]
        //[StringLength(maximumLength:30, MinimumLength =3, ErrorMessage ="El campo debe ser mayor a 3 y menor a 30 letras")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VerificarExisteEstadoReserva", controller: "EstadoReserva")]
        public string Descripcion { get; set; }
    }
}
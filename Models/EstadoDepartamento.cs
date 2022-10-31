using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Turismo.Validaciones;

namespace Turismo.Models
{
    public class EstadoDepartamento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es requerido")]
        //[StringLength(maximumLength:30, MinimumLength =3, ErrorMessage ="El campo debe ser mayor a 3 y menor a 30 letras")]
        [Display(Name = "Nombre del estado")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VerificarExisteEstadoDepartamento", controller: "EstadoDepartamento")]
        public string Descripcion { get; set; }
    }
}

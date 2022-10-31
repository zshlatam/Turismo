using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Turismo.Validaciones;

namespace Turismo.Models
{
    public class Ciudad
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El Nombre es requerido")]
        //[StringLength(maximumLength:30, MinimumLength =3, ErrorMessage ="El campo debe ser mayor a 3 y menor a 30 letras")]
        [Display(Name ="Nombre de la Ciudad")]
        [PrimeraLetraMayuscula]
        [Remote(action:"VerificarExisteCiudad",controller:"Ciudad")]
        public string Descripcion { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Turismo.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "eL campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "eL campo {0} debe ser un correo electronico valido")]
        public string Email { get; set; }
        [Required(ErrorMessage ="eL campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}

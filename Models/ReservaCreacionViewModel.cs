using Microsoft.AspNetCore.Mvc.Rendering;

namespace Turismo.Models
{
    public class ReservaCreacionViewModel: Reserva
    {
        public IEnumerable<SelectListItem> Departamentos { get; set; }
        //public IEnumerable<SelectListItem> Usuarios { get; set; }
        public IEnumerable<SelectListItem> Estados { get; set; }


    }
}

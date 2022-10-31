using Microsoft.AspNetCore.Mvc.Rendering;

namespace Turismo.Models
{
    public class MantenimientoCreacionViewModel:Mantenimiento
    {
        public IEnumerable<SelectListItem> TiposDepartamentos { get; set; }

    }
}

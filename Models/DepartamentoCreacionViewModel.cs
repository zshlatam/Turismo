using Microsoft.AspNetCore.Mvc.Rendering;

namespace Turismo.Models
{
    public class DepartamentoCreacionViewModel: Departamento
    {
        public IEnumerable<SelectListItem> TiposCiudades { get; set; }
        public IEnumerable<SelectListItem> TiposEstadoDepartamento { get; set; }
    }
}

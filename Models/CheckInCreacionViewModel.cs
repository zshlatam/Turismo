using Microsoft.AspNetCore.Mvc.Rendering;

namespace Turismo.Models
{
    public class CheckInCreacionViewModel: CheckIn
    {
        public IEnumerable<SelectListItem> Reservas { set; get; }
    }
}

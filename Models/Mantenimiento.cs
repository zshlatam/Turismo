using System.ComponentModel.DataAnnotations;

namespace Turismo.Models
{
    public class Mantenimiento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        [Display(Name ="Fecha de mantenimiento")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Today;
        [Display(Name = "Nombre departamento de la mantención")]
        public int DepartamentoId  { get; set; }
        public string Departamento { get; set; }
    }
}

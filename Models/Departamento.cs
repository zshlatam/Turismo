using System.ComponentModel.DataAnnotations;

namespace Turismo.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        [Display(Name = "Número del departamento")]
        public int NroDepartamento { get; set; }
        [Display(Name = "Número del piso")]
        public int NroPiso { get; set; }
        [Display(Name = "Cantidad de habitaciones")]
        public int NroHabitaciones { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Baño propio")]
        public bool Bano { get; set; }
        public bool Cocina { get; set; }
        public bool Piscina { get; set; }
        public bool Patio { get; set; }
        public bool Estacionamiento { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Tv { get; set; }
        public bool Parrilla { get; set; }
        [Display(Name = "Area fumadores")]
        public bool Fumadores { get; set; }
        public bool Wifi { get; set; }
        public bool Lavadora { get; set; }
        [Display(Name = "Camara de seguridad")]
        public bool CamaraSeguridad { get; set; }
        [Display(Name = "Aire Acondicionado")]
        public bool AireAcondicionado { get; set; }
        [Display(Name ="Ciudad")]
        public int CiudadId { get; set; }
        [Display(Name = "Estado del departamento")]
        public int EstadoDepartamentoId { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
    }
}

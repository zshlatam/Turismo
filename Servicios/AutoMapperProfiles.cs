using AutoMapper;
using Turismo.Models;

namespace Turismo.Servicios
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Departamento, DepartamentoCreacionViewModel>();
        }
        

    }
}

using AutoMapper;
using Test.Shared.Dtos;
using Test.Shared.Entities;

namespace Test.ServiceDependencies.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeo Nuevo Producto
            CreateMap<NuevoProducto, Producto>();
            //Mapeo Actualizar producto
            CreateMap<ActualizarProducto, Producto>();
        }
    }
}

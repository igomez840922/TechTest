using AutoMapper;
using Test.Shared.Entities;

namespace Test.Server.AuxService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Producto, Producto>()
                .ForMember(x => x.Modelo, option => option.Ignore());

            CreateMap<Producto, Producto>()
                .ForMember(x => x.Foto, option => option.Ignore());
        }
    }
}

using AutoMapper;
using Challenge.Application.DTO;
using Challenge.Domain.Entity;
using System;

namespace Challenge.Transversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Source, SourceDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
        }
    }
}

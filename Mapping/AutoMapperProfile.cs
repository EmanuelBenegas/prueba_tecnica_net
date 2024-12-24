using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Prueba_Tecnica_Net.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            //Include reverse map due to inserts from api service
            CreateMap<Retailer, RetailerDto>()
                .ReverseMap();      
        
        }
    }
}

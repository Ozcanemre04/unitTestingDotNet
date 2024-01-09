using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProductApp.Api.data;
using ProductApp.Api.dto;
using ProductApp.Api.models;


namespace ProductApp.Api
{
    public class AutoMapperProfile : Profile
    {
    
        public AutoMapperProfile()
        {
            
            CreateMap<AddProductDto, Product>();
            
            CreateMap<UpdateProductDto, Product>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) => srcMember != null));
            CreateMap<Product, ProductDto>();
            
        }
    }
}



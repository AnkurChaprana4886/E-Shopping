using AutoMapper;
using E_Shopping_BAL.Dto;
using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Mapper
{
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}

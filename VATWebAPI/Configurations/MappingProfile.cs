using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VATViewModel.DTOs;
using VATWebAPI.Models;
using WebAPI.Models;

namespace VATWebAPI.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, ProductsModel>().ReverseMap();
            CreateMap<ProductsModel, ProductVM>().ReverseMap();
            CreateMap<CustomerDto, CustomersModel>().ReverseMap();
            CreateMap<CustomersModel, CustomerVM>().ReverseMap();
            CreateMap<VendorDto, VendorsModel>().ReverseMap();
            CreateMap<VendorsModel, VendorVM>().ReverseMap();
        }
    }
}
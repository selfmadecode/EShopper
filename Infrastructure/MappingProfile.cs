using AutoMapper;
using E_Shopper.Models.Entities;
using E_Shopper.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ApprovedProductToSale>();
        }
    }
}

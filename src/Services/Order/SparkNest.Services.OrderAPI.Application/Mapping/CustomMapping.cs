using AutoMapper;
using SparkNest.Services.OrderAPI.Application.DTOs;
using SparkNest.Services.OrderAPI.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Application.Mapping
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}

﻿using SparkNest.Services.OrderAPI.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Application.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Address Address { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string BuyerId { get; set; }
        public int Status { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}

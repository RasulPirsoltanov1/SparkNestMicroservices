﻿using SparkNest.Common.Base.Services;
using SparkNest.Common.DTOs;
using SparkNest.UI.MVC.Models.FakePayment;
using SparkNest.UI.MVC.Models.Orders;
using SparkNest.UI.MVC.Models.Orders.StatusChange;
using SparkNest.UI.MVC.Services.Interfaces;
using System.Diagnostics;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class OrderService : IOrderService
    {
        HttpClient _httpClient;
        IPaymentService _paymentService;
        IBasketService _basketService;
        ISharedIdentityService _sharedIdentityService;

        public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderStatusVM> CreateOrder(CheckoutInfoVM checkoutInfoVM)
        {
            var basket = await _basketService.Get();
            PaymentInfoVM paymentInfoVM = new PaymentInfoVM()
            {
                CardName = checkoutInfoVM.CardName,
                CardNumber = checkoutInfoVM.CardNumber,
                CVV = checkoutInfoVM.CVV,
                Expiration = checkoutInfoVM.Expiration,
                TotalPrice = basket.TotalPrice
            };

            var paymentResponse = await _paymentService.ReceivePayment(paymentInfoVM);

            if (!paymentResponse)
            {
                return new OrderStatusVM
                {
                    Error = "something went wrong!",
                    IsSuccessfull = false
                };
            }
            OrderCreateVM orderCreateVM = new OrderCreateVM()
            {
                BuyerId = _sharedIdentityService.UserId,
                UserName = checkoutInfoVM.UserName,
                Email = checkoutInfoVM.Email,
                Address = new AddressCreateVM
                {
                    District = checkoutInfoVM.District,
                    Line = checkoutInfoVM.Line,
                    Province = checkoutInfoVM.Province,
                    Street = checkoutInfoVM.Street,
                    ZipCode = checkoutInfoVM.ZipCode,
                },
            };
            foreach (var item in basket.BasketItems)
            {
                orderCreateVM.OrderItems.Add(new OrderItemCreateVM
                {
                    Price = item.GetCurrentPrice,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductUrl = ""
                });
            }

            var response = await _httpClient.PostAsJsonAsync<OrderCreateVM>("orders", orderCreateVM);
            await _basketService.Delete();
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<OrderStatusVM>>();
                return result.Data;
            }
            return new OrderStatusVM
            {
                Error = "something went wrong!",
                IsSuccessfull = false
            };
        }

        public async Task<List<OrderVM>> GetAllOrders()
        {
            var response = await _httpClient.GetAsync("orders");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await response.Content.ReadFromJsonAsync<Response<List<OrderVM>>>();
            return result.Data;
        }
        public async Task<List<OrderVM>> GetAll()
        {
            var response = await _httpClient.GetAsync("orders/GetAll");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await response.Content.ReadFromJsonAsync<Response<List<OrderVM>>>();
            return result.Data;
        }

        public async Task<bool> StatusChange(StatusChangeVM statusChangeVM)
        {
            var response = await _httpClient.PostAsJsonAsync("orders/StatusChange", statusChangeVM);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
            return true;
        }

        public async Task<OrderSuspendStatusVM> SuspendOrder(CheckoutInfoVM checkoutInfoVM)
        {
            var basket = await _basketService.Get();
            OrderCreateVM orderCreateVM = new OrderCreateVM()
            {
                BuyerId = _sharedIdentityService.UserId,
                UserName=checkoutInfoVM.UserName,
                Email=checkoutInfoVM.Email,
                Address = new AddressCreateVM
                {
                    District = checkoutInfoVM.District,
                    Line = checkoutInfoVM.Line,
                    Province = checkoutInfoVM.Province,
                    Street = checkoutInfoVM.Street,
                    ZipCode = checkoutInfoVM.ZipCode,
                },
            };
            foreach (var item in basket.BasketItems)
            {
                orderCreateVM.OrderItems.Add(new OrderItemCreateVM
                {
                    Price = item.GetCurrentPrice,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductUrl = "",
                    Quantity = item.Quantity
                });
            }
            PaymentInfoVM paymentInfoVM = new PaymentInfoVM()
            {
                CardName = checkoutInfoVM.CardName,
                CardNumber = checkoutInfoVM.CardNumber,
                CVV = checkoutInfoVM.CVV,
                UserName = checkoutInfoVM.UserName,
                Email=checkoutInfoVM.Email,
                Expiration = checkoutInfoVM.Expiration,
                TotalPrice = basket.TotalPrice,
                Order = orderCreateVM
            };
            var paymentResponse = await _paymentService.ReceivePayment(paymentInfoVM);

            if (!paymentResponse)
            {
                return new OrderSuspendStatusVM
                {
                    Error = "something went wrong!",
                    IsSuccessfull = false
                };
            }
            await _basketService.Delete();
            return new OrderSuspendStatusVM();
        }
    }
}

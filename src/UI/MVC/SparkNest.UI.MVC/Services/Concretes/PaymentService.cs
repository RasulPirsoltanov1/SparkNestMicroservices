using SparkNest.UI.MVC.Models.FakePayment;
using SparkNest.UI.MVC.Services.Interfaces;

namespace SparkNest.UI.MVC.Services.Concretes
{
    public class PaymentService : IPaymentService
    {
        HttpClient _httpClient;

        public PaymentService(HttpClient httpClien)
        {
            _httpClient = httpClien;
        }

        public async Task<bool> ReceivePayment(PaymentInfoVM fakePaymentVM)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentInfoVM>("FakePayments", fakePaymentVM);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}

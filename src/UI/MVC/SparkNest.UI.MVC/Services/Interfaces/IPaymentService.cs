using SparkNest.UI.MVC.Models.FakePayment;

namespace SparkNest.UI.MVC.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoVM fakePaymentVM);
    }
}

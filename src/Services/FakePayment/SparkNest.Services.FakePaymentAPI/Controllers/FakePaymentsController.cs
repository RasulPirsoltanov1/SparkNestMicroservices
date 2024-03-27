using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.ControllerBases;
using SparkNest.Common.DTOs;
using SparkNest.Services.FakePaymentAPI.Models;

namespace SparkNest.Services.FakePaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomControllerBase
    {
        [HttpPost]
        public IActionResult ReciewePayment(FakePaymentDto fakePaymentDto) 
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}

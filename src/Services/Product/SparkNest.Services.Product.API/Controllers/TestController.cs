using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SparkNest.Services.ProductAPI.Settings;

namespace SparkNest.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        IDatabaseSettings _databaseSettings;

        public TestController(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_databaseSettings);
        }
    }
}

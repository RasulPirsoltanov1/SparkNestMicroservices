using Microsoft.AspNetCore.Mvc;
using SparkNest.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparkNest.Common.ControllerBases
{
    public class CustomControllerBase:ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}

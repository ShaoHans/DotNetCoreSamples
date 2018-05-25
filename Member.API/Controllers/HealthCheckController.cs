using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Member.API.Controllers
{
    [Route("HealthCheck")]
    public class HealthCheckController : Controller
    {
        [Route("")]
        [HttpGet("")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace whatsmyprogress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckConnectionController : ControllerBase
    {
        [HttpGet]
        public string Get() {
            return "We good";
        }
    }
}

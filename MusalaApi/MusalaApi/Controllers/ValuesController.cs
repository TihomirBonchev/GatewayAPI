using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusalaApi.Model;

namespace MusalaApi.Controllers
{
    [Route("api/values")]

    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok();
        }
        [HttpGet("{Id:int}")]
        public IActionResult Test([FromRoute] int id)
        {
            return Ok();
        }
        [HttpPost]
       
        public IActionResult CreateGateway([FromBody] Gateway gateway)
        {
            return Ok();
        }
    }
}

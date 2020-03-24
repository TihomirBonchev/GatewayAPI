using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusalaApi.Model;

namespace MusalaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewaysController : ControllerBase
    {
        private readonly ApiContext _context;

        public GatewaysController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Gateways
        [HttpGet]
        public IEnumerable<Gateway> GetGateway()
        {
            return _context.Gateway;
        }

        // GET: api/Gateways/5
        [HttpGet("{id}",Name = "GetGateway")]
        public async Task<IActionResult> GetGateway([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gateway = await _context.Gateway.FindAsync(id);

            if (gateway == null)
            {
                return NotFound($"Gateway {id} was not found");
            }

            return Ok(gateway);
        }

        // PUT: api/Gateways/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGateway([FromRoute] string id, [FromBody] Gateway gateway)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gateway.GatewayId)
            {
                return BadRequest();
            }

            _context.Entry(gateway).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GatewayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Gateways
        [HttpPost]
        public async Task<IActionResult> PostGateway([FromBody] Gateway gateway)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Gateway.Add(gateway);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGateway", new { id = gateway.GatewayId }, gateway);
        }

        // DELETE: api/Gateways/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGateway([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gateway = await _context.Gateway.FindAsync(id);
            if (gateway == null)
            {
                return NotFound();
            }

            _context.Gateway.Remove(gateway);
            await _context.SaveChangesAsync();

            return Ok(gateway);
        }

        private bool GatewayExists(string id)
        {
            return _context.Gateway.Any(e => e.GatewayId == id);
        }
    }
}
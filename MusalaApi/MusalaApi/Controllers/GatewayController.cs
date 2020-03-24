using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusalaApi.Model;
using Microsoft.EntityFrameworkCore;
using MusalaApi.Repository;
using Microsoft.Extensions.Logging;

namespace MusalaApi.Controllers
{
    [Route("api/Gateways")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private IGatewayRepository _context;
        private ILogger<GatewayController> _logger;

        public GatewayController(IGatewayRepository gatewayRepository, ILogger<GatewayController> logger)
        {
            _context = gatewayRepository;
            _logger = logger;
        }
       
        // GET: api/gateways/1111/device/5
        [HttpGet("{SerialNumber}/Device/{DeviceId}")]
        public async Task<IActionResult> GetDevice([FromRoute] string SerialNumber,[FromRoute] int DeviceId)
        {
            if (!await _context.GatewayExists(SerialNumber))
            {
                _logger.LogError($"Gateway with serial number {SerialNumber} wasn't found!");
                return NotFound($"Gateway with serial number {SerialNumber} wasn't found!");
            }
            try
            {
               
                var device = await _context.GetDevice(DeviceId);
                if (device==null)
                {
                    _logger.LogError($"Device with serial number {SerialNumber} id {DeviceId} wasn't found!");
                    return NotFound($"Device with id {DeviceId} wasn't found!");
                }

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "A problem happened while handling you request.");
            }
        }
       
        // GET: api/gateways
        [HttpGet()]
        public async Task<IActionResult> GetGateways()
        {
            try
            {
                var gateways = await _context.GetGateways();

                return Ok(gateways);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "A problem happened while handling you request.");
            }
        }


        // GET: api/gateways/1111?includeDevices=true
        [HttpGet("{SerialNumber}")]
        public async Task<IActionResult> GetGateway([FromRoute] string SerialNumber, bool includeDevices)
        {
            if (!await _context.GatewayExists(SerialNumber))
            {
                _logger.LogError($"Gateway with serial number {SerialNumber} wasn't found!");
                return NotFound($"Gateway with serial number {SerialNumber} wasn't found!");
            }
            try
            {
                var gateway = await _context.GetGateway( SerialNumber,  includeDevices);
                return Ok(gateway);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "A problem happened while handling you request.");
            }

        }

        // POST: api/gateways/111/device
        [HttpPost("{SerialNumber}/Device")]
        public async Task<IActionResult> CreateDevice([FromRoute] string SerialNumber, [FromBody] Device device)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(! await _context.GatewayExists(SerialNumber))
            {
                _logger.LogError($"Gateway with serial number {SerialNumber} wasn't found!");
                return NotFound($"Gateway with serial number {SerialNumber} wasn't found!");
            }
           
            try
            {
                await _context.CreateDevice(SerialNumber, device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "A problem happened while handling you request.");
            }

            return CreatedAtAction("GetDevice", new { DeviceId = device.DeviceId }, device);
        }
       
        // POST: api/gateways/
        [HttpPost]
        public async Task<IActionResult> CreateGateway([FromBody] Gateway gateway)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            try
            {
                await _context.CreateGateway(gateway);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "A problem happened while handling you request.");
            }


            return CreatedAtAction("GetGateway", new { SerialNumber = gateway.SerialNumber }, gateway);
        }
        
        // DELETE: api/gateways/5
        [HttpDelete("{SerialNumber}")]
        public async Task<IActionResult> DeleteGateway([FromRoute] string SerialNumber)
        {
           
            if (!await _context.GatewayExists(SerialNumber))
            {
                _logger.LogError($"Gateway with serial number {SerialNumber} wasn't found!");
                return NotFound($"Gateway with serial number {SerialNumber} wasn't found!");
            }
            try
            {
                await _context.DeleteGateway(SerialNumber);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "A problem happened while handling you request.");
            }
}
        // DELETE: api/gateway/111/divice/5
        [HttpDelete("{SerialNumber}/Device/{DeviceId}")]
        public async Task<IActionResult> DeleteDevice([FromRoute] string SerialNumber, [FromRoute] int DeviceId)
        {
            if (!await _context.GatewayExists(SerialNumber))
            {
                _logger.LogError($"Gateway with serial number {SerialNumber} wasn't found!");
                return NotFound($"Gateway with serial number {SerialNumber} wasn't found!");
            }
            if (!await _context.DeviceExists(DeviceId))
            {
                _logger.LogError($"Device with id {DeviceId} wasn't found!");
                return NotFound($"Device with id {DeviceId} wasn't found!");
            }
            try
            {
                await _context.DeleteDevice(DeviceId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "A problem happened while handling you request.");
            }
        }
    }
}
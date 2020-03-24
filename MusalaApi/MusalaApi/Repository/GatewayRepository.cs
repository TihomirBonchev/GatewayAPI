using MusalaApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MusalaApi.Repository
{
    public class GatewayRepository : IGatewayRepository
    {
        private ApiContext _context;

        public GatewayRepository(ApiContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Gateway>> GetGateways()
        {
            return await _context.Gateway.Include(c => c.Devices).ToListAsync();
        }

        public async Task<Gateway> GetGateway(string SerialNumber, bool includeDevices)
        {
            if (includeDevices)
                return await _context.Gateway.Where(c => c.SerialNumber == SerialNumber).FirstOrDefaultAsync<Gateway>();
            else
                return await _context.Gateway.Where(c => c.SerialNumber == SerialNumber).Include(c => c.Devices).FirstOrDefaultAsync<Gateway>();
        }

        public async Task<Device> GetDevice(int DeviceId)
        {
            return await _context.Device.Where(c => c.DeviceId == DeviceId).FirstOrDefaultAsync();
        }

        public async Task<int> CreateDevice(string SerialNumber, Device device)
        {

            //var gateway = await _context.Gateway.Where(c => c.SerialNumber == SerialNumber).FirstOrDefaultAsync();
            var gateway = await GetGateway(SerialNumber, false);
            gateway.Devices.Add(device);
            return await _context.SaveChangesAsync();

        }
        public async Task<int> CreateGateway(Gateway gateway)
        {
            await _context.Gateway.AddAsync(gateway);
            return await _context.SaveChangesAsync();

        }

        public async Task<bool> GatewayExists(string SerialNumber)
        {
            return await _context.Gateway.AnyAsync(c => c.SerialNumber == SerialNumber);
        }
        public async Task<bool> DeviceExists(int DeviceId)
        {
            return await _context.Device.AnyAsync(c => c.DeviceId == DeviceId);
        }

        public async Task<int> DeleteGateway(string SerialNumber)
        {
            var gateway = await _context.Gateway.Include(c => c.Devices).FirstOrDefaultAsync(c => c.SerialNumber == SerialNumber);


            _context.Gateway.Remove(gateway);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteDevice(int DeviceId)
        {

            try
            {
                var device = _context.Device.FirstOrDefault(c => c.DeviceId == DeviceId);
                _context.Device.Remove(device);
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                ////This either returns a error string, or null if it can’t handle that error
                //var error = CheckHandleError(e);
                //if (error != null)
                //{
                //    return error; //return the error string
                //}
                throw; //couldn’t handle that error, so rethrow
            }

        }

    }
}

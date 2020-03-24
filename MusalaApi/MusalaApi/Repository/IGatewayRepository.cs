using MusalaApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusalaApi.Repository
{
    public interface IGatewayRepository
    {
        Task<IEnumerable<Gateway>> GetGateways();
        Task<Gateway> GetGateway(string SerialNumber,bool includeDevices);
       
        Task<Device> GetDevice(int DeviceId);
        Task<bool> GatewayExists(string SerialNumber);

        Task<int> CreateDevice(string SerialNumber, Device device);

        Task<int> CreateGateway(Gateway gateway);
       
        Task<bool> DeviceExists(int DeviceId);
        Task<int> DeleteGateway(string SerialNumber);

        Task<int> DeleteDevice(int DeviceId);
    }
}

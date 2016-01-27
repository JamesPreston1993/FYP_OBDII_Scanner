using System.Collections.Generic;
using System.Threading.Tasks;
using VSDACore.Connection;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Connection
{
    public interface IConnectionModule : IModule
    {
        IList<IDevice> Devices { get; }
        
        string DeviceConnectionStatus { get; }

        Task<bool> Connect(IDevice device);
    }
}

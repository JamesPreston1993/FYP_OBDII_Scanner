using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace VSDA.Communication
{
    public interface IConnectionModule : IModule
    {
        DeviceInformationCollection Devices { get; }

        Task<bool> Connect(DeviceInformation device);
    }
}

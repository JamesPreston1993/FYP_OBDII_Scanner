using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace VSDA.Connection
{
    public interface IDataConnection : INotifyPropertyChanged
    {
        bool IsInitialized { get; }

        DeviceInformation Device{ get; set; }

        Task<bool> Initialize();

        Task<bool> Reset();

        Task<string> SendCommand(string command);
    }
}

using System.Collections.Generic;
using System.Windows.Input;
using VSDACore.Connection;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Connection
{
    public interface IConnectionModuleViewModel : IModuleViewModel
    {
        ICommand ConnectCommand { get; }

        IDevice CurrentDevice { get; set; }

        string DeviceConnectionStatus { get; }

        IList<IDevice> Devices { get; set; }
    }
}

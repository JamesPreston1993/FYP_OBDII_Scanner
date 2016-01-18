using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace VSDACore.Connection
{
    public interface IDataConnection : INotifyPropertyChanged
    {
        bool IsInitialized { get; }

        IDevice CurrentDevice { get; set; }

        Task<IList<IDevice>> GetAvailableDevices();

        Task<bool> Initialize();

        Task<bool> Reset();

        Task<string> SendCommand(string command);
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace VSDACore.Connection
{
    public interface IDataConnection : INotifyPropertyChanged
    {
        bool IsInitialized { get; }

        string CommunicationLog { get; }

        ConnectionStatus DeviceConnectionStatus { get; set; }

        Protocol VehicleProtocol { get; }

        IDevice CurrentDevice { get; set; }

        Task<IList<IDevice>> GetAvailableDevices();

        Task<bool> Initialize();

        Task<bool> AwaitShutdown();

        Task<bool> Reset();

        Task<string> SendCommand(string command);
    }

    public enum ConnectionStatus
    {
        NotConnected,
        Connecting,
        Connected
    }

    public enum Protocol
    {
        PWM,
        VPW,
        ISO9141,
        KWP,
        CAN,
        Unknown
    }
}

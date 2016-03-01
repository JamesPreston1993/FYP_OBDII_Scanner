using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Networking.Sockets;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Storage.Streams;
using System.ComponentModel;
using VSDACore.Connection;
using System.Threading;
using System.Diagnostics;

namespace VSDA.Connection
{
    public class BluetoothDataConnection : IDataConnection
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string NO_CONNECTION = "NO_CONNECTION";

        private RfcommDeviceService service;        
        private StreamSocket socket;
        private DataWriter writer;
        private DataReader reader;

        private SemaphoreSlim asyncLock;

        private bool isInitialized;
        public bool IsInitialized
        {
            get
            {
                return this.isInitialized;
            }
            private set
            {
                this.isInitialized = value;
                if(this.isInitialized)
                {
                    this.DeviceConnectionStatus = ConnectionStatus.Connected;                    
                }
                else
                {
                    this.DeviceConnectionStatus = ConnectionStatus.NotConnected;
                    this.CurrentDevice = null;
                    this.service.Dispose();
                    this.service = null;
                    this.writer.Dispose();
                    this.writer = null;
                    this.reader.Dispose();
                    this.reader = null;
                    this.socket.Dispose();
                    this.socket = null;
                }
                this.RaisePropertyChanged("IsInitialized");
            }
        }

        private string communicationLog;
        public string CommunicationLog
        {
            get
            {
                return this.communicationLog;
            }
            private set
            {
                this.communicationLog = value;
                this.RaisePropertyChanged("CommunicationLog");
            }

        }

        private ConnectionStatus connectionStatus;
        public ConnectionStatus DeviceConnectionStatus
        {
            get
            {
                return this.connectionStatus;
            }
            set
            {
                this.connectionStatus = value;
                this.RaisePropertyChanged("DeviceConnectionStatus");
            }
        }

        public Protocol VehicleProtocol { get; private set; }

        public IDevice CurrentDevice { get; set; }
        
        public BluetoothDataConnection()
        {
            this.CurrentDevice = null;
            this.DeviceConnectionStatus = ConnectionStatus.NotConnected;
            this.isInitialized = false;
            this.asyncLock = new SemaphoreSlim(1);
        }        

        public async Task<IList<IDevice>> GetAvailableDevices()
        {
            IList<IDevice> availableDevices = new List<IDevice>();
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));
            foreach(DeviceInformation device in devices)
            {
                IDevice bluetoothDevice = new BluetoothConnectionDevice(device.Name, device.Id, null);
                availableDevices.Add(bluetoothDevice);
            }
            return availableDevices;
        }

        public async Task<bool> Initialize()
        {
            if (!this.IsInitialized)
            {
               if (this.CurrentDevice != null)
                {
                    await this.asyncLock.WaitAsync();
                    this.service = await RfcommDeviceService.FromIdAsync(this.CurrentDevice.Id);                    

                    if (this.service != null)
                    {
                        this.DeviceConnectionStatus = ConnectionStatus.Connecting;

                        this.socket = new StreamSocket();

                        CancellationTokenSource token = new CancellationTokenSource();
                        try
                        {
                            token.CancelAfter(5000);
                            await socket.ConnectAsync(this.service.ConnectionHostName, this.service.ConnectionServiceName, SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication).AsTask(token.Token);
                            this.asyncLock.Release();

                            this.writer = new DataWriter(this.socket.OutputStream);
                            this.reader = new DataReader(this.socket.InputStream);
                            this.reader.InputStreamOptions = InputStreamOptions.Partial;

                            this.isInitialized = true;

                            bool resetComplete = await this.Reset();

                            if(resetComplete)
                                this.IsInitialized = true;

                            return true;
                        }
                        catch(TaskCanceledException e)
                        {
                            this.DeviceConnectionStatus = ConnectionStatus.NotConnected;                            
                        }
                    }
                    else
                    {
                        this.DeviceConnectionStatus = ConnectionStatus.NotConnected;
                    }
                    this.asyncLock.Release();
                }
            }
            return false;            
        }

        public async Task<bool> AwaitShutdown()
        {
            if (this.IsInitialized)
            {
                await this.asyncLock.WaitAsync();
                this.asyncLock.Release();
            }
            return true;
        }

        public async Task<bool> Reset()
        {
            DateTime startTime = DateTime.Now;
            string[] commands = { "ATZ", "ATAL", "ATE0", "ATL0", "ATS0", "ATSP0", "01001" };

            for(int i= 0; i < commands.Length; i++)
            {
                string response = await this.SendCommand(commands[i]);
                if(response.Equals(NO_CONNECTION))
                {
                    return false;
                }
            }
            
            this.VehicleProtocol = await this.GetProtocol();
            DateTime endTime = DateTime.Now;
            double timeTaken = (endTime - startTime).TotalMilliseconds;
            return true;
        }

        public async Task<string> SendCommand(string command)
        {                        
            await this.asyncLock.WaitAsync();
            string response = string.Empty;

            if (this.IsInitialized)
            {                
                DateTime startTime = DateTime.Now;

                // Write
                if (this.writer != null)
                {
                    this.writer.WriteString(command + "\r");
                    await this.writer.StoreAsync();
                }
                
                CancellationTokenSource token = new CancellationTokenSource();
                token.CancelAfter(5000);

                // Read
                try
                {
                    while (!response.Contains(">"))
                    {
                        uint size = await this.reader.LoadAsync(1).AsTask(token.Token);                        
                        string s = this.reader.ReadString(size);
                        response += s;
                    }
                    response = response.Replace(">", "");
                }
                catch (Exception e)
                {
                    response = NO_CONNECTION;
                    this.IsInitialized = false;
                }                

                if (response.Contains("UNABLE TO CONNECT"))
                {
                    response = NO_CONNECTION;
                    this.IsInitialized = false;
                }
                
                DateTime endTime = DateTime.Now;
                double timeTaken = Math.Round((endTime - startTime).TotalMilliseconds, 0);
                string log = string.Format("SENT: {0} RECEIVED: {1} TIME: {2}ms", command, response.Replace("\r", ""), timeTaken);
                Debug.WriteLine(log);
                this.CommunicationLog = log;
            }
            else
            {
                response = NO_CONNECTION;
            }

            this.asyncLock.Release();

            return response;
        }

        private async Task<Protocol> GetProtocol()
        {
            Protocol protocol;

            string response = await this.SendCommand("ATDP");

            if (response.Contains("9141-2"))
                protocol = Protocol.ISO9141;            
            else if (response.Contains("KWP"))
                protocol = Protocol.KWP;
            else if (response.Contains("CAN"))
                protocol = Protocol.CAN;
            else if (response.Contains("PWM"))
                protocol = Protocol.PWM;
            else if (response.Contains("VPW"))
                protocol = Protocol.VPW;
            else
                protocol = Protocol.Unknown;

            return protocol;
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

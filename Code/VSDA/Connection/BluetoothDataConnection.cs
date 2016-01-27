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
    public class BluetoothDataConnection : VSDACore.Connection.IDataConnection
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
                switch(this.isInitialized)
                {
                    case true: this.DeviceConnectionStatus = ConnectionStatus.Connected; break;
                    case false: this.DeviceConnectionStatus = ConnectionStatus.NotConnected; break;
                }
                this.RaisePropertyChanged("IsInitialized");
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

                            await this.Reset();

                            this.IsInitialized = true;

                            return true;
                        }
                        catch(TaskCanceledException e)
                        {
                            this.DeviceConnectionStatus = ConnectionStatus.NotConnected;
                            this.asyncLock.Release();
                            return false;
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
            await this.SendCommand("ATZ");
            await this.SendCommand("ATAL");
            await this.SendCommand("ATE0");
            await this.SendCommand("ATL0");
            await this.SendCommand("ATS0");
            await this.SendCommand("ATSP0");
            await this.SendCommand("01001");
            DateTime endTime = DateTime.Now;
            double timeTaken = (endTime - startTime).TotalMilliseconds;
            return true;
        }

        

        public async Task<string> SendCommand(string command)
        {            
            string response = string.Empty;
            
            if (this.IsInitialized)
            {
                await this.asyncLock.WaitAsync();
                DateTime startTime = DateTime.Now;

                // Write
                this.writer.WriteString(command + "\r");
                await this.writer.StoreAsync();

                CancellationTokenSource token = new CancellationTokenSource();
                token.CancelAfter(5000);

                
                // Read
                while (true)
                {
                    uint size = await this.reader.LoadAsync(1).AsTask(token.Token);
                    string s = this.reader.ReadString(size);
                    if (s.Equals(">"))
                        break;
                    else
                        response += s;
                }

                /*
                try
                {
                    while (!response.Contains(">"))
                    {
                        uint size = await this.reader.LoadAsync(32).AsTask(token.Token);
                        string s = this.reader.ReadString(size);
                        response += s;
                    }
                }
                catch(TaskCanceledException te)
                {

                }
                */
                DateTime endTime = DateTime.Now;
                string log = command + ": " + (endTime - startTime).TotalMilliseconds + "ms";
                Debug.WriteLine(log);
                this.asyncLock.Release();
            }
            else
            {
                response = "No connection!";
            }
            return response;
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

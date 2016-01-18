using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Networking.Sockets;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Storage.Streams;
using Windows.Devices.Bluetooth.Background;
using System.ComponentModel;
using VSDACore.Connection;

namespace VSDA.Connection
{
    public class BluetoothDataConnection : VSDACore.Connection.IDataConnection
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private RfcommDeviceService service;        
        private StreamSocket socket;
        private DataWriter writer;
        private DataReader reader;

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
                this.RaisePropertyChanged("IsInitialized");
            }
        }
        public IDevice CurrentDevice { get; set; }
        
        public BluetoothDataConnection()
        {
            this.CurrentDevice = null;
            this.isInitialized = false;
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
                    this.service = await RfcommDeviceService.FromIdAsync(this.CurrentDevice.Id);
                    
                    if (this.service != null)
                    {
                        this.socket = new StreamSocket();
                        await socket.ConnectAsync(this.service.ConnectionHostName, this.service.ConnectionServiceName, SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication);
                        
                        this.writer = new DataWriter(this.socket.OutputStream);
                        this.reader = new DataReader(this.socket.InputStream);

                        this.isInitialized = true;

                        await this.Reset();

                        this.IsInitialized = true;

                        return true;
                    }
                }
            }
            return false;            
        }

        public async Task<bool> Reset()
        {
            await this.SendCommand("ATZ");                
            await this.SendCommand("ATE0");                
            await this.SendCommand("ATSP0");                
            await this.SendCommand("ATAL");
            return true;
        }

        public void Shutdown()
        {
            if (this.IsInitialized)
            {

            }
        }

        public async Task<string> SendCommand(string command)
        {            
            string response = string.Empty;
            if (this.IsInitialized)
            {                
                // Write
                this.writer.WriteString(command + "\r");
                await this.socket.OutputStream.WriteAsync(this.writer.DetachBuffer());                

                // Read
                while (true)
                {
                    uint size = await this.reader.LoadAsync(1);
                    string s = this.reader.ReadString(size);
                    if (s.Equals(">"))
                        break;
                    else
                        response += s;
                }                        
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

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

namespace VSDA.Connection
{
    public class BluetoothDataConnection : IDataConnection
    {
        private bool isInitialized;

        private RfcommDeviceService service;
        private StreamSocket socket;
        private DataWriter writer;
        private DataReader reader;

        public BluetoothDataConnection()
        {
            this.isInitialized = false;
        }

        public async void Initialize()
        {
            if (!this.isInitialized)
            {
                DeviceInformationCollection services = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));

                if (services.Count > 0)
                {
                    try
                    {
                        string name = services[0].Name;
                        this.service = await RfcommDeviceService.FromIdAsync(services[0].Id);
                    }
                    catch (InvalidOperationException e)
                    {
                        string s = e.StackTrace;
                    }
                    if (this.service != null)
                    {
                        this.socket = new StreamSocket();
                        await socket.ConnectAsync(this.service.ConnectionHostName, this.service.ConnectionServiceName, SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication);
                        
                        this.writer = new DataWriter(this.socket.OutputStream);
                        this.reader = new DataReader(this.socket.InputStream);

                        this.isInitialized = true;

                        this.Reset();
                    }
                }
            }            
        }

        public void Reset()
        {
            if(this.isInitialized)
            {
                var result = this.SendCommand("ATZ");
                result.Wait();
                result = this.SendCommand("ATE0");
                result.Wait();
                result = this.SendCommand("ATSP0");
                result.Wait();
                result = this.SendCommand("ATAL");
                result.Wait();
                result = this.SendCommand("0100");
                result.Wait();
            }
        }

        public void Shutdown()
        {
            if (this.isInitialized)
            {

            }
        }

        public async Task<string> SendCommand(string command)
        {
            string response = string.Empty;
            if (this.isInitialized)
            {
                // Write
                this.writer.WriteString(command + "\r");
                await this.socket.OutputStream.WriteAsync(this.writer.DetachBuffer());
                await Task.Delay(200);
                
                // Read
                while (true)
                {
                    uint size = await this.reader.LoadAsync(1);
                    string s = reader.ReadString(size);
                    if (s.Equals(">"))
                        break;
                    else
                        response += s;
                }                        
            }
            return response;
        }
    }
}

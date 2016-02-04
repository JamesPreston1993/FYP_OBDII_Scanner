using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VSDACore.Connection;
using System.ComponentModel;
using System.Threading.Tasks;
using Java.Util;
using Android.Bluetooth;

namespace VSDAAndroid.Connection
{
    public class BluetoothDataConnection : IDataConnection
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private UUID uuid = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

        private BluetoothSocket socket;
        private BluetoothDevice androidDevice;

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
                switch (this.isInitialized)
                {
                    case true: this.DeviceConnectionStatus = ConnectionStatus.Connected; break;
                    case false: this.DeviceConnectionStatus = ConnectionStatus.NotConnected; break;
                }
                this.RaisePropertyChanged("IsInitialized");
            }
        }

        public Protocol VehicleProtocol { get; private set; }

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

        private IDevice currentDevice;
        public IDevice CurrentDevice
        {
            get
            {
                return this.currentDevice;
            }
            set
            {
                this.currentDevice = value;
                this.androidDevice = BluetoothAdapter.DefaultAdapter.BondedDevices.First(m => m.Name == this.currentDevice.Name);
            }
        }

        public BluetoothDataConnection()
        {
            this.currentDevice = null;
            this.DeviceConnectionStatus = ConnectionStatus.NotConnected;
            this.isInitialized = false;
            this.socket = null;
            this.androidDevice = null;
        }

        public Task<IList<IDevice>> GetAvailableDevices()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Initialize()
        {
            if (!this.IsInitialized)
            {
                if (this.androidDevice != null)
                {
                    BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
                    this.socket = this.androidDevice.CreateRfcommSocketToServiceRecord(this.uuid);
                    if(socket != null)
                    {
                        try
                        {
                            await this.socket.ConnectAsync();
                            
                            this.isInitialized = true;
                            await this.Reset();
                            this.IsInitialized = true;
                        }
                        catch(Exception e)
                        {

                        }                        
                    }
                }
            }
            return false;
        }

        public Task<bool> AwaitShutdown()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Reset()
        {
            await this.SendCommand("ATZ");
            await this.SendCommand("ATAL");
            await this.SendCommand("ATE0");
            await this.SendCommand("ATL0");
            await this.SendCommand("ATS0");
            string protocol = await this.SendCommand("ATDP");
            this.VehicleProtocol = this.GetProtocol(protocol);
            await this.SendCommand("ATSP0");
            await this.SendCommand("01004");
            return true;
        }

        public async Task<string> SendCommand(string command)
        {
            string response = string.Empty;
            if (this.IsInitialized)
            {
                DateTime startTime = DateTime.Now;

                char[] charArray = (command + "\r").ToCharArray();
                byte[] outgoingMessage = new byte[charArray.Length];
                for (int i = 0; i < charArray.Length; i++)
                    outgoingMessage[i] = (byte)charArray[i];

                // Write
                await this.socket.OutputStream.WriteAsync(outgoingMessage, 0, outgoingMessage.Length);                

                // Read
                while (!response.Contains(">"))
                {
                    byte[] incomingMessage = new byte[64];
                    await this.socket.InputStream.ReadAsync(incomingMessage, 0, incomingMessage.Length);
                    
                    foreach (byte b in incomingMessage)
                        response += Convert.ToChar(b);
                   
                }

                DateTime endTime = DateTime.Now;
                string log = command + ": " + (endTime - startTime).TotalMilliseconds + "ms";
                System.Diagnostics.Debug.WriteLine(log);
            }
            else
            {
                response = "No connection";
            }
            return response;
        }

        private Protocol GetProtocol(string response)
        {
            Protocol protocol;

            if (response.Contains("ISO 9141-2"))
                protocol = Protocol.ISO9141;
            else if (response.Contains("SAE J1850 PWM"))
                protocol = Protocol.PWM;
            else if (response.Contains("SAE J1850 VPW"))
                protocol = Protocol.VPW;
            else if (response.Contains("ISO 14230-4 KWP"))
                protocol = Protocol.KWP;
            else if (response.Contains("ISO 15765-4 CAN"))
                protocol = Protocol.CAN;
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
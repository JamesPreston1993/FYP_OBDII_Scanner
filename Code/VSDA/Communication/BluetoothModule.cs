using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using VSDA.Connection;
using System.Windows.Input;

namespace VSDA.Communication
{
    public class BluetoothModule : IConnectionModule
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        private DeviceInformationCollection devices;
        public DeviceInformationCollection Devices
        {
            get
            {
                return this.devices;
            }
            private set
            {
                this.devices = value;
                this.RaisePropertyChanged("Devices");
            }
        }

        public string Name { get; private set; }

        public BluetoothModule()
        {
            this.Name = "Connection";
        }

        public async Task<bool> Initialize()
        {
            this.Devices = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));
            if(this.Devices.Count > 0)
            {                
                return true;
            }
            return false;
        }

        public async Task<bool> Connect(DeviceInformation device)
        {
            ConnectionManager.Instance.Device = device;
            bool val = await ConnectionManager.Instance.Initialize();
            return val;
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

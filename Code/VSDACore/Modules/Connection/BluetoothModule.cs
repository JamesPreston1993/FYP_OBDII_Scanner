using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using VSDACore.Connection;

namespace VSDACore.Modules.Connection
{
    public class BluetoothModule : IConnectionModule
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IList<IDevice> devices;
        public IList<IDevice> Devices
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
            this.Devices = await ConnectionManager.Instance.GetAvailableDevices();
            if (this.Devices.Count > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Connect(IDevice device)
        {
            ConnectionManager.Instance.CurrentDevice = device;
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

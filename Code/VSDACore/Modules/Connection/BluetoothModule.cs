using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using VSDACore.Connection;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Connection
{
    public class BluetoothModule : IConnectionModule
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IList<IHelpItem> HelpItems { get; private set; }

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

        private string connectionStatus;
        public string DeviceConnectionStatus
        {
            get
            {
                return this.connectionStatus;
            }
            private set
            {
                this.connectionStatus = value;
                this.RaisePropertyChanged("DeviceConnectionStatus");
            }
        }

        public string Name { get; private set; }

        public BluetoothModule()
        {
            this.Name = "Connection";
            this.HelpItems = HelpItemFactory.GetHelpItems(this);
            this.CommunicationLog = ConnectionManager.Instance.CommunicationLog;
            this.GetConnectionStatus();
            ConnectionManager.Instance.PropertyChanged += this.RaiseModelPropertyChanged;
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

        public async Task<bool> Shutdown()
        {
            this.Devices.Clear();
            return true;
        }

        public async Task<bool> Connect(IDevice device)
        {
            ConnectionManager.Instance.CurrentDevice = device;
            bool val = false;
            try
            {
                await ConnectionManager.Instance.Initialize();
            }
            catch(Exception e)
            {
                val = false;
            }
            return val;
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void GetConnectionStatus()
        {
            switch (ConnectionManager.Instance.DeviceConnectionStatus)
            {
                case ConnectionStatus.Connected:
                case ConnectionStatus.Connecting:
                    this.DeviceConnectionStatus = ConnectionManager.Instance.DeviceConnectionStatus.ToString();
                    break;
                case ConnectionStatus.NotConnected:
                    this.DeviceConnectionStatus = "Not Connected";
                    break;
            }
        }

        private void RaiseModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DeviceConnectionStatus")
            {
                this.GetConnectionStatus();
            }
            else if (e.PropertyName == "CommunicationLog")
            {
                this.CommunicationLog = ConnectionManager.Instance.CommunicationLog;
            }
            else
            {
                this.RaisePropertyChanged(e.PropertyName);
            }
        }
    }
}

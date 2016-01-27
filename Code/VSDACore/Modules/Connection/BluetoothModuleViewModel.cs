﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VSDACore.Connection;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Connection
{
    public class BluetoothModuleViewModel : IConnectionModuleViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ConnectCommand { get; private set; }

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

        private IConnectionModule connectionModule;
        public IModule ModuleModel { get; set; }

        public string Name { get; private set; }

        private IList<IDevice> devices;
        public IList<IDevice> Devices
        {
            get
            {
                return this.devices;
            }
            set
            {
                this.devices = value;
                this.RaisePropertyChanged("Devices");
            }
        }

        public IDevice CurrentDevice { get; set; }

        public BluetoothModuleViewModel(IConnectionModule module)
        {
            this.ModuleModel = module;
            this.connectionModule = module;
            this.Name = module.Name;
            this.CurrentDevice = null;
            this.ConnectCommand = new RelayCommand(this.Connect);
            this.DeviceConnectionStatus = this.connectionModule.DeviceConnectionStatus;
            this.ModuleModel.PropertyChanged += this.RaiseModelPropertyChanged;
        }

        public async Task<bool> InitializeModule()
        {
            bool val = await this.ModuleModel.Initialize();
            return true;
        }

        public async void Connect()
        {
            if (this.CurrentDevice != null)
            {
                await this.connectionModule.Connect(this.CurrentDevice);
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void RaiseModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Devices")
            {
                this.Devices = this.connectionModule.Devices;
            }
            else if(e.PropertyName == "DeviceConnectionStatus")
            {
                this.DeviceConnectionStatus = this.connectionModule.DeviceConnectionStatus;
            }
            this.RaisePropertyChanged(e.PropertyName);
        }
    }
}

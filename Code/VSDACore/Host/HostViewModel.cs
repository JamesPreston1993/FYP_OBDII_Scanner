using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VSDACore.Modules.Base;
using VSDACore.Modules.Codes;
using VSDACore.Modules.Connection;
using VSDACore.Modules.Data;

namespace VSDACore.Host
{
    public class HostViewModel : IHostViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IHost HostModel { get; private set; }

        private IModuleViewModel currentModule;
        public IModuleViewModel CurrentModule
        {
            get
            {
                return this.currentModule;
            }
            set
            {
                this.currentModule = value;
                this.CurrentModuleName = this.currentModule.Name;
                this.HostModel.CurrentModule = this.CurrentModule.ModuleModel;
                this.RaisePropertyChanged("CurrentModule");
            }
        }

        private string currentModuleName;
        public string CurrentModuleName
        {
            get
            {
                return this.currentModuleName;
            }
            private set
            {
                this.currentModuleName = value;
                this.RaisePropertyChanged("CurrentModuleName");
            }
        }

        private IList<IModuleViewModel> modules;
        public IList<IModuleViewModel> Modules
        {
            get
            {
                return this.modules;
            }
            set
            {
                this.modules = value;
                this.RaisePropertyChanged("Modules");
            }
        }

        public HostViewModel(IHost hostModel)
        {
            this.HostModel = hostModel;
            this.CurrentModuleName = hostModel.CurrentModule.Name;
            this.Modules = new List<IModuleViewModel>();
            foreach (IModule module in this.HostModel.Modules)
            {
                switch (module.Name)
                {
                    case "Data": this.Modules.Add(new DataModuleViewModel(module as IDataModule)); break;
                    case "Codes": this.Modules.Add(new DTCModuleViewModel(module as IDtcModule)); break;
                    case "Connection": this.Modules.Add(new BluetoothModuleViewModel(module as IConnectionModule)); break;
                }
            }
            this.CurrentModule = this.Modules.First(m => m.Name == this.CurrentModuleName);
            this.HostModel.PropertyChanged += this.RaiseModelPropertyChanged;
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
            if (e.PropertyName == "CurrentModule")
            {
                this.currentModule = this.Modules.First(m => m.Name == this.HostModel.CurrentModule.Name);
                this.CurrentModuleName = this.currentModule.Name;
                this.RaisePropertyChanged(e.PropertyName);
            }
            else if(e.PropertyName == "IsInitialized")
            {
                this.RaisePropertyChanged("IsInitialized");
            }
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VSDACore.Connection;
using VSDACore.Modules.Base;

namespace VSDACore.Host
{
    public class Host : IHost
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IList<IModule> Modules { get; private set; }

        private IModule currentModule;
        public IModule CurrentModule
        {
            get
            {
                return this.currentModule;
            }
            set
            {
                this.currentModule = value;
                this.RaisePropertyChanged("CurrentModule");
            }
        }

        public Host(IList<IModule> modules)
        {
            this.Modules = modules;
            this.CurrentModule = this.Modules.First(m => m.Name == "Connection");            
            ConnectionManager.Instance.PropertyChanged += this.RaiseConnectionPropertyChanged;
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void RaiseConnectionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsInitialized")
            {
                
                switch (ConnectionManager.Instance.IsInitialized)
                {
                    case true:
                        this.CurrentModule = this.Modules[0];
                        break;

                    case false:
                        this.CurrentModule = this.Modules.First(m => m.Name == "Connection");
                        break;
                }
                
            }
        }
    }
}

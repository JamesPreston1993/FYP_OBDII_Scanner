using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using VSDA.Connection;
using System.ComponentModel;

namespace VSDA.Communication
{
    public class Host : IHost
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<IModule> Modules { get; private set; }

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

        public Host(List<IModule> modules)
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
            if(e.PropertyName == "IsInitialized")
            {
                switch(ConnectionManager.Instance.IsInitialized)
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

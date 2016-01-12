using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VSDA.Communication.Data;
using VSDA.Communication.DTC;

namespace VSDA.Communication
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
            this.RaisePropertyChanged(e.PropertyName);
        }
    }  
}

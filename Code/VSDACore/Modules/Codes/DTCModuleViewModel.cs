using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Codes
{
    public class DTCModuleViewModel : IDtcModuleViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IList<IHelpItem> HelpItems { get; private set; }

        private IDtcModule dtcModuleModel;
        public IModule ModuleModel { get; set; }

        public string Name { get; private set; }

        private IList<ICodeViewModel> currentCodes;
        public IList<ICodeViewModel> CurrentCodes
        {
            get
            {
                return this.currentCodes;
            }
            private set
            {
                this.currentCodes = value;
                this.RaisePropertyChanged("CurrentCodes");
            }
        }

        private IList<ICodeViewModel> pendingCodes;
        public IList<ICodeViewModel> PendingCodes
        {
            get
            {
                return this.pendingCodes;
            }
            private set
            {
                this.pendingCodes = value;
                this.RaisePropertyChanged("PendingCodes");
            }
        }

        private IList<ICodeViewModel> permanentCodes;
        public IList<ICodeViewModel> PermanentCodes
        {
            get
            {
                return this.permanentCodes;
            }
            private set
            {
                this.permanentCodes = value;
                this.RaisePropertyChanged("PermanentCodes");
            }
        }

        public ICommand ClearCodesCommand { get; private set; }

        public DTCModuleViewModel(IDtcModule module)
        {
            this.ModuleModel = module;
            this.dtcModuleModel = module;
            this.Name = this.ModuleModel.Name;
            this.HelpItems = module.HelpItems;
            this.CurrentCodes = new List<ICodeViewModel>();
            this.PendingCodes = new List<ICodeViewModel>();
            this.PermanentCodes = new List<ICodeViewModel>();
            this.ClearCodesCommand = new RelayCommand(this.ClearCodes);
            foreach (ICode code in this.dtcModuleModel.CurrentCodes)
            {
                this.CurrentCodes.Add(new CodeViewModel(code));
            }
            foreach (ICode code in this.dtcModuleModel.PendingCodes)
            {
                this.PendingCodes.Add(new CodeViewModel(code));
            }
            foreach (ICode code in this.dtcModuleModel.PermanentCodes)
            {
                this.PermanentCodes.Add(new CodeViewModel(code));
            }
            this.ModuleModel.PropertyChanged += this.RaiseModelPropertyChanged;
        }

        public async Task<bool> InitializeModule()
        {
            bool val = await this.ModuleModel.Initialize();
            return true;
        }

        private async void ClearCodes()
        {
            await this.dtcModuleModel.ClearCodes();
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void RaiseModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentCodes")
            {
                this.currentCodes.Clear();
                foreach (ICode code in this.dtcModuleModel.CurrentCodes)
                {
                    this.CurrentCodes.Add(new CodeViewModel(code));
                }
            }
            if (e.PropertyName == "PendingCodes")
            {
                this.pendingCodes.Clear();
                foreach (ICode code in this.dtcModuleModel.PendingCodes)
                {
                    this.PendingCodes.Add(new CodeViewModel(code));
                }
            }
            if (e.PropertyName == "PermanentCodes")
            {
                this.permanentCodes.Clear();
                foreach (ICode code in this.dtcModuleModel.PermanentCodes)
                {
                    this.PermanentCodes.Add(new CodeViewModel(code));
                }
            }
            this.RaisePropertyChanged(e.PropertyName);
        }
    }
}

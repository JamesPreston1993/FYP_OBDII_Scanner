using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSDA.Connection;

namespace VSDA.Communication
{
    public class DTCModule : IDtcModule
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; private set; }

        private IList<ICode> currentCodes;
        public IList<ICode> CurrentCodes
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

        private IList<ICode> pendingCodes;
        public IList<ICode> PendingCodes
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

        private IList<ICode> permanentCodes;
        public IList<ICode> PermanentCodes
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

        private IDtcCommsSystem commsSystem;

        public DTCModule()
        {
            this.Name = "Codes";
            this.commsSystem = new DTCCommunicationSystem();
            this.currentCodes = this.pendingCodes = this.permanentCodes = new List<ICode>();
        }

        public async Task<bool> Initialize()
        {
            this.commsSystem.Initialize();
            await this.GetCurrentCodes();
            await this.GetPendingCodes();
            await this.GetPermanentCodes();
            return true;
        }        

        public async Task<IList<ICode>> GetCurrentCodes()
        {
            this.CurrentCodes = await this.commsSystem.GetCurrentCodes();
            return this.CurrentCodes;
        }

        public async Task<IList<ICode>> GetPendingCodes()
        {
            this.PendingCodes = await this.commsSystem.GetPendingCodes();
            return this.PendingCodes;
        }

        public async Task<IList<ICode>> GetPermanentCodes()
        {
            this.PermanentCodes = await this.commsSystem.GetPermanentCodes();
            return this.PermanentCodes;
        }

        public async Task<bool> ClearCodes()
        {
            bool val = await this.commsSystem.ClearCodes();
            await this.GetCurrentCodes();
            return val;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

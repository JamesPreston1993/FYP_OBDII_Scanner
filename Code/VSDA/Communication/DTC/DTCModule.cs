using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSDA.Connection;

namespace VSDA.Communication
{
    public class DTCModule : IDtcModule
    {
        public string Name { get; private set; }

        private IDtcCommsSystem commsSystem;
        
        public DTCModule()
        {
            this.Name = "Codes";
            this.commsSystem = new DTCCommunicationSystem();
        }

        public async Task<bool> Initialize()
        {
            this.commsSystem.Initialize();
            return true;
        }

        public void Notify()
        {

        }

        public async Task<IList<ICode>> GetCurrentCodes()
        {
            return await this.commsSystem.GetCurrentCodes();
        }

        public async Task<IList<ICode>> GetPendingCodes()
        {
            return await this.commsSystem.GetPendingCodes();
        }

        public async Task<IList<ICode>> GetPermanentCodes()
        {
            return await this.commsSystem.GetPermanentCodes();
        }

        public async Task<bool> ClearCodes()
        {
            return await this.commsSystem.ClearCodes();
        }
    }
}

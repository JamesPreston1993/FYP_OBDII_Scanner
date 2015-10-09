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

        public void Initialize()
        {
            this.commsSystem.Initialize();
        }

        public void Notify()
        {

        }

        public async Task<IList<ICode>> GetCurrentCodes()
        {
            return await this.commsSystem.GetCurrentCodes();
        }

        public void GetHistoryCodes()
        {
            this.commsSystem.GetHistoryCodes();
        }

        public void GetPermanentCodes()
        {
            this.commsSystem.GetPermanentCodes();
        }

        public void ClearCodes()
        {
            this.commsSystem.ClearCodes();
        }
    }
}

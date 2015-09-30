using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSDA.Connection;

namespace VSDA.Communication
{
    public class DTCModule : IModule
    {
        private ICommunicationSystem commsSystem;
        

        public DTCModule()
        {
            this.commsSystem = new DTCCommunicationSystem();
        }

        public void Initialize()
        {
            this.commsSystem.Initialize();
        }

        public void Notify()
        {

        }
    }
}

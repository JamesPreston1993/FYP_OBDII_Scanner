using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSDA.Connection;

namespace VSDA.Communication
{
    public class DTCCommunicationSystem : ICommunicationSystem
    {
        private IDataConnection dataConnection;

        public DTCCommunicationSystem()
        {
            this.dataConnection = ConnectionManager.Instance;
        }

        public void Initialize()
        {
            this.dataConnection.Initialize();
        }

        public void Update()
        {

        }

        public void Shutdown()
        {

        }

        public string SendCommand(string command)
        {
            var result = this.dataConnection.SendCommand(command);
            result.Wait();
            return result.Result;
        }
    }
}

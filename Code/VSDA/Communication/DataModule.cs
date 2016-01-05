using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public class DataModule : IDataModule
    {
        public string Name { get; private set; }

        public IList<IPid> Pids { get; private set; }

        private IDataCommsSystem commsSystem;

        private bool isRecording;

        public DataModule()
        {
            this.Name = "Data";
            this.Pids = new List<IPid>();
            this.commsSystem = new DataCommunicationSystem();
        }

        public void Initialize()
        {
            this.commsSystem.Initialize();            
        }

        public void Notify()
        {

        }

        public async Task<IList<IPid>> GetSupportedPids()
        {
            this.Pids = await this.commsSystem.GetSupportedPids();
            return this.Pids;
        }

        public async Task<bool> UpdateData(IPid pid)
        {
            await this.commsSystem.UpdateData(pid);
            return true;
        }

        public async Task<bool> UpdateData()
        {
            await this.commsSystem.UpdateData(this.Pids);

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication.Data
{
    public class DataModule : IDataModule
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; private set; }

        private ObservableCollection<IPid> pids;
        public ObservableCollection<IPid> Pids
        {
            get
            {
                return this.pids;
            }
            private set
            {
                this.pids = value;
                this.RaisePropertyChanged("Pids");
            }
        }

        private IDataCommsSystem commsSystem;        

        public bool IsRecording { get; set; }        

        public DataModule()
        {
            this.Name = "Data";
            this.Pids = new ObservableCollection<IPid>();
            this.commsSystem = new DataCommunicationSystem();
            this.IsRecording = false;            
            this.Pids.CollectionChanged += this.RaiseCollectionChanged;            
        }

        public async Task<bool> Initialize()
        {
            this.commsSystem.Initialize();
            this.Pids = await this.GetSupportedPids();
            return true;
        }

        public async Task<ObservableCollection<IPid>> GetSupportedPids()
        {
            IList<IPid> pids = await this.commsSystem.GetSupportedPids();
            this.Pids = new ObservableCollection<IPid>(pids);
            return this.Pids;
        }

        public async Task<bool> UpdateData(IPid pid)
        {            
            if (this.IsRecording)
            {
                await this.commsSystem.UpdateData(pid);
            }
            return IsRecording;
        }

        public async Task<bool> UpdateData()
        {
            while (this.IsRecording)
            {
                await this.commsSystem.UpdateData(this.Pids);
            }
            return IsRecording;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void RaiseCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged("Pids");
        }
    }
}

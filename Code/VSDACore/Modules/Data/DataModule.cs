using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Data
{
    public class DataModule : IDataModule
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SemaphoreSlim asyncLock;

        public IList<IHelpItem> HelpItems { get; private set; }

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

        private bool isRecording;
        public bool IsRecording
        {
            get
            {
                return this.isRecording;
            }
            set
            {
                this.isRecording = value;
                this.RaisePropertyChanged("IsRecording");
            }
        }

        private IDataCommsSystem commsSystem;        

        public DataModule()
        {
            this.Name = "Data";
            this.HelpItems = HelpItemFactory.GetHelpItems(this);
            this.Pids = new ObservableCollection<IPid>();
            this.commsSystem = new DataCommunicationSystem();
            this.asyncLock = new SemaphoreSlim(1);
            this.IsRecording = false;
            this.Pids.CollectionChanged += this.RaiseCollectionChanged;
        }

        public async Task<bool> Initialize()
        {
            this.commsSystem.Initialize();
            try
            {
                this.Pids = await this.GetSupportedPids();
            }
            catch(Exception e)
            {

            }
            return true;
        }

        public async Task<bool> Shutdown()
        {
            this.IsRecording = false;

            //await this.asyncLock.WaitAsync();

            //foreach(IPid pid in this.Pids)
            //{
               // pid.DataItems.Clear();
            //}

            //this.asyncLock.Release();

            return true;
        }

        public async Task<ObservableCollection<IPid>> GetSupportedPids()
        {
            await this.asyncLock.WaitAsync();

            if (this.commsSystem != null)
            {
                IList<IPid> pids = await this.commsSystem.GetSupportedPids();
                this.Pids = new ObservableCollection<IPid>(pids);
            }

            this.asyncLock.Release();

            return this.Pids;
        }

        public async Task<bool> UpdateData(IPid pid)
        {
            await this.asyncLock.WaitAsync();

            if (this.IsRecording)
            {
                if (this.commsSystem != null)
                {
                    await this.commsSystem.UpdateData(pid);
                }
            }

            this.asyncLock.Release();

            return IsRecording;
        }

        public async Task<bool> UpdateData()
        {
            while (this.IsRecording)
            {
                await this.asyncLock.WaitAsync();
                if (this.commsSystem != null)
                {
                    await this.commsSystem.UpdateData(this.Pids);
                }
                this.asyncLock.Release();
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

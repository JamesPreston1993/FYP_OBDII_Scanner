using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace VSDACore.Modules.Data
{
    public class DataListViewModel : IDataListViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IPid PidModel { get; private set; }

        public string PidName { get; private set; }

        private int currentSample;
        public int CurrentSample
        {
            get
            {
                return this.currentSample;
            }
            set
            {
                if (value >= 0 && value < this.DataItems.Count)
                {
                    this.currentSample = value;
                    this.CurrentValue = this.DataItems[this.currentSample];
                }
            }
        }

        public IList<string> DataItems { get; private set; }

        private string currentValue;
        public string CurrentValue
        {
            get
            {
                return this.currentValue;
            }
            private set
            {
                this.currentValue = value;
                this.RaisePropertyChanged("CurrentValue");
            }
        }

        public DataListViewModel(IPid pid)
        {
            this.PidModel = pid;
            this.PidName = pid.Name;
            this.currentSample = 0;
            this.DataItems = pid.DataItems;
            this.CurrentValue = "--";
            this.PidModel.PropertyChanged += this.RaiseModelPropertyChanged;
        }

        public void StepBack()
        {
            if (this.currentSample > 0)
            {
                this.CurrentSample -= 1;
            }
        }

        public void StepForward()
        {
            if (this.currentSample < this.DataItems.Count - 1)
            {
                this.CurrentSample += 1;
            }
        }

        public void SkipToStart()
        {
            if (this.currentSample > 0)
            {
                this.CurrentSample = 0;
            }
        }

        public void SkipToEnd()
        {
            if (this.currentSample < this.DataItems.Count - 1)
            {
                this.CurrentSample = this.DataItems.Count - 1;
            }
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
            if (e.PropertyName == "DataItems")
            {
                this.RaisePropertyChanged("DataItems");
                this.CurrentValue = this.DataItems.Last();
                this.currentSample = this.DataItems.Count - 1;
            }

        }
    }
}

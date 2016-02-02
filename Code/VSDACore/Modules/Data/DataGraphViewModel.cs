using System.Collections.Generic;
using System.ComponentModel;

namespace VSDACore.Modules.Data
{
    public class DataGraphViewModel : IDataGraphViewModel
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
                this.currentSample = value;
                this.CursorPosition = this.currentSample;
                this.RaisePropertyChanged("CurrentSample");
            }
        }

        public IList<IDataItem> DataItems { get; private set; }

        private double maxValue;
        public double MaxPossibleValue
        {
            get
            {
                return this.maxValue;
            }
            private set
            {
                this.maxValue = value;
                this.RaisePropertyChanged("MaxPossibleValue");
            }
        }

        private double minValue;
        public double MinPossibleValue
        {
            get
            {
                return this.minValue;
            }
            private set
            {
                this.minValue = value;
                this.RaisePropertyChanged("MinPossibleValue");
            }
        }

        private double cursorPosition;
        public double CursorPosition
        {
            get
            {
                return this.cursorPosition;
            }
            private set
            {
                this.cursorPosition = value;
                this.RaisePropertyChanged("CursorPosition");
            }
        }

        public DataGraphViewModel(IPid pid)
        {
            this.PidModel = pid;
            this.PidName = pid.Name;
            this.currentSample = 0;
            this.MaxPossibleValue = pid.MaxPossibleValue;
            this.MinPossibleValue = pid.MinPossibleValue;
            this.DataItems = pid.DataItems;
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
                this.CurrentSample = this.DataItems.Count - 1;
                this.RaisePropertyChanged("DataItems");
            }
        }
    }
}

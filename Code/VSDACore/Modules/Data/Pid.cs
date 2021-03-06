﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace VSDACore.Modules.Data
{
    public class Pid : IPid
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<IDataItem> DataItems { get; private set; }

        public string Name { get; private set; }

        public string PidHex { get; private set; }

        public double MaxPossibleValue { get; private set; }

        public double MinPossibleValue { get; private set; }

        //public string CurrentValue { get; private set; }

        public Pid(string hex, string name, double min, double max)
        {
            this.PidHex = hex;
            this.Name = name;
            this.MinPossibleValue = min;
            this.MaxPossibleValue = max;
            this.DataItems = new ObservableCollection<IDataItem>();
            this.DataItems.CollectionChanged += this.RaiseCollectionChanged;
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
            this.RaisePropertyChanged("DataItems");
        }
    }
}

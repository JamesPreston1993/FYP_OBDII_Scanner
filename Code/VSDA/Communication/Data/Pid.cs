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
    public class Pid : IPid
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> DataItems { get; private set; }

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
            this.DataItems = new ObservableCollection<string>();
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

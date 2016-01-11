using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public interface IPid :  INotifyPropertyChanged
    {
        ObservableCollection<string> DataItems { get; }

        string Name { get; }

        string PidHex { get; }

        double MaxPossibleValue { get; }

        double MinPossibleValue { get; }
    }
}

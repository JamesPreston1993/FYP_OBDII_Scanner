using System.Collections.ObjectModel;
using System.ComponentModel;

namespace VSDACore.Modules.Data
{
    public interface IPid : INotifyPropertyChanged
    {
        ObservableCollection<IDataItem> DataItems { get; }

        string Name { get; }

        string PidHex { get; }

        double MaxPossibleValue { get; }

        double MinPossibleValue { get; }
    }
}

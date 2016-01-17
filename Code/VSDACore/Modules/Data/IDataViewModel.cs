using System.Collections.Generic;
using System.ComponentModel;

namespace VSDACore.Modules.Data
{
    public interface IDataViewModel : INotifyPropertyChanged
    {
        IPid PidModel { get; }

        string PidName { get; }

        int CurrentSample { get; set; }

        IList<string> DataItems { get; }

        void StepBack();

        void StepForward();

        void SkipToStart();

        void SkipToEnd();
    }
}

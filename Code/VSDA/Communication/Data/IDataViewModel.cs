using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSDA.Communication;

namespace VSDA.Communication.Data
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

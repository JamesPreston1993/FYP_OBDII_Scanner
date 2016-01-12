using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace VSDA.Communication
{
    public interface IDataModule : IModule
    {
        ObservableCollection<IPid> Pids { get; }

        bool IsRecording { get; set; }

        Task<ObservableCollection<IPid>> GetSupportedPids();

        Task<bool> UpdateData(IPid pid);

        Task<bool> UpdateData();
    }
}

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Data
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

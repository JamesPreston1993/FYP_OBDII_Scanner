using System.ComponentModel;
using System.Threading.Tasks;

namespace VSDACore.Modules.Base
{
    public interface IModule : INotifyPropertyChanged
    {
        string Name { get; }

        Task<bool> Initialize();
    }
}

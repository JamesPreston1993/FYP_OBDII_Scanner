using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace VSDACore.Modules.Base
{
    public interface IModule : INotifyPropertyChanged
    {
        string Name { get; }

        IList<IHelpItem> HelpItems { get; }

        Task<bool> Initialize();
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace VSDACore.Modules.Base
{
    public interface IModuleViewModel : INotifyPropertyChanged
    {
        IModule ModuleModel { get; set; }

        string Name { get; }

        IList<IHelpItem> HelpItems { get; }

        Task<bool> InitializeModule();
    }
}

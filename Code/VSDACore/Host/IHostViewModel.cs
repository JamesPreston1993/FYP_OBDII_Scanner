using System.Collections.Generic;
using System.ComponentModel;
using VSDACore.Modules.Base;

namespace VSDACore.Host
{
    public interface IHostViewModel : INotifyPropertyChanged
    {
        IHost HostModel { get; }

        string CurrentModuleName { get; }

        IModuleViewModel CurrentModule { get; set; }

        IList<IModuleViewModel> Modules { get; }
    }
}

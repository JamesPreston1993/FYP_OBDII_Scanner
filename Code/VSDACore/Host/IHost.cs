using System.Collections.Generic;
using System.ComponentModel;
using VSDACore.Modules.Base;

namespace VSDACore.Host
{
    public interface IHost : INotifyPropertyChanged
    {
        IList<IModule> Modules { get; }

        IModule CurrentModule { get; set; }
    }
}

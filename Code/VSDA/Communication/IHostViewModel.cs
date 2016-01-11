using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public interface IHostViewModel : INotifyPropertyChanged
    {
        IHost HostModel { get; }

        string CurrentModuleName { get; }

        IModuleViewModel CurrentModule { get; set; }

        IList<IModuleViewModel> Modules { get; }
    }
}

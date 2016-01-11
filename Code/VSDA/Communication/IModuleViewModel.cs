using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public interface IModuleViewModel : INotifyPropertyChanged
    {
        string Name { get; }

        Task<bool> InitializeModule();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Enumeration;

namespace VSDA.Communication
{
    public interface IConnectionModuleViewModel : IModuleViewModel
    {
        ICommand ConnectCommand { get; }

        DeviceInformation CurrentDevice { get; set; }

        DeviceInformationCollection Devices { get; set; }
    }
}

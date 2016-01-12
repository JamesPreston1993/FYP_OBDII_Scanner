using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VSDA.Communication.DTC
{
    public interface IDtcModuleViewModel : IModuleViewModel
    {
        //IDtcModule ModuleModel { get; }

        IList<ICodeViewModel> CurrentCodes { get; }

        IList<ICodeViewModel> PendingCodes { get; }

        IList<ICodeViewModel> PermanentCodes { get; }

        ICommand ClearCodesCommand { get; }        
    }
}

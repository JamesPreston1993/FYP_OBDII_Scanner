using System.Collections.Generic;
using System.Windows.Input;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Codes
{
    public interface IDtcModuleViewModel : IModuleViewModel
    {
        IList<ICodeViewModel> CurrentCodes { get; }

        IList<ICodeViewModel> PendingCodes { get; }

        IList<ICodeViewModel> PermanentCodes { get; }

        ICommand ClearCodesCommand { get; }
    }
}

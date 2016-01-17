using System.Collections.Generic;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Data
{
    public interface IDataModuleViewModel : IModuleViewModel
    {
        IList<IDataListViewModel> ListViews { get; }

        IList<IDataGraphViewModel> GraphViews { get; }
    }
}

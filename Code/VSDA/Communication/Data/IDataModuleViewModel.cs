using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VSDA.Communication.Data
{
    public interface IDataModuleViewModel : IModuleViewModel
    {
        IDataModule ModuleModel { get; }

        IList<IDataListViewModel> ListViews { get; }

        IList<IDataGraphViewModel> GraphViews { get; }

        // Commands
    }
}

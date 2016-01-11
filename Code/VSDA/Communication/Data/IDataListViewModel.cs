using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication.Data
{
    public interface IDataListViewModel : IDataViewModel
    {
        string CurrentValue { get; }
    }
}

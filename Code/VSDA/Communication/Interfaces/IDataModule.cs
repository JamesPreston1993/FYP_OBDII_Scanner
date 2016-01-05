using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public interface IDataModule : IModule
    {
        IList<IPid> Pids { get; }

        Task<IList<IPid>> GetSupportedPids();

        Task<bool> UpdateData(IPid pid);

        Task<bool> UpdateData();
    }
}

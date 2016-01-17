using System.Collections.Generic;
using System.Threading.Tasks;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Data
{
    public interface IDataCommsSystem : ICommunicationSystem
    {
        Task<IList<IPid>> GetSupportedPids();

        Task<bool> UpdateData(IPid pid);

        Task<bool> UpdateData(IList<IPid> pids);
    }
}

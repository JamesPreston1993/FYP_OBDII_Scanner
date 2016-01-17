using System.Collections.Generic;
using System.Threading.Tasks;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Codes
{
    public interface IDtcCommsSystem : ICommunicationSystem
    {
        Task<IList<ICode>> GetCurrentCodes();

        Task<IList<ICode>> GetPendingCodes();

        Task<IList<ICode>> GetPermanentCodes();

        Task<bool> ClearCodes();
    }
}

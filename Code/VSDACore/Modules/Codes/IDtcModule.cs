using System.Collections.Generic;
using System.Threading.Tasks;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Codes
{
    public interface IDtcModule : IModule
    {
        IList<ICode> CurrentCodes { get; }

        IList<ICode> PendingCodes { get; }

        IList<ICode> PermanentCodes { get; }

        Task<IList<ICode>> GetCurrentCodes();

        Task<IList<ICode>> GetPendingCodes();

        Task<IList<ICode>> GetPermanentCodes();

        Task<bool> ClearCodes();
    }
}

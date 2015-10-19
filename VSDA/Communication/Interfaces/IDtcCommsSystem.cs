using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public interface IDtcCommsSystem : ICommunicationSystem
    {
        Task<IList<ICode>> GetCurrentCodes();

        Task<IList<ICode>> GetPendingCodes();

        Task<IList<ICode>> GetPermanentCodes();

        void ClearCodes();
    }
}

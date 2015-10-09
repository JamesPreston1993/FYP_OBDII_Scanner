using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public interface IDtcModule : IModule
    {
        Task<IList<ICode>> GetCurrentCodes();

        void GetHistoryCodes();

        void GetPermanentCodes();
    }
}

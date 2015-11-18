using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public interface IHost
    {
        List<IModule> Modules { get; }

        IModule CurrentModule { get; set; }

        void Initialize();        
    }
}

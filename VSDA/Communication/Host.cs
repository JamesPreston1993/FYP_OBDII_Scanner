using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace VSDA.Communication
{
    public class Host : IHost
    {
        private List<IModule> modules;

        public Host(List<IModule> modules)
        {
            this.modules = modules;
            this.CurrentModule = modules[0];
        }
    
        public IModule CurrentModule { get; set; }

        public void Initialize()
        {
            this.CurrentModule.Initialize();               
        }
    }
}

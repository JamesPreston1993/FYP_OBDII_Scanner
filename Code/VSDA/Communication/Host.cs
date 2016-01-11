using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using VSDA.Connection;


namespace VSDA.Communication
{
    public class Host : IHost
    {
        public List<IModule> Modules { get; private set; }
        public IModule CurrentModule { get; set; }

        public Host(List<IModule> modules)
        {
            this.Modules = modules;
            this.CurrentModule = modules[0];
            ConnectionManager.Instance.Initialize();
        }            

        public void Initialize()
        {
            //this.CurrentModule.Initialize();               
        }
    }
}

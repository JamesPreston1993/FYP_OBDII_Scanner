using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Home
{
    public class HomeModule : IHomeModule
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; private set; }

        public IList<IHelpItem> HelpItems { get; private set; }

        public async Task<bool> Initialize()
        {
            return true;
        }

        public async Task<bool> Shutdown()
        {
            return true;
        }
    }
}

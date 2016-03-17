using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Home
{
    public class HomeModule : IHomeModule
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; private set; }

        public IList<IHelpItem> HelpItems { get; private set; }        
        
        public HomeModule()
        {
            this.Name = "Home";
            this.HelpItems = HelpItemFactory.GetHelpItems(this);
        }

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

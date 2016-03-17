using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Home
{
    public class HomeModuleViewModel : IHomeModuleViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; private set; }

        public IList<IHelpItem> HelpItems { get; private set; }
        
        public IModule ModuleModel { get; set; }

        public HomeModuleViewModel(IHomeModule module)
        {
            this.ModuleModel = module;
            this.Name = module.Name;
            this.HelpItems = module.HelpItems;
        }

        public async Task<bool> InitializeModule()
        {
            await this.ModuleModel.Initialize();
            return true;
        }

        public string FormatForEmail()
        {
            return string.Empty;
        }
    }
}

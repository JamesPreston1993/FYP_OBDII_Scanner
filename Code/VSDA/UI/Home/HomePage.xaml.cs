using VSDACore.Modules.Base;
using VSDACore.Modules.Home;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VSDA.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private IHomeModuleViewModel module;

        public HomePage(IHomeModuleViewModel module)
        {
            this.module = module;
            this.InitializeComponent();

            this.ModuleHintsGrid.Children.Clear();
            foreach (IHelpItem moduleHint in this.module.HelpItems)
            {
                this.ModuleHintsGrid.Children.Add(new ModuleHintView(moduleHint));
                this.ModuleHintsGrid.Children.Add(new TextBlock() { Height = 10 });
            }
        }
    }
}

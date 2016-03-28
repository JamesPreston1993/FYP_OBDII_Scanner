using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.ComponentModel;
using VSDACore.Modules.Codes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VSDA.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DTCPage : Page
    {
        private IDtcModuleViewModel module;

        public DTCPage(IDtcModuleViewModel module)
        {
            this.module = module;
            this.DataContext = this.module;            
            this.InitializeComponent();
            this.Loaded += this.PageLoaded;
            this.module.PropertyChanged += this.RaiseViewModelChanged;
        }

        public async void PageLoaded(object sender, RoutedEventArgs e)
        {
            await this.module.InitializeModule();

            this.CurrentCodesControl.Children.Clear();
            this.PendingCodesControl.Children.Clear();
            this.PermanentCodesControl.Children.Clear();

            if (this.module.CurrentCodes.Count == 0)
                this.CurrentCodesControl.Children.Add(new DTCView());            
            else
            {
                foreach (ICodeViewModel code in this.module.CurrentCodes)
                    this.CurrentCodesControl.Children.Add(new DTCView(code));
            }

            if (this.module.PendingCodes.Count == 0)
                this.PendingCodesControl.Children.Add(new DTCView());
            else
            {
                foreach (ICodeViewModel code in this.module.PendingCodes)
                    this.PendingCodesControl.Children.Add(new DTCView(code));
            }

            if (this.module.PermanentCodes.Count == 0)
                this.PermanentCodesControl.Children.Add(new DTCView());
            else
            {
                foreach (ICodeViewModel code in this.module.PermanentCodes)
                    this.PermanentCodesControl.Children.Add(new DTCView(code));
            }
        }

        public void RaiseViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentCodes")
            {
                this.CurrentCodesControl.Children.Clear();

                if (this.module.CurrentCodes.Count == 0)
                {
                    this.CurrentCodesControl.Children.Add(new DTCView());
                }
                else
                {
                    foreach (ICodeViewModel code in this.module.CurrentCodes)
                        this.CurrentCodesControl.Children.Add(new DTCView(code));
                }
            }
        }
    }
}

using VSDACore.Modules.Codes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace VSDA.UI
{
    public sealed partial class DTCView : UserControl
    {
        private ICodeViewModel code;

        public DTCView(ICodeViewModel code)
        {
            this.code = code;
            this.DataContext = code;
            this.InitializeComponent();            
        }

        private void ExpandClick(object sender, RoutedEventArgs e)
        {
            
        }

    }
}

using VSDACore.Modules.Data;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace VSDA.UI
{
    public sealed partial class DataView : UserControl
    {
        private IDataListViewModel pid;

        public DataView(IDataListViewModel pid)
        {
            this.pid = pid;
            this.DataContext = this.pid;
            this.InitializeComponent();            
        }     
    }
}

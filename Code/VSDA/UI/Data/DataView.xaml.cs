using System.ComponentModel;
using VSDACore.Modules.Data;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

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
            this.pid.PropertyChanged += this.RaiseViewModelPropertyChanged;
        }
        
        private void RaiseViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "CurrentValue")
            {                
                switch(this.pid.DataItems[this.pid.CurrentSample].Type)
                {                    
                    case ValueType.Caution:
                        this.PidName.Foreground = (App.Current.Resources["GraphPlotColorCaution"] as SolidColorBrush);
                        this.PidValue.Foreground = (App.Current.Resources["GraphPlotColorCaution"] as SolidColorBrush);
                        break;
                    case ValueType.Danger:
                        this.PidName.Foreground = (App.Current.Resources["GraphPlotColorDanger"] as SolidColorBrush);
                        this.PidValue.Foreground = (App.Current.Resources["GraphPlotColorDanger"] as SolidColorBrush);
                        break;
                    case ValueType.Normal:
                    case ValueType.Default:
                        this.PidName.Foreground = (App.Current.Resources["ModulePageButtonForeground"] as SolidColorBrush);
                        this.PidValue.Foreground = (App.Current.Resources["ModulePageButtonForeground"] as SolidColorBrush);
                        break;
                }
            }
        }
    }
}

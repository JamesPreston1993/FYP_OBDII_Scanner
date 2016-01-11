using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using VSDA.Communication.Data;
using System.ComponentModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace VSDA.UI
{
    public sealed partial class DataGraph : UserControl
    {
        private IDataGraphViewModel pidViewModel;

        public DataGraph(IDataGraphViewModel pid)
        {
            this.pidViewModel = pid;
            this.DataContext = this.pidViewModel;            
            this.InitializeComponent();
            
            // Temp
            this.Loaded += delegate
            {
                ((DataGraphViewModel)this.pidViewModel).GraphHeight = this.GraphArea.ActualHeight;
                ((DataGraphViewModel)this.pidViewModel).GraphWidth = this.GraphArea.ActualWidth;                
            };
            this.pidViewModel.PropertyChanged += this.ScrollToPosition;
        }

        public void ScrollToPosition(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "CursorPosition")
            {
                this.Scroller.ChangeView(this.pidViewModel.CursorPosition, null, null);
            }
        }
    }
}

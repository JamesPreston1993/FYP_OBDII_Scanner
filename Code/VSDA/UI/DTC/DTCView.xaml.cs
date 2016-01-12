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
using VSDA.Communication;
using VSDA.Communication.DTC;

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

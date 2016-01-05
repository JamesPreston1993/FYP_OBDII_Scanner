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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace VSDA.UI
{
    public sealed partial class DataView : UserControl
    {
        private IPid pid;

        public DataView(IPid pid)
        {
            this.pid = pid;
            this.InitializeComponent();
            this.PidName.Text = this.pid.Name;
            //this.PidValue.Text = this.pid.DataItems[this.pid.DataItems.Count - 1];
        }

        public string PidValueText
        {            
            set
            {
                this.PidValue.Text = value;
            }
        }
    }
}

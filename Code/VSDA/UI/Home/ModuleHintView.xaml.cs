using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VSDACore.Modules.Base;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace VSDA.UI
{
    public sealed partial class ModuleHintView : UserControl
    {
        public ModuleHintView(IHelpItem helpItem)
        {
            this.DataContext = helpItem;
            this.InitializeComponent();

            switch (helpItem.Title)
            {
                case "Codes": this.ModuleIcon.Text = "\xE814"; break;
                case "Data": this.ModuleIcon.Text = "\xE877"; break;
                case "Connection": this.ModuleIcon.Text = "\xE702"; break;
                case "Email": this.ModuleIcon.Text = "\xE715"; break;
                case "Help": this.ModuleIcon.Text = "\xE897"; break;
            }
        }
    }
}

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
using VSDA.Communication.Data;
using System.ComponentModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VSDA.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HostView : Page
    {
        private IHostViewModel host;

        public HostView(IHostViewModel host)
        {            
            this.host = host;
            this.DataContext = this.host;
            this.InitializeComponent();
            this.host.PropertyChanged += this.RaiseCurrentViewModelChanged;

            // Hack
            this.host.CurrentModule = this.host.CurrentModule;           
        }
        
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            this.SideMenu.IsPaneOpen = !this.SideMenu.IsPaneOpen;
        }
        
        public void RaiseCurrentViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "CurrentModule")
            {
                switch(this.host.CurrentModuleName)
                {
                    case "Data":
                        this.ModulePage.Children.Clear();
                        this.ModulePage.Children.Add(new DataPage(this.host.CurrentModule as IDataModuleViewModel));
                        break;
                }
            }
        }               
    }
}

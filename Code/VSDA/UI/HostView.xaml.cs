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
using VSDA.Communication.DTC;
using Windows.UI;

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

            foreach(IModuleViewModel module in this.host.Modules)
            {
                Grid grid = new Grid(){ HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch};
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                
                TextBlock icon = new TextBlock()
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    FontSize = 24,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 50,
                    Foreground = App.Current.Resources["ModuleBarForeground"] as SolidColorBrush
                };
                switch(module.Name)
                {
                    case "Codes": icon.Text = "\xE814"; break;
                    case "Data": icon.Text = "\xE877"; break;
                    case "Connection": icon.Text = "\xE702"; break;
                }
                TextBlock moduleName = new TextBlock()
                {
                    Text = module.Name,
                    FontSize = 18,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = App.Current.Resources["ModuleBarForeground"] as SolidColorBrush
                };
                Grid.SetColumn(icon, 0);
                grid.Children.Add(icon);
                Grid.SetColumn(moduleName, 1);
                grid.Children.Add(moduleName);
                
                Button button = new Button()
                {
                    Height = 50,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Background = new SolidColorBrush(Colors.Transparent),
                    Content = grid,                    
                    Command = new RelayCommand(delegate
                    {
                        this.host.CurrentModule = module;
                        this.SideMenu.IsPaneOpen = false;
                    })
                };
                this.ModuleIcons.Children.Add(button);
            }

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
                    case "Codes":
                        this.ModulePage.Children.Clear();
                        this.ModulePage.Children.Add(new DTCPage(this.host.CurrentModule as IDtcModuleViewModel));
                        break;

                    case "Connection":
                        this.ModulePage.Children.Clear();
                        this.ModulePage.Children.Add(new ConnectionPage(this.host.CurrentModule as IConnectionModuleViewModel));
                        break;
                }
            }
        }               
    }
}

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.ComponentModel;
using Windows.UI;
using VSDACore.Host;
using VSDACore.Modules.Base;
using VSDACore.Modules.Data;
using VSDACore.Modules.Codes;
using VSDACore.Modules.Connection;
using VSDACore.Connection;
using System;
using Windows.UI.Popups;
using Windows.UI.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VSDA.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HostView : Page
    {
        private IHostViewModel host;
        private bool showHelp;        

        public HostView(IHostViewModel host)
        {            
            this.host = host;
            this.showHelp = false;            
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
                };
                button.Command = new RelayCommand(delegate
                                                    {
                                                        this.host.CurrentModule = module;
                                                        this.SideMenu.IsPaneOpen = false;
                                                    },
                                                    new Func<bool>(() => ConnectionManager.Instance.IsInitialized));

                this.ModuleIcons.Children.Add(button);
            }
            this.HelpView.Width = 0;

            // Hack
            this.host.CurrentModule = this.host.CurrentModule;           
        }        

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            this.SideMenu.IsPaneOpen = !this.SideMenu.IsPaneOpen;
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            this.showHelp = !this.showHelp;

            switch (this.showHelp)
            {
                case true: this.HelpView.Width = 360; break;
                case false: this.HelpView.Width = 0; break;
            }
        }

        private void SetHelpViewItems()
        {
            this.HelpView.Children.Clear();

            foreach(IHelpItem helpItem in this.host.CurrentModule.HelpItems)
            {
                TextBlock titleBlock = new TextBlock()
                {
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    Text = helpItem.Title,
                    Foreground = App.Current.Resources["ModulePageForeground"] as SolidColorBrush,
                    Margin = new Thickness(10, 10, 10, 10),
                    TextWrapping = TextWrapping.WrapWholeWords
                };
                TextBlock descBlock = new TextBlock()
                {
                    FontSize = 18,
                    Text = helpItem.Description,
                    Foreground = App.Current.Resources["ModulePageForeground"] as SolidColorBrush,
                    Margin = new Thickness(10, 10, 10, 20),                    
                    TextWrapping = TextWrapping.WrapWholeWords
                };
                this.HelpView.Children.Add(titleBlock);
                this.HelpView.Children.Add(descBlock);
            }
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
                this.SetHelpViewItems();
                this.HelpView.Width = 0;
                this.showHelp = false;
            }
            else if(e.PropertyName == "IsInitialized")
            {
                foreach(Button button in this.ModuleIcons.Children)
                {
                    (button.Command as RelayCommand).RaiseCanExecuteChanged();
                }
                if(ConnectionManager.Instance.IsInitialized == false)
                {
                    MessageDialog dialog = new MessageDialog("Bluetooth connection was lost... Please reconnect");
                    dialog.ShowAsync();
                }
            }
        }        
    }
}

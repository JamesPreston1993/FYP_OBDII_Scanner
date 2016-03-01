using System.Collections.Generic;
using System.ComponentModel;
using VSDACore.Connection;
using VSDACore.Modules.Base;
using VSDACore.Modules.Connection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VSDA.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConnectionPage : Page
    {
        private IConnectionModuleViewModel module;

        private IList<Button> deviceButtons;

        public ConnectionPage(IConnectionModuleViewModel module)
        {
            this.module = module;
            this.DataContext = module;
            this.InitializeComponent();
            this.deviceButtons = new List<Button>();
            this.module.PropertyChanged += this.RaiseViewModelChanged;
            this.Loaded += this.PageLoaded;
        }

        public async void PageLoaded(object sender, RoutedEventArgs e)
        {
            await this.module.InitializeModule();

            this.PopulateDeviceList();

            this.RefreshButton.Click += delegate
            {
                this.module.InitializeModule();
            };
        }

        private void PopulateDeviceList()
        {
            this.deviceButtons.Clear();
            this.DevicesPanel.Children.Clear();

            foreach (IDevice device in this.module.Devices)
            {
                Button block = new Button()
                {
                    Content = device.Name,
                    FontSize = 18,
                    Height = 50,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    Background = App.Current.Resources["ModulePageButtonBackground"] as SolidColorBrush,
                    Foreground = App.Current.Resources["ModulePageButtonForeground"] as SolidColorBrush,
                    Command = new RelayCommand(delegate
                    {
                        this.module.CurrentDevice = device;
                        this.module.ConnectCommand.Execute(null);
                    })

                };
                this.deviceButtons.Add(block);
                this.DevicesPanel.Children.Add(block);
            }
        }

        public void RaiseViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Devices")
            {
                this.PopulateDeviceList();
            }
            else if (e.PropertyName == "DeviceConnectionStatus")
            {
                if (module.DeviceConnectionStatus == "Connecting")
                {
                    this.ConnectingProgress.IsActive = true;
                    if (this.deviceButtons != null)
                    {
                        foreach (Button button in this.deviceButtons)
                        {
                            button.IsEnabled = false;                            
                        }
                    }
                    if(this.RefreshButton != null)
                    {
                        this.RefreshButton.IsEnabled = false;
                    }
                }
                else
                {
                    this.ConnectingProgress.IsActive = false;
                    if (this.deviceButtons != null)
                    {
                        foreach (Button button in this.deviceButtons)
                        {
                            button.IsEnabled = true;
                        }
                    }
                    if (this.RefreshButton != null)
                    {
                        this.RefreshButton.IsEnabled = true;
                    }
                }
            }
            else if (e.PropertyName == "CommunicationLog")
            {
                this.CommunicationLog.Text += string.Format("{0}\n", this.module.CommunicationLog);
            }
        }
    }
}

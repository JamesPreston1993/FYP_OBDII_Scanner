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

        public ConnectionPage(IConnectionModuleViewModel module)
        {
            this.module = module;
            this.DataContext = module;
            this.InitializeComponent();
            this.module.PropertyChanged += this.RaiseViewModelChanged;
            this.Loaded += this.PageLoaded;
        }

        public async void PageLoaded(object sender, RoutedEventArgs e)
        {
            await this.module.InitializeModule();

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
                    })

                };
                this.DevicesPanel.Children.Add(block);
            }
        }

        public void RaiseViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DeviceConnectionStatus")
            {
                if (module.DeviceConnectionStatus == "Connecting")
                {
                    this.ConnectingProgress.IsActive = true;
                }
                else
                {
                    this.ConnectingProgress.IsActive = false;
                }
            }

        }
    }
}

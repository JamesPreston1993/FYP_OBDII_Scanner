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
using VSDA.Communication.DTC;
using System.ComponentModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VSDA.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DTCPage : Page
    {
        private IDtcModuleViewModel module;

        public DTCPage(IDtcModuleViewModel module)
        {
            this.module = module;
            this.DataContext = this.module;
            this.InitializeComponent();
            this.Loaded += this.PageLoaded;
        }

        public async void PageLoaded(object sender, RoutedEventArgs e)
        {
            await this.module.InitializeModule();

            foreach (ICodeViewModel code in this.module.CurrentCodes)
                this.CurrentCodesControl.Children.Add(new DTCView(code));

            foreach (ICodeViewModel code in this.module.PendingCodes)
                this.PendingCodesControl.Children.Add(new DTCView(code));

            foreach (ICodeViewModel code in this.module.PermanentCodes)
                this.PermanentCodesControl.Children.Add(new DTCView(code));
        }

        public void RaiseViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentCodes")
            {
                this.CurrentCodesControl.Children.Clear();

                foreach (ICodeViewModel code in this.module.CurrentCodes)
                    this.CurrentCodesControl.Children.Add(new DTCView(code));
            }
        }
    }
}

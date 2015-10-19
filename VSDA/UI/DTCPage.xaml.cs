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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VSDA.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DTCPage : Page
    {
        private IDtcModule module;

        public DTCPage(IDtcModule module)
        {
            this.module = module;
            this.InitializeComponent();
            this.DisplayCodes();
        }

        private async void DisplayCodes()
        {
            this.StoredCodesGrid.Children.Clear();
            this.PendingCodesGrid.Children.Clear();
            this.PermanentCodesGrid.Children.Clear();

            IList<ICode> storedCodes = await this.module.GetCurrentCodes();
            foreach(ICode code in storedCodes)
            {
                DTCView view = new DTCView(code);
                view.Height = 80;
                this.StoredCodesGrid.Children.Add(view);
            }

            IList<ICode> pendingCodes = await this.module.GetPendingCodes();
            foreach (ICode code in pendingCodes)
            {
                DTCView view = new DTCView(code);
                view.Height = 80;
                this.PendingCodesGrid.Children.Add(view);
            }
            
            IList<ICode> permanentCodes = await this.module.GetPermanentCodes();
            foreach (ICode code in permanentCodes)
            {
                DTCView view = new DTCView(code);
                view.Height = 80;
                this.PermanentCodesGrid.Children.Add(view);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.DisplayCodes();
        }
    }
}

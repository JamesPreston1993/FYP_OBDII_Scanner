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
    public sealed partial class DataPage : Page
    {
        private IDataModule module;

        public DataPage(IDataModule module)
        {
            this.module = module;
            this.InitializeComponent();
            this.module.GetSupportedPids();            
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
           // this.DataList.Children.Clear();

            await this.module.GetSupportedPids();            

            for (int i = 0; i < this.module.Pids.Count; i++)
            {
                IPid pid = this.module.Pids[i];
                DataView dataView = new DataView(pid);
                RowDefinition rDef = new RowDefinition();
                this.DataList.RowDefinitions.Add(rDef);
                Grid.SetRow(dataView, i) ;
                this.DataList.Children.Add(dataView);
            }

            int frame = 0;
            this.Frame.Text = frame.ToString();

            while(true)
            {
                for (int i = 0; i < this.module.Pids.Count; i++)
                {                    
                    IPid pid = this.module.Pids[i];
                    await this.module.UpdateData(pid);
                    ((DataView)this.DataList.Children[i]).PidValueText = pid.DataItems[pid.DataItems.Count - 1];
                }
                
                this.Frame.Text = frame.ToString();
                frame++;
            }
        }
    }
}

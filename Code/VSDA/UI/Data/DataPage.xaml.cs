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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VSDA.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataPage : Page
    {
        private IDataModuleViewModel module;        

        public DataPage(IDataModuleViewModel module)
        {
            this.module = module;            
            this.DataContext = this.module;
            this.InitializeComponent();
            
            this.Loaded += this.PageLoaded;            
        }
                
        public async void PageLoaded(object sender, RoutedEventArgs e)
        {
            /* TEMP */
            //await this.module.ModuleModel.GetSupportedPids();
            await ((DataModuleViewModel)this.module).InitializeModule();
            /* TEMP */
            double graphHeight = this.Graph.ActualHeight / 2;
            int index = 0;
            foreach (IDataListViewModel listViewModel in this.module.ListViews)
            {
                // Create List View
                DataView dataView = new DataView(listViewModel);
                RowDefinition rowDef = new RowDefinition();
                this.DataList.RowDefinitions.Add(rowDef);
                Grid.SetRow(dataView, index);
                this.DataList.Children.Add(dataView);
                index++;
            }
            index = 0;
            foreach (IDataGraphViewModel graphViewModel in this.module.GraphViews)
            {
                // Create Graph View
                DataGraph dataGraph = new DataGraph(graphViewModel);
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(graphHeight);
                this.Graph.RowDefinitions.Add(rowDef);

                Grid.SetRow(dataGraph, index / 2);
                Grid.SetColumn(dataGraph, index % 2);
                this.Graph.Children.Add(dataGraph);
                index++;
            }
            this.Graph.Height = graphHeight * (this.module.GraphViews.Count / 2 + this.module.GraphViews.Count % 2);
        }        
    }
}

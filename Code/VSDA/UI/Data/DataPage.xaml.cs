using VSDACore.Modules.Base;
using VSDACore.Modules.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            await this.module.InitializeModule();
            
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

                // Workaround for C# issue: Create a copy of index for lambda expression   
                int i = index;
                dataView.JumpToGraphCommand = new RelayCommand(() =>
                {   
                    this.GraphScrollViewer.ChangeView(null, graphHeight * (i / 2), null);
                });
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

        public void ExpandCollapseListClick(object sender, RoutedEventArgs e)
        {
            if(this.MainPage.ColumnDefinitions[0].Width.Value == 0)
            {
                this.MainPage.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                this.ExpandCollapseList.Content = "\xE00E";
            }
            else
            {
                this.MainPage.ColumnDefinitions[0].Width = new GridLength(0);
                this.ExpandCollapseList.Content = "\xE00F";
            }
        }
    }
}

using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using System.ComponentModel;
using VSDACore.Modules.Data;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace VSDA.UI
{
    public sealed partial class DataGraph : UserControl
    {
        private IDataGraphViewModel pidViewModel;

        // Graph Drawing
        private PointCollection points;
        public PointCollection Points
        {
            get
            {
                return this.points;
            }
            set
            {
                this.points = value;
            }
        }
        private double graphHeight;
        private double graphWidth;
        private double yAxisRange;
        private double xAxisScale;

        public DataGraph(IDataGraphViewModel pid)
        {
            this.pidViewModel = pid;
            this.DataContext = this.pidViewModel;            
            this.InitializeComponent();
            this.Points = new PointCollection();

            Binding pointsBinding = new Binding();
            pointsBinding.Source = this.Points;
            this.GraphPlot.SetBinding(Polyline.PointsProperty, pointsBinding);

            this.Loaded += this.GraphLoaded;
            this.pidViewModel.PropertyChanged += this.RaiseViewModelPropertyChanged;
        }

        public void GraphLoaded(object sender, RoutedEventArgs e)
        {
            this.graphHeight = this.GraphArea.ActualHeight;
            this.graphWidth = this.GraphArea.ActualWidth;
            this.yAxisRange = this.pidViewModel.MaxPossibleValue - this.pidViewModel.MinPossibleValue;
            this.xAxisScale = this.graphWidth / 10;
            this.Cursor.Y2 = this.graphHeight;
        }

        public void RaiseViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "CursorPosition")
            {
                this.Cursor.X1 = this.Cursor.X2 = this.pidViewModel.CursorPosition * this.xAxisScale;
                this.Scroller.ChangeView(this.pidViewModel.CursorPosition * this.xAxisScale, null, null);
            }
            else if(e.PropertyName == "DataItems")
            {
                // Add point to graph
                try
                {
                    Point point = new Point(this.pidViewModel.CurrentSample * this.xAxisScale,
                                            this.graphHeight - (this.graphHeight * (Double.Parse(this.pidViewModel.DataItems.Last()) - this.pidViewModel.MinPossibleValue) / this.yAxisRange));
                    this.Points.Add(point);
                }
                catch (FormatException) { }
            }            
        }
    }
}

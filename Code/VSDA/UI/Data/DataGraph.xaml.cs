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
using Windows.UI;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace VSDA.UI
{
    public sealed partial class DataGraph : UserControl
    {
        private IDataGraphViewModel pidViewModel;

        // Graph Drawing
        private Color previousColor;
        private Point previousPoint;
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

            this.previousPoint = new Point(-1, 0);
            this.Points = new PointCollection();

            this.Loaded += this.GraphLoaded;
            this.pidViewModel.PropertyChanged += this.RaiseViewModelPropertyChanged;
            this.GraphArea.SizeChanged += delegate
            {
                this.Scroller.ChangeView(double.MaxValue, null, null);
            };
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
                    IDataItem currentItem = this.pidViewModel.DataItems.Last();
                    
                    Point point = new Point(this.pidViewModel.CurrentSample * this.xAxisScale,
                                            this.graphHeight - (this.graphHeight * (currentItem.Value - this.pidViewModel.MinPossibleValue) / this.yAxisRange));
                    this.Points.Add(point);

                    Color currentColor;

                    if (this.previousPoint.X >= 0)
                    {
                        Line line = new Line()
                        {
                            X1 = this.previousPoint.X,
                            Y1 = this.previousPoint.Y,
                            X2 = point.X,
                            Y2 = point.Y,                            
                            StrokeThickness = 2
                        };
                        
                        if (currentItem.Type == VSDACore.Modules.Data.ValueType.Normal)
                        {
                            currentColor = (App.Current.Resources["GraphPlotColorNormal"] as SolidColorBrush).Color;
                        }
                        else if (currentItem.Type == VSDACore.Modules.Data.ValueType.Caution)
                        {
                            currentColor = (App.Current.Resources["GraphPlotColorCaution"] as SolidColorBrush).Color;
                        }
                        else if (currentItem.Type == VSDACore.Modules.Data.ValueType.Danger)
                        {
                            currentColor = (App.Current.Resources["GraphPlotColorDanger"] as SolidColorBrush).Color;
                        }
                        
                        GradientStopCollection gradients = new GradientStopCollection();
                        if (previousPoint.Y > point.Y)
                        {
                            gradients.Add(new GradientStop() { Color = currentColor, Offset = 0 });
                            gradients.Add(new GradientStop() { Color = this.previousColor, Offset = 1 });
                        }
                        else
                        {
                            gradients.Add(new GradientStop() { Color = this.previousColor, Offset = 0 });
                            gradients.Add(new GradientStop() { Color = currentColor, Offset = 1 });
                        }

                        line.Stroke = new LinearGradientBrush() { GradientStops = gradients };
                        this.GraphArea.Children.Add(line);
                    }         
                    else
                    {
                        switch (currentItem.Type)
                        {
                            case VSDACore.Modules.Data.ValueType.Normal: currentColor = (App.Current.Resources["GraphPlotColorNormal"] as SolidColorBrush).Color; break;
                            case VSDACore.Modules.Data.ValueType.Caution: currentColor = (App.Current.Resources["GraphPlotColorCaution"] as SolidColorBrush).Color; break;
                            case VSDACore.Modules.Data.ValueType.Danger: currentColor = (App.Current.Resources["GraphPlotColorDanger"] as SolidColorBrush).Color; break;
                        }
                    }                               
                    this.previousPoint = point;
                    this.previousColor = currentColor;
                }
                catch (FormatException) { }
                catch (ArgumentException) { }
            }            
        }
    }
}

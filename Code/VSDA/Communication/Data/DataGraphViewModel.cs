using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace VSDA.Communication.Data
{
    public class DataGraphViewModel : IDataGraphViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IPid PidModel { get; private set; }

        public string PidName { get; private set; }

        private int currentSample;
        public int CurrentSample
        {
            get
            {
                return this.currentSample;
            }
            set
            {
                this.currentSample = value;
                this.CursorPosition = this.currentSample * this.xScale;
                this.RaisePropertyChanged("CurrentSample");
            }
        }

        public IList<string> DataItems { get; private set; }       

        private double maxValue;
        public double MaxPossibleValue
        {
            get
            {
                return this.maxValue;
            }
            private set
            {
                this.maxValue = value;
                this.RaisePropertyChanged("MaxPossibleValue");
            }
        }

        private double minValue;
        public double MinPossibleValue
        {
            get
            {
                return this.minValue;
            }
            private set
            {
                this.minValue = value;
                this.RaisePropertyChanged("MinPossibleValue");
            }
        }

        // Graph Related       
        public double GraphHeight
        {
            get;
            set;
        }

        /* Collection of Points */
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
                this.RaisePropertyChanged("Points");
            }
        }        

        private double cursorPosition;
        public double CursorPosition
        {
            get
            {
                return this.cursorPosition;
            }
            private set
            {
                this.cursorPosition = value;
                this.RaisePropertyChanged("CursorPosition");
            }
        }
        private double graphRange;
        private double xScale;

        public DataGraphViewModel(IPid pid)
        {
            this.PidModel = pid;
            this.PidName = pid.Name;
            this.currentSample = 0;
            this.MaxPossibleValue = pid.MaxPossibleValue;
            this.MinPossibleValue = pid.MinPossibleValue;
            this.DataItems = pid.DataItems;
            this.graphRange = this.PidModel.MaxPossibleValue - this.PidModel.MinPossibleValue;
            this.xScale = 20;
            this.points = new PointCollection();
            this.PidModel.PropertyChanged += this.RaiseModelPropertyChanged;
        }

        public void StepBack()
        {
            if (this.currentSample > 0)
            {
                this.CurrentSample -= 1;
            }
        }

        public void StepForward()
        {
            if (this.currentSample < this.DataItems.Count - 1)
            {
                this.CurrentSample += 1;
            }
        }

        public void SkipToStart()
        {
            if(this.currentSample > 0)
            {
                this.CurrentSample = 0;
            }
        }

        public void SkipToEnd()
        {
            if (this.currentSample < this.DataItems.Count - 1)
            {
                this.CurrentSample = this.DataItems.Count - 1;
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void RaiseModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DataItems")
            {
                this.RaisePropertyChanged("DataItems");
                this.CurrentSample = this.DataItems.Count - 1;
                // Add points, Update cursor and scroll
                //this.CursorPosition = this.ScrollPosition = this.CurrentSample * this.xScale;
                //this.ScrollPosition += this.xScale;
                this.Points.Add(new Point(this.CurrentSample * this.xScale,
                                          this.GraphHeight - (this.GraphHeight * (Double.Parse(this.DataItems.Last()) - this.MinPossibleValue) / this.graphRange)));
            }
        }        
    }    
}

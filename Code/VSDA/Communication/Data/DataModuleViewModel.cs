using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VSDA.Communication.Data
{
    public class DataModuleViewModel : IDataModuleViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IDataModule ModuleModel { get; private set; }

        public IList<IDataListViewModel> ListViews { get; private set; }

        public IList<IDataGraphViewModel> GraphViews { get; private set; }

        // Commands
        public ICommand PlayPauseCommand { get; private set; }
        public ICommand StepBackCommand { get; private set; }
        public ICommand StepForwardCommand { get; private set; }
        public ICommand SkipToStartCommand { get; private set; }
        public ICommand SkipToEndCommand { get; private set; }

        public DataModuleViewModel(IDataModule module)
        {
            this.ModuleModel = module;
            this.ListViews = new List<IDataListViewModel>();
            this.GraphViews = new List<IDataGraphViewModel>();
            this.PlayPauseCommand = new RelayCommand(this.PlayPause);
            this.StepBackCommand = new RelayCommand(this.StepBack);
            this.StepForwardCommand = new RelayCommand(this.StepForward);
            this.SkipToEndCommand = new RelayCommand(this.SkipToEnd);
            this.SkipToStartCommand = new RelayCommand(this.SkipToStart);

            foreach (IPid pid in module.Pids)
            {
                this.ListViews.Add(new DataListViewModel(pid));
                this.GraphViews.Add(new DataGraphViewModel(pid));
            }
            this.ModuleModel.PropertyChanged += this.RaiseModelPropertyChanged;
        }

        public async Task<bool> InitializeModule()
        {
            bool val = await this.ModuleModel.Initialize();
            return val;
        }

        // Graph Controls
        public async void PlayPause()
        {
            this.ModuleModel.IsRecording = !this.ModuleModel.IsRecording;
            await this.ModuleModel.UpdateData();
        }
        
        public void StepBack()
        {
            
            if (!this.ModuleModel.IsRecording)
            {
                foreach(DataGraphViewModel graph in this.GraphViews)
                {
                    graph.StepBack();
                }
                foreach(DataListViewModel list in this.ListViews)
                {
                    list.StepBack();
                }
            }
        }

        public void StepForward()
        {
            if (!this.ModuleModel.IsRecording)
            {
                foreach (DataGraphViewModel graph in this.GraphViews)
                {
                    graph.StepForward();
                }
                foreach (DataListViewModel list in this.ListViews)
                {
                    list.StepForward();
                }
            }
        }

        public void SkipToStart()
        {
            if (!this.ModuleModel.IsRecording)
            {
                foreach (DataGraphViewModel graph in this.GraphViews)
                {
                    graph.SkipToStart();
                }
                foreach (DataListViewModel list in this.ListViews)
                {
                    list.SkipToStart();
                }
            }
        }

        public void SkipToEnd()
        {
            if (!this.ModuleModel.IsRecording)
            {
                foreach (DataGraphViewModel graph in this.GraphViews)
                {
                    graph.SkipToEnd();
                }
                foreach (DataListViewModel list in this.ListViews)
                {
                    list.SkipToEnd();
                }
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
            if (e.PropertyName == "Pids")
            {
                this.RaisePropertyChanged("ListViews");
                this.RaisePropertyChanged("GraphViews");

                this.ListViews.Clear();
                this.GraphViews.Clear();
                foreach (IPid pid in this.ModuleModel.Pids)
                {
                    this.ListViews.Add(new DataListViewModel(pid));
                    this.GraphViews.Add(new DataGraphViewModel(pid));
                }
            }
        }
    }
}

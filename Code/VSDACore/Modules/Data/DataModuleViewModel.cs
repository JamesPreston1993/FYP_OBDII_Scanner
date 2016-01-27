using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VSDACore.Modules.Base;

namespace VSDACore.Modules.Data
{
    public class DataModuleViewModel : IDataModuleViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; private set; }

        private IDataModule dataModuleModel;
        public IModule ModuleModel { get; set; }

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
            this.dataModuleModel = module;
            this.Name = module.Name;
            this.ListViews = new List<IDataListViewModel>();
            this.GraphViews = new List<IDataGraphViewModel>();
            this.PlayPauseCommand = new RelayCommand(this.PlayPause);
            this.StepBackCommand = new RelayCommand(this.StepBack, this.CanStep);
            this.StepForwardCommand = new RelayCommand(this.StepForward, this.CanStep);
            this.SkipToEndCommand = new RelayCommand(this.SkipToEnd, this.CanStep);
            this.SkipToStartCommand = new RelayCommand(this.SkipToStart, this.CanStep);

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
        private async void PlayPause()
        {
            this.dataModuleModel.IsRecording = !this.dataModuleModel.IsRecording;
            await this.dataModuleModel.UpdateData();
        }

        private bool CanStep()
        {
            return !this.dataModuleModel.IsRecording;
        }

        private void StepBack()
        {
            //if (!this.dataModuleModel.IsRecording)
            //{
                foreach (DataGraphViewModel graph in this.GraphViews)
                {
                    graph.StepBack();
                }
                foreach (DataListViewModel list in this.ListViews)
                {
                    list.StepBack();
                }
            //}
        }

        private void StepForward()
        {
            if (!this.dataModuleModel.IsRecording)
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

        private void SkipToStart()
        {
            if (!this.dataModuleModel.IsRecording)
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

        private void SkipToEnd()
        {
            if (!this.dataModuleModel.IsRecording)
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

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void RaiseModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Pids")
            {
                this.RaisePropertyChanged("ListViews");
                this.RaisePropertyChanged("GraphViews");

                this.ListViews.Clear();
                this.GraphViews.Clear();
                foreach (IPid pid in this.dataModuleModel.Pids)
                {
                    this.ListViews.Add(new DataListViewModel(pid));
                    this.GraphViews.Add(new DataGraphViewModel(pid));
                }
            }
            else if(e.PropertyName == "IsRecording")
            {                
                (this.StepBackCommand as RelayCommand).RaiseCanExecuteChanged();
                (this.StepForwardCommand as RelayCommand).RaiseCanExecuteChanged();
                (this.SkipToEndCommand as RelayCommand).RaiseCanExecuteChanged();
                (this.SkipToStartCommand as RelayCommand).RaiseCanExecuteChanged();
            }
        }
    }
}

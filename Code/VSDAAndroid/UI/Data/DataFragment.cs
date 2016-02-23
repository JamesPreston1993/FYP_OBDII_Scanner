using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using VSDACore.Modules.Data;
using System.ComponentModel;

namespace VSDAAndroid.UI.Data
{
    public class DataFragment : Fragment
    {
        private IDataModuleViewModel module;

        // UI
        private LinearLayout dataItemsLayout;

        public DataFragment(IDataModuleViewModel module)
        {
            this.module = module;
            this.module.PropertyChanged += this.RaiseViewModelChanged;
        }

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            await this.module.InitializeModule();
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.DataFragment, container, false);

            this.dataItemsLayout = view.FindViewById<LinearLayout>(Resource.Id.DataItemsLinearLayout);

            Button b = view.FindViewById<Button>(Resource.Id.PlayPauseButton);
            b.Click += delegate
            {
                ((DataModuleViewModel)this.module).PlayPauseCommand.Execute(null);
            };
            
            this.PopulateDataViews();

            return view;
            
        }

        private void PopulateDataViews()
        {
            if(this.dataItemsLayout != null)
            {
                this.dataItemsLayout.RemoveAllViews();
                foreach (IDataListViewModel dataList in this.module.ListViews)
                {
                    DataListView dataView = new DataListView(Activity.ApplicationContext, dataList);
                    this.dataItemsLayout.AddView(dataView);
                }
            }
        }

        private void RaiseViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PopulateDataViews();
        }
    }
}
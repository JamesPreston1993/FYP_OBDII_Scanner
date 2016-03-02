using System.ComponentModel;
using Android.App;
using Android.OS;
using VSDACore.Host;
using Android.Widget;
using Android.Support.V7.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Widget;
using Android.Views;
using VSDACore.Modules.Base;
using VSDACore.Connection;
using System.Collections.Generic;
using VSDAAndroid.UI.Codes;
using VSDACore.Modules.Codes;
using VSDAAndroid.UI.Connection;
using VSDACore.Modules.Connection;
using VSDAAndroid.UI.Data;
using VSDACore.Modules.Data;

namespace VSDAAndroid.UI.Host
{
    [Activity(Label = "VSDA", MainLauncher = true, Theme = "@style/MyDarkTheme")]
    public class HostActivity : ActionBarActivity
    {
        // UI
        private SupportToolbar toolbar;
        private HostModuleListToggle toggle;
        private DrawerLayout drawerLayout;
        private ListView moduleListView;
        private ListView helpItemsListView;
        private FrameLayout modulePanel;

        // ViewModel
        private IHostViewModel host;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AppRoot.Initialize();

            this.host = new HostViewModel(AppRoot.Instance.Host);
            this.host.PropertyChanged += this.RaiseViewModelPropertyChanged;

            // Setup View
            this.SetContentView(Resource.Layout.HostLayout);
            this.toolbar = this.FindViewById<SupportToolbar>(Resource.Id.HostToolBar);
            this.drawerLayout = this.FindViewById<DrawerLayout>(Resource.Id.Drawer);
            this.moduleListView = this.FindViewById<ListView>(Resource.Id.ModuleListView);
            this.helpItemsListView = this.FindViewById<ListView>(Resource.Id.HelpItemsListView);

            // Tags
            this.moduleListView.Tag = 0;
            this.helpItemsListView.Tag = 1;


            // Hamburger Menu
            this.moduleListView.Adapter = new ModuleListViewAdapter(this.ApplicationContext, this.host);
            this.helpItemsListView.Adapter = new HelpItemsListViewAdapter(this.ApplicationContext, this.host);

            // Module Panel
            this.modulePanel = this.FindViewById<FrameLayout>(Resource.Id.CurrentModulePanel);
            this.PopulateModulePanel();

            // Toolbar
            this.SetSupportActionBar(this.toolbar);
            this.toggle = new HostModuleListToggle(this, this.drawerLayout, Resource.String.Empty, Resource.String.Empty);
            this.drawerLayout.SetDrawerListener(this.toggle);
            
            this.SupportActionBar.SetHomeButtonEnabled(true);
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            this.SupportActionBar.Title = this.host.CurrentModuleName;
            this.toggle.SyncState();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Android.Resource.Id.Home:
                    this.drawerLayout.CloseDrawer(this.helpItemsListView);
                    this.toggle.OnOptionsItemSelected(item);
                    return true;

                case Resource.Id.ActionHelp:
                    if(this.drawerLayout.IsDrawerOpen(this.helpItemsListView))
                    {
                        this.drawerLayout.CloseDrawer(this.helpItemsListView);
                    }
                    else
                    {                        
                        this.drawerLayout.OpenDrawer(this.helpItemsListView);
                        this.drawerLayout.CloseDrawer(this.moduleListView);
                    }
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.ActionMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void PopulateModulePanel()
        {
            this.modulePanel.RemoveAllViews();
            Fragment fragment = null;
            switch (this.host.CurrentModuleName)
            {
                case "Codes": fragment = new CodesFragment(this.host.CurrentModule as IDtcModuleViewModel); break;
                case "Data": fragment = new DataFragment(this.host.CurrentModule as IDataModuleViewModel); break;
                case "Connection": fragment = new ConnectionFragment(this.host.CurrentModule as IConnectionModuleViewModel); break;
            }

            if (fragment != null)
            {
                FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
                transaction.Add(Resource.Id.CurrentModulePanel, fragment).CommitAllowingStateLoss();
            }
        }

        private void RaiseViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentModule")
            {
                switch (this.host.CurrentModuleName)
                {
                    case "Codes":
                    case "Data":
                    case "Connection":
                        this.SupportActionBar.Title = this.host.CurrentModuleName;
                        this.PopulateModulePanel();
                        break;
                }
            }
            else if (e.PropertyName == "IsInitialized")
            {

            }
        }
    }
}
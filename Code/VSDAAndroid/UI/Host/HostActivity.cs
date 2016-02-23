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
        private DrawerLayout drawerLayout;
        private ListView leftDrawer;
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
            this.leftDrawer = this.FindViewById<ListView>(Resource.Id.ModuleListView);

            // Hamburger Menu
            this.leftDrawer.Adapter = new ModuleListViewAdapter(this.ApplicationContext, this.host);

            // Module Panel
            this.modulePanel = this.FindViewById<FrameLayout>(Resource.Id.CurrentModulePanel);
            this.PopulateModulePanel();

            // Toolbar
            this.SetSupportActionBar(this.toolbar);
            this.SupportActionBar.Title = this.host.CurrentModuleName;
            this.SupportActionBar.SetHomeButtonEnabled(true);

            //ConnectionManager.Instance.CurrentDevice = new BluetoothConnectionDevice("OBDII", "", "");
            //await ConnectionManager.Instance.Initialize();
            //await this.host.CurrentModule.InitializeModule(); //ConnectionManager.Instance.Initialize();
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
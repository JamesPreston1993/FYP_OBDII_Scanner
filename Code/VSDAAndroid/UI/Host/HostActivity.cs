using System.ComponentModel;
using Android.App;
using Android.OS;
using VSDACore.Host;
using Android.Widget;
using Android.Support.V7.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Widget;
using Android.Views;

namespace VSDAAndroid.UI.Host
{
    [Activity(Label = "VSDA", MainLauncher = true, Theme = "@style/MyDarkTheme")]
    public class HostActivity : ActionBarActivity
    {
        // UI
        private SupportToolbar toolbar;
        private DrawerLayout drawerLayout;
        private ListView leftDrawer;

        // ViewModel
        private IHostViewModel host;

        protected override void OnCreate(Bundle savedInstanceState)
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

            // Toolbar
            this.SetSupportActionBar(this.toolbar);
            this.SupportActionBar.Title = this.host.CurrentModuleName;
            this.SupportActionBar.SetHomeButtonEnabled(true);
        }

        private void RaiseViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentModule")
            {
                this.SupportActionBar.Title = this.host.CurrentModuleName;

                switch (this.host.CurrentModuleName)
                {
                    case "Codes": break;
                    case "Data": break;
                    case "Connection": break;
                }
            }
        }
    }
}
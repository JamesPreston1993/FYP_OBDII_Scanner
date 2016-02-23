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
using VSDACore.Modules.Connection;
using System.ComponentModel;
using VSDACore.Connection;
using Android.Graphics;

namespace VSDAAndroid.UI.Connection
{
    public class ConnectionFragment : Fragment
    {
        private IConnectionModuleViewModel module;

        // UI
        private LinearLayout devicesLinearLayout;
        private ProgressBar connectingSpinner;
        private TextView connectionStatusTextView;
        private Button refreshButton;
        private IList<DeviceView> deviceViews;

        public ConnectionFragment(IConnectionModuleViewModel module)
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
            //base.OnSaveInstanceState(outState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.ConnectionFragment, container, false);

            this.devicesLinearLayout = view.FindViewById<LinearLayout>(Resource.Id.DevicesLinearLayout);
            this.connectingSpinner = view.FindViewById<ProgressBar>(Resource.Id.ConnectingSpinner);
            this.connectionStatusTextView = view.FindViewById<TextView>(Resource.Id.ConnectionStatusTextView);
            this.refreshButton = view.FindViewById<Button>(Resource.Id.RefreshButton);
            this.deviceViews = new List<DeviceView>();

            this.connectionStatusTextView.Text = this.module.DeviceConnectionStatus;
            this.refreshButton.Click += delegate
            {
                this.module.InitializeModule();
            };
            this.PopulateDevices();
            
            return view;
        }

        private void SetDeviceButtonsEnabled(bool enabled)
        {
            foreach (DeviceView view in this.deviceViews)
            {
                view.Enabled = enabled;
                if(enabled)
                    view.SetTextColor(view.EnabledColor);
                else
                    view.SetTextColor(view.DisabledColor);
            }
        }

        private void PopulateDevices()
        {
            if(this.devicesLinearLayout != null)
            {
                this.deviceViews.Clear();
                this.devicesLinearLayout.RemoveAllViews();

                foreach (IDevice device in this.module.Devices)
                {                    
                    DeviceView deviceView = new DeviceView(Activity.ApplicationContext, device);
                    this.deviceViews.Add(deviceView);
                    deviceView.Click += delegate
                    {                        
                        this.module.CurrentDevice = device;
                        this.module.ConnectCommand.Execute(null);
                    };

                    this.devicesLinearLayout.AddView(deviceView);                    
                }
            }
        }

        private void RaiseViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Devices")
            {
                this.PopulateDevices();
            }
            else if(e.PropertyName == "DeviceConnectionStatus")
            {
                switch(this.module.DeviceConnectionStatus)
                {
                    case "Connected":
                        this.connectingSpinner.Visibility = ViewStates.Invisible;
                        this.SetDeviceButtonsEnabled(true);
                        break;
                    case "Not Connected":
                        this.connectingSpinner.Visibility = ViewStates.Invisible;
                        this.SetDeviceButtonsEnabled(true);
                        this.module.CurrentDevice = null;
                        break;
                    case "Connecting":
                        this.connectingSpinner.Visibility = ViewStates.Visible;
                        this.SetDeviceButtonsEnabled(false);
                        break;
                }
                this.connectionStatusTextView.Text = this.module.DeviceConnectionStatus;
            }
        }
    }
}
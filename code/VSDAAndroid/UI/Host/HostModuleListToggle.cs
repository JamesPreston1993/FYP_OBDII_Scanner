using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SupportActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace VSDAAndroid.UI.Host
{
    public class HostModuleListToggle : SupportActionBarDrawerToggle
    {
        private ActionBarActivity activity;
        private int openedResource;
        private int closedResource;

        public HostModuleListToggle(ActionBarActivity activity, DrawerLayout layout, int openedResource, int closedResource) : base(activity, layout, openedResource, closedResource)
        {
            this.activity = activity;
            this.openedResource = openedResource;
            this.closedResource = closedResource;
        }

        public override void OnDrawerOpened(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                base.OnDrawerOpened(drawerView);
            }
        }

        public override void OnDrawerClosed(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                base.OnDrawerClosed(drawerView);
            }
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                base.OnDrawerSlide(drawerView, slideOffset);
            }
        }
    }
}
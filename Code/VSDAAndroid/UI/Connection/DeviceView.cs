using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VSDACore.Connection;
using Android.Graphics;

namespace VSDAAndroid.UI.Connection
{
    public class DeviceView : Button
    {
        private IDevice device;

        // UI
        public Color EnabledColor { get; private set; }
        public Color DisabledColor { get; private set; }

        public DeviceView(Context context, IDevice device) : base(context)
        {
            this.device = device;
            this.EnabledColor = Color.White;
            this.DisabledColor = Color.Gray;
            this.SetAttributes();
        }

        private void SetAttributes()
        {
            // Text
            this.Text = device.Name;
            this.TextSize = 20.0f;

            // Color
            this.SetBackgroundColor(new Color(0x66, 0x66, 0x66));
            this.SetTextColor(this.EnabledColor);

            // Layout
            this.SetPadding(10, 0, 10, 0);
            this.Gravity = GravityFlags.CenterVertical;
            LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 100);
            layoutParams.BottomMargin = 10;
            this.LayoutParameters = layoutParams;
        }
    }
}
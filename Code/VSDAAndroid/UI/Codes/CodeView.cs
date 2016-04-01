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
using VSDACore.Modules.Codes;
using Android.Graphics;

namespace VSDAAndroid.UI.Codes
{
    public class CodeView : TextView
    {
        private ICodeViewModel code;

        public CodeView(Context context, ICodeViewModel code) : base(context)
        {
            this.code = code;
            this.SetAttributes();            
        }

        private void SetAttributes()
        {
            // Text
            if(code != null)
                this.Text = string.Format("{0} - {1}", code.Name, code.Description);
            else
                this.Text = "No Codes Found";
            this.TextSize = 20.0f;

            // Color
            this.SetBackgroundColor(new Color(0x66, 0x66, 0x66));
            this.SetTextColor(Color.White);

            // Layout
            this.SetPadding(10, 0, 10, 0);
            this.Gravity = GravityFlags.CenterVertical;
            LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 100);
            layoutParams.BottomMargin = 10;
            this.LayoutParameters = layoutParams;
        }
    }
}
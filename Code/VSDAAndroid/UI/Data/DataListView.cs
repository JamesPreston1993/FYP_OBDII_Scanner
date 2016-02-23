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
using VSDACore.Modules.Data;
using Android.Graphics;
using System.ComponentModel;

namespace VSDAAndroid.UI.Data
{
    public class DataListView : LinearLayout
    {
        private Context context;
        private IDataListViewModel dataItem;

        // UI
        private TextView nameTextView;
        private TextView valueTextView;

        public DataListView(Context context, IDataListViewModel dataItem) : base(context)
        {
            this.context = context;
            this.dataItem = dataItem;
            this.dataItem.PropertyChanged += this.RaiseViewModelChanged;
            this.SetAttributes();
        }           
        
        private void SetAttributes()
        {
            // Layout
            //LinearLayout layout = new LinearLayout(this.context);
            this.Orientation = Orientation.Vertical;
            this.SetBackgroundColor(new Color(0x66, 0x66, 0x66));
            this.SetPadding(10, 0, 10, 0);
            LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 200);
            layoutParams.BottomMargin = 10;
            this.LayoutParameters = layoutParams;

            layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 100);            

            // Name text view
            this.nameTextView = new TextView(this.context);
            this.nameTextView.TextSize = 20.0f;
            this.nameTextView.Text = this.dataItem.PidName;
            
            this.nameTextView.Gravity = GravityFlags.Left | GravityFlags.CenterVertical;
            this.nameTextView.SetTextColor(Color.White);
            this.nameTextView.LayoutParameters = layoutParams;

            // Value text view
            this.valueTextView = new TextView(this.context);
            this.valueTextView.TextSize = 20.0f;
            this.valueTextView.Text = this.dataItem.CurrentValue;
                  
            this.valueTextView.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
            this.valueTextView.SetTextColor(Color.White);
            this.valueTextView.LayoutParameters = layoutParams;

            this.AddView(this.nameTextView);
            this.AddView(this.valueTextView);

        }

        private void RaiseViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.valueTextView != null)
                this.valueTextView.Text = this.dataItem.CurrentValue;
        }
    }
}
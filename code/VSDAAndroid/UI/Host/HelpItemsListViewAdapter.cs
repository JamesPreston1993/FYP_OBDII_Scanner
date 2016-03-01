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
using VSDACore.Host;

namespace VSDAAndroid.UI.Host
{
    public class HelpItemsListViewAdapter : BaseAdapter
    {
        public override int Count
        {
            get
            {
                return this.host.CurrentModule.HelpItems.Count;
            }
        }

        private Context context;
        private IHostViewModel host;

        public HelpItemsListViewAdapter(Context context, IHostViewModel host)
        {
            this.context = context;
            this.host = host;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView view = new TextView(context);
            view.Text = this.host.CurrentModule.HelpItems[position].Title + "\n" + this.host.CurrentModule.HelpItems[position].Description + "\n";
            view.TextSize = 18;
            view.TextAlignment = TextAlignment.ViewStart;
            view.Background = parent.Background;            
            return view;
        }
    }
}
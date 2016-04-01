using Android.Content;
using Android.Views;
using Android.Widget;
using VSDACore.Connection;
using VSDACore.Host;

namespace VSDAAndroid.UI.Host
{
    public class ModuleListViewAdapter : BaseAdapter
    {
        public override int Count
        {
            get
            {
                return this.host.Modules.Count;
            }
        }

        private Context context;
        private IHostViewModel host;

        public ModuleListViewAdapter(Context context, IHostViewModel host)
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
            view.Text = this.host.Modules[position].Name;
            view.TextSize = 24;
            view.TextAlignment = TextAlignment.Center;
            view.Background = parent.Background;

            LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 100);            
            view.LayoutParameters = layoutParams;            
            
            view.Click += delegate
            {
                // Set Module
                if(ConnectionManager.Instance.IsInitialized)
                    this.host.CurrentModule = this.host.Modules[position];
            };

            return view;
        }
    }
}
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
using VSDACore.Modules.Codes;
using Android.Graphics;
using System.ComponentModel;

namespace VSDAAndroid.UI.Codes
{
    public class CodesFragment : Fragment
    {
        private IDtcModuleViewModel module;

        // Layouts
        private LinearLayout currentCodesLayout;
        private LinearLayout pendingCodesLayout;
        private LinearLayout permanentCodesLayout;
        private Button clearCodesButton;

        public CodesFragment(IDtcModuleViewModel module)
        {
            this.module = module;
            this.module.PropertyChanged += this.RaiseViewModelChanged;
        }

        public async override void OnCreate(Bundle savedInstanceState)
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
            View view = inflater.Inflate(Resource.Layout.CodesFragment, container, false);
                        
            this.currentCodesLayout = view.FindViewById<LinearLayout>(Resource.Id.CurrentCodesLinearLayout);                        
            this.pendingCodesLayout = view.FindViewById<LinearLayout>(Resource.Id.PendingCodesLinearLayout);
            this.permanentCodesLayout = view.FindViewById<LinearLayout>(Resource.Id.PermanentCodesLinearLayout);

            this.clearCodesButton = view.FindViewById<Button>(Resource.Id.ClearCodesButton);
            this.clearCodesButton.Click += delegate
            {
                this.module.ClearCodesCommand.Execute(null);
            };

            this.PopulateCodes();

            return view;            
        }

        private void PopulateCodes()
        {
            // Clear
            this.currentCodesLayout.RemoveAllViews();
            this.pendingCodesLayout.RemoveAllViews();
            this.permanentCodesLayout.RemoveAllViews();

            // Current Codes
            foreach (ICodeViewModel code in this.module.CurrentCodes)
            {
                currentCodesLayout.AddView(new CodeView(this.Activity.ApplicationContext, code));
            }

            // Pending Codes
            foreach (ICodeViewModel code in this.module.PendingCodes)
            {
                pendingCodesLayout.AddView(new CodeView(this.Activity.ApplicationContext, code));
            }

            // Permanent Codes
            foreach (ICodeViewModel code in this.module.PermanentCodes)
            {
                permanentCodesLayout.AddView(new CodeView(this.Activity.ApplicationContext, code));
            }
        }

        private void RaiseViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PopulateCodes();
        }        
    }
}
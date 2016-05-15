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
using WMS_Android.Classes.Activities;
using WMS_Android.Classes;
using WMS_Android.Classes.Model;
using System.Linq;

namespace WMS_Android.Activities
{
    [Activity(Label = "Home")]
    public class HomeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Home);

            var btnReceive = FindViewById<Button>(Resource.Id.btnReceive);
            btnReceive.Click += (sender, e) =>
            {
                var enterPOActivity = new Intent(this, typeof(EnterPOActivity));
                StartActivity(enterPOActivity);
            };

            var btnStock = FindViewById<Button>(Resource.Id.btnStock);
            btnStock.Click += (sender, e) =>
            {
                var enterStockActivity = new Intent(this, typeof(EnterBinnedStockActivity));
                StartActivity(enterStockActivity);
            };
        }
    }
}
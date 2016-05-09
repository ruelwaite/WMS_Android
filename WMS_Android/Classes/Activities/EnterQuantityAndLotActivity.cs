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

namespace WMS_Android.Classes.Activities
{
    [Activity(Label = "EnterQuantityActivity")]
    public class EnterQuantityAndLotActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EnterSkuQuantity);

            var txtPONumber = FindViewById<TextView>(Resource.Id.txtPONumber);
            var txtSkuNumber = FindViewById<TextView>(Resource.Id.txtSkuNumber);

            txtPONumber.Text = Intent.GetStringExtra(Globals._poNumber);
            txtSkuNumber.Text = Intent.GetStringExtra(Globals._skuNumber);
        }
    }
}
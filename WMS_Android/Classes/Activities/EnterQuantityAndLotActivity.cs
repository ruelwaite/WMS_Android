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
using System.IO;
using SQLite;
using WMS_Android.Classes.Model;

namespace WMS_Android.Classes.Activities
{
    [Activity(Label = "EnterQuantityActivity")]
    public class EnterQuantityAndLotActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EnterQuantityAndLot);

            var txtPONumber = FindViewById<TextView>(Resource.Id.txtPONumber);
            var txtSkuNumber = FindViewById<TextView>(Resource.Id.txtSkuNumber);
            var txtQuantity = FindViewById<TextView>(Resource.Id.txtQuanity);
            var txtLotNumber = FindViewById<TextView>(Resource.Id.txtLotNumber);
            var btnNext = FindViewById<Button>(Resource.Id.btnNext);

            txtPONumber.Text = Intent.GetStringExtra(Globals._poNumber);
            txtSkuNumber.Text = Intent.GetStringExtra(Globals._skuNumber);

            btnNext.Click += (sender, e) =>
            {
                btnNext.Enabled = false;
                SaveStock(txtPONumber, txtSkuNumber, txtLotNumber);

                var confirmReceivedActivity = new Intent(this, typeof(ConfirmReceivedActivity));
                StartActivity(confirmReceivedActivity);


            };
        }

        private void SaveStock(TextView txtPONumber, TextView txtSkuNumber, TextView txtLotNumber)
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), this.Resources.GetString(Resource.String.DatabaseFileName));
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<ReceivedStock>();
            var stock = new ReceivedStock { LotNumber = txtLotNumber.Text, PONumber = txtPONumber.Text, Quantity = 30, SKU = txtSkuNumber.Text };
            db.Insert(stock);
        }
    }
}
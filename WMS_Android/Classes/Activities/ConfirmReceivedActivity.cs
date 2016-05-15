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
    [Activity(Label = "Confirm Received")]
    public class ConfirmReceivedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ConfirmReceived);

            SetupGrid();

            SetupNextPOButton();

            SetupNextSkuButton();

        }

        private void SetupNextSkuButton()
        {
            var btnNextSkuForPO = FindViewById(Resource.Id.btnNextSkuForPO);
            btnNextSkuForPO.Click += (sender, e) =>
            {
                var enterNextSkuActivity = new Intent(this, typeof(EnterSkuActivity));

                PutPONumberIfExists(enterNextSkuActivity);

                StartActivity(enterNextSkuActivity);
            };
        }

        private void SetupNextPOButton()
        {
            var btnNextPO = FindViewById(Resource.Id.btnNextPO);
            btnNextPO.Click += (sender, e) =>
            {
                var enterPOActivity = new Intent(this, typeof(EnterPOActivity));
                StartActivity(enterPOActivity);
            };
        }

        private void PutPONumberIfExists(Intent enterPOActivity)
        {
            if (string.IsNullOrEmpty(Intent.GetStringExtra(Globals._poNumber)) == false)
            {
                enterPOActivity.PutExtra(Globals._poNumber, Intent.GetStringExtra(Globals._poNumber));
            }
        }

        private void SetupGrid()
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), this.Resources.GetString(Resource.String.DatabaseFileName));
            var db = new SQLiteConnection(dbPath);
            var stock = db.Table<ReceivedStock>().ToList();

            SetupGrid(stock);

            var btnNextSku = FindViewById<Button>(Resource.Id.btnNextSkuForPO);

            btnNextSku.Click += (sender, e) => {
                var enterSKUActivity = new Intent(this, typeof(EnterSkuActivity));
                StartActivity(enterSKUActivity);
            };
        }

        private void SetupGrid(List<ReceivedStock> stock)
        {
            var gvObject = FindViewById<GridView>(Resource.Id.gvCtrl);
            gvObject.Adapter = new ReceivedStockAdapter(this, stock);
            gvObject.ItemClick += (sender, e) =>
            {
                //string selectedName = e.View.FindViewById<TextView>(Resource.Id.txtPO1).Text;
            };
        }
    }
}
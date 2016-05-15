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
    [Activity(Label = "@string/EnterPO")]
    public class EnterPOActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EnterPO);
            SetTitle(Resource.String.EnterPO);
            var txtPONumber = FindViewById<EditText>(Resource.Id.txtPONumber);

            SetupScan(txtPONumber);
            //SetupManualEntry(txtPONumber);
            SetupSelectPO();

            var btnNext = FindViewById<Button>(Resource.Id.btnNext);
            btnNext.Click += (sender, e) =>
            {
                StartNextScreen(txtPONumber);
            };
        }
        private void SetupGrid()
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), this.Resources.GetString(Resource.String.DatabaseFileName));
            var db = new SQLiteConnection(dbPath);
            var purchaseOrders = db.Table<PurchaseOrder>().ToList();

            var gvObject = FindViewById<GridView>(Resource.Id.gvCtrl);
            gvObject.Adapter = new PurchaseOrderAdapter(this, purchaseOrders);

            gvObject.ItemClick += (sender, e) =>
            {
                var txtPONumber = FindViewById<EditText>(Resource.Id.txtPONumber);
                txtPONumber.Text = e.View.FindViewById<TextView>(Resource.Id.txtPO).Text;
            };
        }

        private void StartNextScreen(EditText txtPONumber)
        {
            var enterSkuActivity = new Intent(this, typeof(EnterSkuActivity));
            enterSkuActivity.PutExtra(Globals._poNumber, txtPONumber.Text);
            StartActivity(enterSkuActivity);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            var txtPONumber = FindViewById<EditText>(Resource.Id.txtPONumber);
            outState.PutString(Globals._poNumber, txtPONumber.Text);
        }

        private void SetupSelectPO()
        {
            var btnSelectPO = FindViewById<Button>(Resource.Id.btnSelectPO);
            btnSelectPO.Click += (sender, e) => { SetupGrid(); };
        }

        //private void SetupManualEntry(EditText txtPONumber)
        //{
        //    var btnEnterManually = FindViewById<Button>(Resource.Id.btnEnterManually);
        //    btnEnterManually.Click += (sender, e) => { txtPONumber.Enabled = true; txtPONumber.Text = string.Empty; };
        //}

        private void SetupScan(EditText txtPONumber)
        {
            var btnScan = FindViewById<Button>(Resource.Id.btnScan);
            Globals.SetupScanEvent(txtPONumber, btnScan, this);

        }
    }
}
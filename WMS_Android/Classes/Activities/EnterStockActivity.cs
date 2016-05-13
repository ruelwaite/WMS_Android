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
    [Activity(Label = "@string/EnterStock")]
    public class EnterStockActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EnterStock);
            SetTitle(Resource.String.EnterStock);

            var txtSkuNumber = FindViewById<EditText>(Resource.Id.txtSku);
            var btnScanSku = FindViewById<Button>(Resource.Id.btnScanSku);
            SetupScan(txtSkuNumber, btnScanSku);
            

            var btnScanBin = FindViewById<Button>(Resource.Id.btnScanBin);
            var txtBinNumber = FindViewById<EditText>(Resource.Id.txtBinNumber);
            SetupScan(txtBinNumber, btnScanBin);

            var btnEnterBin = FindViewById<Button>(Resource.Id.btnEnterBin);
            SetupManualEntry(txtBinNumber, btnEnterBin);

            var btnEnterSku = FindViewById<Button>(Resource.Id.btnEnterSku);
            SetupManualEntry(txtSkuNumber, btnEnterSku);


            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnAdd.Click += (sender, e) => {

                if (txtBinNumber.Text.Trim() == string.Empty) return;

                var db = Globals.GetDB();
                db.CreateTable<BinnedStock>();
                var stock = new BinnedStock { BinNumber = txtBinNumber.Text, SKU = txtSkuNumber.Text };

                db.Insert(stock);
                ShowGrid();
            };

            
        }

        private void ShowGrid()
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), this.Resources.GetString(Resource.String.DatabaseFileName));
            var db = new SQLiteConnection(dbPath);
            var binnedStock = db.Table<BinnedStock>().ToList();

            var gvObject = FindViewById<GridView>(Resource.Id.gvCtrl);
            gvObject.Adapter = new BinnedStockAdapter(this, binnedStock);

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


        private void SetupManualEntry(EditText txtField, Button button)
        {
            button.Click += (sender, e) => { txtField.Enabled = true; txtField.Text = string.Empty; txtField.RequestFocus(); };
        }

        private void SetupScan(EditText txtPONumber, Button scanButton)
        {
            Globals.SetupScanEvent(txtPONumber, scanButton, this);

        }
    }
}
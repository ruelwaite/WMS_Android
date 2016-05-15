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
using WMS_Android.Activities;

namespace WMS_Android.Classes.Activities
{
    [Activity(Label = "@string/EnterStock")]
    public class EnterBinnedStockActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EnterStock);
            SetTitle(Resource.String.EnterStock);

            var txtSkuNumber = SetupSKu();
            var txtBinNumber = SetupBin();
            var txtLotNumber = SetupLot();
            var txtQuantity = FindViewById<EditText>(Resource.Id.txtSkuQuantity);

            SetupAddButton(txtSkuNumber, txtBinNumber, txtQuantity, txtLotNumber);

            var btnFinish = FindViewById<Button>(Resource.Id.btnFinish);
            btnFinish.Click += (sender, e) => {

                var homeActivity = new Intent(this, typeof(HomeActivity));
                StartActivity(homeActivity);
            };

        }

        private void SetupAddButton(EditText txtSkuNumber, EditText txtBinNumber, EditText txtQuantity, EditText txtLotNumber)
        {
            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnAdd.Click += (sender, e) =>
            {

                if (txtBinNumber.Text.Trim() == string.Empty) return;

                var db = Globals.GetDB();
                db.CreateTable<BinnedStock>();
                var stock = new BinnedStock { BinNumber = txtBinNumber.Text, SKU = txtSkuNumber.Text, Quantity = int.Parse(txtQuantity.Text), LotNumber= txtLotNumber.Text };

                db.Insert(stock);
                ShowGrid();
            };
        }

        private EditText SetupLot()
        {
            var btnScanLot = FindViewById<Button>(Resource.Id.btnScanLot);
            var txtLotNumber = FindViewById<EditText>(Resource.Id.txtLotNumber);
            SetupScan(txtLotNumber, btnScanLot);
            return txtLotNumber;
        }

        private EditText SetupBin()
        {
            var btnScanBin = FindViewById<Button>(Resource.Id.btnScanBin);
            var txtBinNumber = FindViewById<EditText>(Resource.Id.txtBinNumber);
            SetupScan(txtBinNumber, btnScanBin);
            return txtBinNumber;
        }

        private EditText SetupSKu()
        {
            var txtSkuNumber = FindViewById<EditText>(Resource.Id.txtSku);
            var btnScanSku = FindViewById<Button>(Resource.Id.btnScanSku);
            SetupScan(txtSkuNumber, btnScanSku);
            return txtSkuNumber;
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
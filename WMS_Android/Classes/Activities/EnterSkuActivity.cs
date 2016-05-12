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
    [Activity(Label = "@string/EnterSku")]
    public class EnterSkuActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EnterSku);
            SetTitle(Resource.String.EnterSku);
            var txtSkuNumber = FindViewById<EditText>(Resource.Id.txtSkuNumber);
            var txtPONumber = FindViewById<TextView>(Resource.Id.txtPONumber);

            SetupScan(txtSkuNumber);
            SetupManualEntry(txtSkuNumber);
            ProcessPreviousInput(txtPONumber);

            var btnNext = FindViewById<Button>(Resource.Id.btnNext);
            btnNext.Click += (sender, e) =>
            {
                StartNextScreen(txtPONumber, txtSkuNumber);
            };
        }

        private void ProcessPreviousInput(TextView txtPONumber)
        {
            var poNumber = Intent.GetStringExtra(Globals._poNumber);

            if (poNumber != null)
            {
                txtPONumber.Text = poNumber;
            }
        }

        private void StartNextScreen(TextView txtPONumber, EditText txtSkuNumber)
        {
            var enterQuantityActivity = new Intent(this, typeof(EnterQuantityAndLotActivity));
            enterQuantityActivity.PutExtra(Globals._poNumber, txtPONumber.Text);
            enterQuantityActivity.PutExtra(Globals._skuNumber, txtSkuNumber.Text);
            StartActivity(enterQuantityActivity);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            var txtSkuNumber = FindViewById<EditText>(Resource.Id.txtSkuNumber);
            outState.PutString(Globals._poNumber, txtSkuNumber.Text);
        }

        private void SetupManualEntry(EditText txtSkuNumber)
        {
            var btnEnterManually = FindViewById<Button>(Resource.Id.btnEnterManually);
            btnEnterManually.Click += (sender, e) => { txtSkuNumber.Enabled = true; txtSkuNumber.Text = string.Empty; };
        }

        private void SetupScan(EditText txtSkuNumber)
        {
            var btnScan = FindViewById<Button>(Resource.Id.btnScan);

            btnScan.Click += async (sender, e) =>
            {
                ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);

                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null) {
                    txtSkuNumber.Text = result.Text;
                    txtSkuNumber.Enabled = true;
                }

            };
        }
    }
}
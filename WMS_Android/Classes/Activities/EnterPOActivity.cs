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
            SetupManualEntry(txtPONumber);

            var btnNext = FindViewById<Button>(Resource.Id.btnNext);
            btnNext.Click += (sender, e) =>
            {
                StartNextScreen(txtPONumber);
            };
        }

        private void StartNextScreen(EditText txtPONumber)
        {
            var enterQuantityActivity = new Intent(this, typeof(EnterQuantityActivity));
            enterQuantityActivity.PutExtra("PONumber", txtPONumber.Text);
            StartActivity(enterQuantityActivity);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            var txtPONumber = FindViewById<EditText>(Resource.Id.txtPONumber);
            outState.PutString("PONumber", txtPONumber.Text);
        }

        private void SetupManualEntry(EditText txtPONumber)
        {
            var btnEnterManually = FindViewById<Button>(Resource.Id.btnEnterManually);
            btnEnterManually.Click += (sender, e) => { txtPONumber.Enabled = true; txtPONumber.Text = string.Empty; };
        }

        private void SetupScan(EditText txtPONumber)
        {
            var btnScan = FindViewById<Button>(Resource.Id.btnScan);

            btnScan.Click += async (sender, e) =>
            {
                ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);

                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null) {
                    txtPONumber.Text = result.Text;
                    txtPONumber.Enabled = true;
                }

            };
        }
    }
}
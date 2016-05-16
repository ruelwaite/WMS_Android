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
using SQLite;
using System.IO;
using Android.Graphics;

namespace WMS_Android.Classes
{
    public static class Globals
    {
        public const string _poNumber = "PONumber";
        public const string _skuNumber = "SkuNumber";


        public const string _dbName = "database.db3";

        public static int[] _blueGrayAlternating = new int[] { 0xF2F2F2, 0x001DFC };
        public static int[] _blackWhiteAlternating = new int[] { 0x000000, 0xFFFFFF };

        public static void SetAlternatingBackground(View view, int position)
        {
            view.SetBackgroundColor(GetColorFromInteger(Globals._blueGrayAlternating[position % Globals._blueGrayAlternating.Length]));
        }

        public static void SetAlternatingText(TextView txtBox, int position)
        {
            txtBox.SetTextColor(GetColorFromInteger(Globals._blackWhiteAlternating[position % Globals._blackWhiteAlternating.Length]));
        }

        private static Color GetColorFromInteger(int color)
        {
            return Color.Rgb(Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color));
        }

        public static SQLiteConnection GetDB()
        {
            var dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), _dbName);
            var db = new SQLiteConnection(dbPath);
            return db;
        }

        public static void SetupScanEvent(EditText txtTarget, Button btnScan, Activity activity)
        {
            btnScan.Click += async (sender, e) =>
            {
                ZXing.Mobile.MobileBarcodeScanner.Initialize(activity.Application);

                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                {
                    txtTarget.Text = result.Text;
                    txtTarget.Enabled = true;
                }

            };
        }
    }

}
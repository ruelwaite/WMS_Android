using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using WMS_Android.Classes.Model;
using WMS_Android.Classes.Activities;

namespace WMS_Android
{
    [Activity( MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            SetTitle(Resource.String.ApplicationName);

            var button = FindViewById<Button>(Resource.Id.btnEnterPO);

            button.Click += (sender, e) =>
           {
               var intent = new Intent(this, typeof(EnterPOActivity));
               StartActivity(intent);
           };
                
        }

        private void SetupDB()
        {
            //var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), this.Resources.GetString(Resource.String.DatabaseFileName));
            //var txtDBLocation = FindViewById<TextView>(Resource.Id.txtDatabaseLocation);
            //txtDBLocation.Text = dbPath;
            //var db = new SQLiteConnection(dbPath);
            //db.CreateTable<ReceivedStock>();
            //var testStock = new ReceivedStock { LotNumber="5/3/2016", PONumber="PO_12345", Quantity=30, SKU="SKU8888" };
            //db.Insert(testStock);
            //testStock = new ReceivedStock { LotNumber = "5/3/2016", PONumber = "PO_67890", Quantity = 25, SKU = "SKU8888" };
            //db.Insert(testStock);

            //var table = db.Table<ReceivedStock>();
            //var txtData = FindViewById<TextView>(Resource.Id.txtData);
            //foreach (var s in table)
            //{
            //    txtData.Text += string.Format("{0} - {1}, {2}, {3}, {4}", s.Id.ToString(), s.PONumber, s.SKU, s.Quantity, s.LotNumber);
            //}
        }

    }
}


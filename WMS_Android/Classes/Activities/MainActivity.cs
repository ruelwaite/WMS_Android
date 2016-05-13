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
using System.Linq;
using WMS_Android.Activities;
using WMS_Android.Classes;

namespace WMS_Android
{
    [Activity( MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetupAdminUser();
            LoadPurchaseOrders();

            var loginActivity = new Intent(this, typeof(LoginActivity));
            StartActivity(loginActivity);

                
        }

        private void LoadPurchaseOrders()
        {
            var db = Globals.GetDB();
            db.CreateTable<PurchaseOrder>();

            var users = db.DeleteAll<PurchaseOrder>();

            db.Insert(new PurchaseOrder { IsOpen=true, PONumber="12345", Quantity = 20, Vendor = "Robert's Supplies" });
            db.Insert(new PurchaseOrder { IsOpen = true, PONumber = "56789", Quantity = 20, Vendor = "John's Produce" });
            db.Insert(new PurchaseOrder { IsOpen = true, PONumber = "78988", Quantity = 20, Vendor = "Greg's Goods" });
        }
        private void SetupAdminUser()
        {
            var db = Globals.GetDB();
            db.CreateTable<User>();
            var users = db.Table<User>().ToList();

            if (users.Count == 0)
            {
                db.Insert(new User { Username = "rugi", Password = "Taylor" });
            }
        }

    }
}


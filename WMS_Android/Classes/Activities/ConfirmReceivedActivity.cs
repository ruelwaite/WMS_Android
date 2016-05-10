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
    [Activity(Label = "ConfirmReceivedActivity")]
    public class ConfirmReceivedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ConfirmReceived);

            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), this.Resources.GetString(Resource.String.DatabaseFileName));
            var db = new SQLiteConnection(dbPath);
            var stock = db.Table<ReceivedStock>().ToList();

            var gvObject = FindViewById<GridView>(Resource.Id.gvCtrl);
            gvObject.Adapter = new gvItemAdapter(this, stock);

            gvObject.ItemClick += (sender, e) => {
                string selectedName = e.View.FindViewById<TextView>(Resource.Id.txtPO1).Text;
                Toast.MakeText(this, "You Click on name " + selectedName, ToastLength.Long).Show();
            };
        }
    }
}
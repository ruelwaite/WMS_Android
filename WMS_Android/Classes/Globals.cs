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

namespace WMS_Android.Classes
{
    public static class Globals
    {
        public const string _poNumber = "PONumber";
        public const string _skuNumber = "SkuNumber";


        public const string _dbName = "database.db3";

        public static SQLiteConnection GetDB()
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), _dbName);
            var db = new SQLiteConnection(dbPath);
            return db;
        }

    }
}
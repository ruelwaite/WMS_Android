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

namespace WMS_Android.Classes.Model
{
    [Table("ReceivedStock")]
    public class ReceivedStock
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string PONumber { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public string LotNumber { get; set; }

    }
}
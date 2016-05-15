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
using WMS_Android.Classes.Model;

namespace WMS_Android.Classes
{
    public class BinnedStockAdapter : BaseAdapter<BinnedStock>
    {

        private List<BinnedStock> _binnedStockItems { get; set; }
        private Activity _gridViewActivity { get; set; }

        public BinnedStockAdapter(Activity gridViewActivity, List<BinnedStock> binnedStockItems)
        {
            _binnedStockItems = binnedStockItems;
            _gridViewActivity = gridViewActivity;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View row, ViewGroup parent)
        {
            var item = _binnedStockItems[position];
            if (row == null)
                row = _gridViewActivity.LayoutInflater.Inflate(Resource.Layout.ListBinnedStock, null);

            row.FindViewById<TextView>(Resource.Id.txtBinNumber).Text = item.BinNumber;
            row.FindViewById<TextView>(Resource.Id.txtSku1).Text = item.SKU;
            row.FindViewById<TextView>(Resource.Id.txtQuantity1).Text = item.Quantity.ToString();
            row.FindViewById<TextView>(Resource.Id.txtLot1).Text = item.LotNumber;

            return row;
        }

        public override int Count
        {
            get
            {
                return _binnedStockItems.Count;
            }
        }

        public override BinnedStock this[int position]
        {
            get
            {
                return _binnedStockItems[position];
            }
        }
    }
}
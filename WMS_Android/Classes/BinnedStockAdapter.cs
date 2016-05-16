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
using Android.Graphics;

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

            Globals.SetAlternatingBackground(row, position);

            var txtBinNumber = row.FindViewById<TextView>(Resource.Id.txtBinNumber);
            txtBinNumber.Text = item.BinNumber;
            Globals.SetAlternatingText(txtBinNumber, position);

            var txtSku1 = row.FindViewById<TextView>(Resource.Id.txtSku1);
            txtSku1.Text = item.SKU;
            Globals.SetAlternatingText(txtSku1, position);

            var txtQuantity1 = row.FindViewById<TextView>(Resource.Id.txtQuantity1);
            row.FindViewById<TextView>(Resource.Id.txtQuantity1).Text = item.Quantity.ToString();
            Globals.SetAlternatingText(txtQuantity1, position);

            var txtLot1 = row.FindViewById<TextView>(Resource.Id.txtLot1);
            row.FindViewById<TextView>(Resource.Id.txtLot1).Text = item.LotNumber;
            Globals.SetAlternatingText(txtLot1, position);

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
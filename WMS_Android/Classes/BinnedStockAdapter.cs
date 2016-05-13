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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _binnedStockItems[position];
            if (convertView == null)
                convertView = _gridViewActivity.LayoutInflater.Inflate(Resource.Layout.ListBinnedStock, null);

            convertView.FindViewById<TextView>(Resource.Id.txtBinNumber).Text = item.BinNumber;
            convertView.FindViewById<TextView>(Resource.Id.txtSku1).Text = item.SKU;
            convertView.FindViewById<TextView>(Resource.Id.txtQuantity1).Text = item.Quantity.ToString();
            convertView.FindViewById<TextView>(Resource.Id.txtLot1).Text = item.LotNumber;

            return convertView;
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
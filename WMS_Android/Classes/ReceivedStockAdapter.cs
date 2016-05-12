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
    public class ReceivedStockAdapter : BaseAdapter<ReceivedStock>
    {

        private List<ReceivedStock> _receivedStockItems { get; set; }
        private Activity _gridViewActivity { get; set; }

        public ReceivedStockAdapter(Activity gridViewActivity, List<ReceivedStock> receivedStockItems)
        {
            _receivedStockItems = receivedStockItems;
            _gridViewActivity = gridViewActivity;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _receivedStockItems[position];
            if (convertView == null)
                convertView = _gridViewActivity.LayoutInflater.Inflate(Resource.Layout.ListStock, null);

            convertView.FindViewById<TextView>(Resource.Id.txtPO1).Text = item.PONumber;
            convertView.FindViewById<TextView>(Resource.Id.txtSku1).Text = item.SKU;
            convertView.FindViewById<TextView>(Resource.Id.txtQuantity1).Text = item.Quantity.ToString();
            convertView.FindViewById<TextView>(Resource.Id.txtLot1).Text = item.LotNumber;

            return convertView;
        }

        public override int Count
        {
            get
            {
                return _receivedStockItems.Count;
            }
        }

        public override ReceivedStock this[int position]
        {
            get
            {
                return _receivedStockItems[position];
            }
        }
    }
}
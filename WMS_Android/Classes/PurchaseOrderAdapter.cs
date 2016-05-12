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
    public class PurchaseOrderAdapter : BaseAdapter<PurchaseOrder>
    {

        private List<PurchaseOrder> _purchaseOrders { get; set; }
        private Activity _gridViewActivity { get; set; }

        public PurchaseOrderAdapter(Activity gridViewActivity, List<PurchaseOrder> purchaseOrderItems)
        {
            _purchaseOrders = purchaseOrderItems;
            _gridViewActivity = gridViewActivity;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _purchaseOrders[position];
            if (convertView == null)
                convertView = _gridViewActivity.LayoutInflater.Inflate(Resource.Layout.ListPurchaseOrder, null);

            convertView.FindViewById<TextView>(Resource.Id.txtPO).Text = item.PONumber;
            convertView.FindViewById<TextView>(Resource.Id.txtVendor).Text = item.Vendor;
            convertView.FindViewById<TextView>(Resource.Id.txtQuantity).Text = item.Quantity.ToString();
            convertView.FindViewById<TextView>(Resource.Id.txtOpenClosed).Text = item.IsOpen ? "Open" : "Closed";

            return convertView;
        }

        public override int Count
        {
            get
            {
                return _purchaseOrders.Count;
            }
        }

        public override PurchaseOrder this[int position]
        {
            get
            {
                return _purchaseOrders[position];
            }
        }
    }
}
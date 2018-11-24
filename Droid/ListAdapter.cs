using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Locations;
using Android.Runtime;
using Android.Content.PM;
using Newtonsoft.Json;
using Android.Support.Design.Widget;
using System.Net.Http;
using System.Text;
using Android.Support.V4.Content;
using Android;
using Android.Views;
using Java.Lang;
using Android.Content;
using System.Collections.Generic;

namespace BallPOoN.Droid {
	public class ListAdapter : BaseAdapter{
		Context context_;
		LayoutInflater inflater_;
		List<TimeLineUnit> around_;
		public ListAdapter(Context _context):base() {
			context_ = _context;
			inflater_ = context_.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
		}

		public override int Count => around_.Count;

		public override Object GetItem(int position) {
			return Java.Lang.Object.FromArray<TimeLineUnit>(around_.ToArray());
		}

		public override long GetItemId(int position) {
			return (long)around_[position].Id;
		}

		public void setAround(List<TimeLineUnit> _around){
			around_ = _around;
		}

		public override View GetView(int position, View convertView, ViewGroup parent) {
			convertView = inflater_.Inflate(Resource.Layout.tweetCard, parent, false);
			convertView.FindViewById<TextView>(Resource.Id.cardComment).Text = around_[position].Comment;
			convertView.FindViewById<TextView>(Resource.Id.cardFeel).Text = around_[position].Feel;

			return convertView;
		}
	}
}

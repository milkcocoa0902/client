using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

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

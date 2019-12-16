using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using BallPOoN.Droid.Fragment;
using Java.Lang;

namespace BallPOoN.Droid {
	//using Fragment = Android.Support.V4.App.Fragment;
	public class FragmentAdapter :  FragmentPagerAdapter{
		Context context_;
		string[] tabTitles_ = {
			"アカウント",
			"タイムライン",
			"自分",
			"近くにいる人",
			"設定"
		};

		public FragmentAdapter(FragmentManager _fm, Context _context) : base(_fm) {
			context_ = _context;

		}

		public FragmentAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) {
		}

		public override int Count => 5;

		public override Android.Support.V4.App.Fragment GetItem(int _position) {
			switch(_position){
			case 0:
				return new AccountFragment();
			case 1:
				return new TimeLineFragment();
			case 2:
				return new FeelingFragment();
			case 3:
				return new AroundFragment();
			case 4:
				return new PreferenceFragment();
			}

			return null;
		}

		public override ICharSequence GetPageTitleFormatted(int position) {
			return new Java.Lang.String(tabTitles_[position]);
		}
	}
}


using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Wikitude.Architect;

namespace BallPOoN.Droid.Fragment {
	public class AroundFragment : Android.Support.V4.App.Fragment {
		public static ArchitectView architectView;
		public static Context context_;
		public static bool paused_;
		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			//Toast.MakeText(Application.Context, "OnCreate on ArchitectView", ToastLength.Short).Show();
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			//var view = inflater.Inflate(Resource.Layout.around, container, false);
			//return view;

			//Toast.MakeText(Application.Context, "OnCreateView on ArchitectView", ToastLength.Short).Show();
			return architectView;
		}

		public override void OnDestroyView() {
			base.OnDestroyView();

			//Toast.MakeText(Application.Context, "OnDestroyView on ArchitectView", ToastLength.Short).Show();
		}

		public override void OnResume() {
			base.OnResume();
			if(architectView != null) {
				architectView.OnResume();
				paused_ = false;
			}
			//Toast.MakeText(Application.Context, "OnResume on ArchitectView", ToastLength.Short).Show();
		}

		public override void OnPause() {
			base.OnPause();

			if(architectView != null) {
				architectView.OnPause();
				paused_ = true;
			}

			//Toast.MakeText(Application.Context, "OnPause on ArchitectView", ToastLength.Short).Show();
		}

	}
}

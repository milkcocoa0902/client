using Android.OS;
using Android.Views;


namespace BallPOoN.Droid.Fragment {
	public class AccountFragment : Android.Support.V4.App.Fragment {
		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			var view = inflater.Inflate(Resource.Layout.account, container, false);

			return view;
		}
	}
}

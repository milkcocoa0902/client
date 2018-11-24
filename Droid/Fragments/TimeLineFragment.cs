
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Locations;
using Android.Content.PM;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android;
using System.Threading.Tasks;


namespace BallPOoN.Droid.Fragment {
	public class TimeLineFragment : Android.Support.V4.App.Fragment, SwipeRefreshLayout.IOnRefreshListener {
		View view_;
		ListView list_;
		ListAdapter adapter_;
		SwipeRefreshLayout swipe_;
		List<TimeLineUnit> around_;

		public SwipeRefreshLayout.IOnRefreshListener listener_;

		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			view_ = inflater.Inflate(Resource.Layout.timeLine, container, false);
			list_ = view_.FindViewById<ListView>(Resource.Id.listView1);
			adapter_ = new ListAdapter(Context.ApplicationContext);

			swipe_ = view_.FindViewById<SwipeRefreshLayout>(Resource.Id.refresh);
			swipe_.SetOnRefreshListener(this);
			swipe_.SetColorSchemeColors(new int[] { Application.Context.GetColor(Resource.Color.red),
				Application.Context.GetColor(Resource.Color.blue),
				Application.Context.GetColor(Resource.Color.limegreen)
			});
		
			return view_;
		}

		void SwipeRefreshLayout.IOnRefreshListener.OnRefresh() {
			new Handler().PostDelayed(() => {
				swipe_.Refreshing = false;
			},3000);

			new Handler().Post(async () => {
				if(MainActivity.latitude == null || MainActivity.longitude == null) {
					Toast.MakeText(Application.Context, "位置情報が取得できません", ToastLength.Short).Show();
					return;
				}

				var json = JsonConvert.SerializeObject(new Key("aaaaa", "", "", 200, MainActivity.latitude, MainActivity.longitude, 7000000, 100)); // optional comment

				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var client = new HttpClient();

				try {
					var response = await client.PostAsync("http://koron0902.ddns.net:23456/get_around", content);
					var resString = await response.Content.ReadAsStringAsync();
					around_ = JsonConvert.DeserializeObject<List<TimeLineUnit>>(resString);

					adapter_.setAround(around_);
					list_.Adapter = adapter_;

				} catch(Exception e) {
					Toast.MakeText(Context, e.ToString(), ToastLength.Short).Show();
				}
			});
		}
	}
}

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

namespace BallPOoN.Droid {
	[Activity(Label = "BallPOoN", 
	          MainLauncher = true, 
	          Icon = "@mipmap/icon",
	          Theme = "@style/appTheme")]
	public class MainActivity : AppCompatActivity, Android.Locations.ILocationListener {
		public static string latitude;
		public static string longitude;

		LocationManager locationManager;
		string locationProvider;

		public void OnLocationChanged(Location location) {
			latitude = location.Latitude.ToString();
			longitude = location.Longitude.ToString();
		}

		public void OnProviderDisabled(string provider) {
		}

		public void OnProviderEnabled(string provider) {
		}

		public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras) {
		}

		protected override void OnResume() {
			base.OnResume();

			if(locationProvider != null) {
				locationManager.RequestLocationUpdates(locationProvider, 1500, 1, this);
			}
		}

		protected override void OnPause() {
			base.OnPause();
			if(locationManager != null)
				locationManager.RemoveUpdates(this);
		}

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			// ToDo:
			// ユーザ認証を行い、以下のケースはログイン画面に戻す(Activity遷移)。正常に認証された場合は続行
			// 1. ユーザが存在しない
			// 2. 最後に起動してから一定期間経過(セッション:2~3[weeks])
			// 3. トークン, トークンシークレットが一致しない -> 不正アクセスされた恐れがあるので本人確認

			SetContentView(Resource.Layout.Main);


			// Mパーミッション設定
			{
				var permission = CheckSelfPermission(Manifest.Permission.AccessFineLocation);
				if(permission != Android.Content.PM.Permission.Granted) {
					if(ShouldShowRequestPermissionRationale(Manifest.Permission.AccessFineLocation)){

					}


					ActivityCompat.RequestPermissions(this,
									new string[] { Manifest.Permission.AccessFineLocation },
									1);
				}else {
					using(var locationCriteria = new Criteria()) {
						locationManager = (LocationManager)GetSystemService(LocationService);
						locationCriteria.Accuracy = Accuracy.Fine;
						locationCriteria.PowerRequirement = Power.NoRequirement;

						locationProvider = locationManager.GetBestProvider(locationCriteria, true);
					}
					locationManager.RequestLocationUpdates(locationProvider, 1500, 1, this);
				}
			}

			// tab設定
			using(var tablayout = FindViewById<TabLayout>(Resource.Id.tab_layout)){
				using(var viewPager = FindViewById<ViewPager>(Resource.Id.view_pager)) {
					viewPager.Adapter = new FragmentAdapter(SupportFragmentManager, ApplicationContext);
					tablayout.SetupWithViewPager(viewPager);
				}
				tablayout.GetTabAt(2).Select();
			}
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults) {
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			switch(requestCode) {
			case 1:
				if(grantResults.Length > 0 && grantResults[0] == Permission.Granted) {
					// GPS設定
					using(var locationCriteria = new Criteria()) {
						locationManager = (LocationManager)GetSystemService(LocationService);
						locationCriteria.Accuracy = Accuracy.Fine;
						locationCriteria.PowerRequirement = Power.NoRequirement;

						locationProvider = locationManager.GetBestProvider(locationCriteria, true);
					}
					locationManager.RequestLocationUpdates(locationProvider, 1500, 1, this);
				}
				break;
			}
		}
	}
}


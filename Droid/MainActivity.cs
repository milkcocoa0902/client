using Android;
using Android.App;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using BallPOoN.Droid.Fragment;
using Android.Content;
using Com.Wikitude.Architect;
using Com.Wikitude.Common.Camera;
using System.Threading.Tasks;
using Android.Widget;

namespace BallPOoN.Droid {
	[Activity(Label = "BallPOoN", 
	          MainLauncher = true, 
	          Icon = "@mipmap/icon",
	          Theme = "@style/appTheme",
	          ConfigurationChanges = ConfigChanges.Orientation |
						ConfigChanges.KeyboardHidden |
						ConfigChanges.ScreenSize)]
	public class MainActivity : AppCompatActivity, Android.Locations.ILocationListener {
		public static string latitude;
		public static string longitude;
		public static string altitude;
		public static Context context;
		bool enabled;
		bool isLocationInitialized;

		LocationManager locationManager;
		string locationProvider;

		public void OnLocationChanged(Location location) {
			latitude = location.Latitude.ToString();
			longitude = location.Longitude.ToString();
			altitude = location.Altitude.ToString();

			if(!isLocationInitialized){
				Toast.MakeText(ApplicationContext, "Location is initialized", ToastLength.Short).Show();
				isLocationInitialized = true;
			}

			if(AroundFragment.architectView != null){
				AroundFragment.architectView.SetLocation(location.Latitude, 
				                                         location.Longitude, 
				                                         location.Altitude, 
				                                         location.Accuracy);
				

			}
		}

		public void OnProviderDisabled(string provider) {
		}

		public void OnProviderEnabled(string provider) {
		}

		public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras) {
		}

		protected override void OnResume() {
			base.OnResume();

			Toast.MakeText(this, "OnResume", ToastLength.Short).Show();
			if(AroundFragment.architectView != null && AroundFragment.paused_) {
				AroundFragment.architectView.OnResume();
				AroundFragment.paused_ = false;
			}

			if(locationProvider != null) {
				locationManager.RequestLocationUpdates(locationProvider, 1500, 1, this);
			}
		}

		protected override void OnPause() {
			base.OnPause();
			if(locationManager != null)
				locationManager.RemoveUpdates(this);

			if(AroundFragment.architectView != null && !AroundFragment.paused_) {
				AroundFragment.architectView.OnPause();
				AroundFragment.paused_ = true;
			}
		}

		protected override void OnDestroy() {
			base.OnDestroy();

			if(AroundFragment.architectView != null)
				AroundFragment.architectView.OnDestroy();
		}

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			// ToDo:
			// ユーザ認証を行い、以下のケースはログイン画面に戻す(Activity遷移)。正常に認証された場合は続行
			// 1. ユーザが存在しない
			// 2. 最後に起動してから一定期間経過(セッション:2~3[weeks])
			// 3. トークン, トークンシークレットが一致しない -> 不正アクセスされた恐れがあるので本人確認


			SetContentView(Resource.Layout.Main);
			context = this;

			// Mパーミッション設定
			{
				var permission = CheckSelfPermission(Manifest.Permission.AccessFineLocation);
				var cameraPermission = CheckSelfPermission(Manifest.Permission.Camera);
				if(permission != Android.Content.PM.Permission.Granted ||
				   cameraPermission != Permission.Granted) {
					if(ShouldShowRequestPermissionRationale(Manifest.Permission.AccessFineLocation) ||
					   ShouldShowRequestPermissionRationale(Manifest.Permission.Camera) || 
					   ShouldShowRequestPermissionRationale(Manifest.Permission.Internet)){

					}

					ActivityCompat.RequestPermissions(this,
					                                  new string[] { Manifest.Permission.AccessFineLocation, 
						Manifest.Permission.Camera,
						Manifest.Permission.Internet},
									1);
				} else {
					using(var locationCriteria = new Criteria()) {
						locationManager = (LocationManager)GetSystemService(LocationService);
						locationCriteria.Accuracy = Accuracy.Fine;
						locationCriteria.PowerRequirement = Power.NoRequirement;

						locationProvider = locationManager.GetBestProvider(locationCriteria, true);
					}
					locationManager.RequestLocationUpdates(locationProvider, 1500, 1, this);
				}


				if(cameraPermission == Permission.Granted) {
					var config = new ArchitectStartupConfiguration {
						LicenseKey = "RZXUaNt3EqX/INY0REirYMQ4e73tOYpsoFywmJTrg6HWPdp7V/7e27rqIJBpT9Ni5CAu115KhWsY4IzVRXhzUw+9k9sLujt/OUcfLlOJJ0UymQystslk8VqfqaiIHf1+VtI7aK3SqAGlDYFLrWXgAWMXiW4DqqK7dqoOQ5kN6OBTYWx0ZWRfXyWhW0AGb4tTioEED84Fzr//uWdd5t8dzpPtYnJNIXKyOVICVkXDJRwfpJHtx4N0zIX43T5GB+FiMbU92xZurSK7OUiOcY2E35oqo9KGpnxL3syXdT2/03qxZ71/tQ8Yl3qautVzxVq/vu1ruXp/Utt9Nm5GNKKqjetm+pDRQe4URtmPuKgvKVy3dBmtBqceKZM/sVidHwps7m+DyWOtABv9Mz9u2NwifjtJiUiYC3oJMrnFUHS+9LuqNEo6phf6joXOdlYCMXCCTJiHPiyUKoTIF6rc3WVOIJ72seQpcvoZrIBtO4kd9GbjAMRaYeOeSfaFf+fAFhhbqIrjQGgCP50KVtt70bpCVdf4ADZXYrimKUMcUezL/QoA+FACDA+KVXJrGZHpmpbdmo+7WhUTnhe8hCUglg+fSp8cNbLmNoA+BXPp3o8p6VBDuTMgukNORJRAFtiUws62dhgu7RrKeHV0PBw0gXNJ92COVASrP/T478qMjk0XpxxepeRaTSq+rBSBv9OL48YE0SkV856UQ+9inVkf8ec54Q==",
						CameraPosition = CameraSettings.CameraPosition.Back,
						CameraResolution = CameraSettings.CameraResolution.FULLHD1920x1080,
						CameraFocusMode = CameraSettings.CameraFocusMode.Continuous,
						ArFeatures = ArchitectStartupConfiguration.Features.Geo,
						Camera2Enabled = true
					};

					AroundFragment.architectView = new ArchitectView(this);
					AroundFragment.architectView.OnCreate(config);
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

		protected override void OnPostCreate(Bundle savedInstanceState) {
			base.OnPostCreate(savedInstanceState);
			Toast.MakeText(this, "OnPostCreate", ToastLength.Short).Show();

			if(AroundFragment.architectView != null) {
				AroundFragment.architectView.OnPostCreate();
				AroundFragment.architectView.Load("architectworld/index.html");
			}
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults) {
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			enabled = true;

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


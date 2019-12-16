using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Widget;
using BallPOoN.Droid.Fragment;
using Com.Wikitude.Architect;
using Com.Wikitude.Common.Camera;

namespace BallPOoN.Droid {
	[JsonObject]
	class oauth{
		[JsonProperty("name")]
		public string Name { get; private set; }

		[JsonProperty("token")]
		public string Token { get; private set; }

		[JsonProperty("tokenSec")]
		public string TokenSec { get; private set; }

		public oauth(string _name, string _token, string _tokenSec){
			Name = _name;
			Token = _token;
			TokenSec = _tokenSec;
		}
	}

	[JsonObject]
	class oauthRes{
		[JsonProperty("result")]
		public bool Result { get; private set; }

		public oauthRes(bool _res){
			Result = _res;
		}
	}

	[Activity(Label = "BallPOoN",
						MainLauncher = true,
						Icon = "@mipmap/icon",
						Theme = "@style/appTheme",
						ConfigurationChanges = ConfigChanges.Orientation |
						ConfigChanges.KeyboardHidden |
						ConfigChanges.ScreenSize)]
	public class loginActivity : AppCompatActivity {

		public static string account;
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.login);

			var loginButton = FindViewById<Button>(Resource.Id.loginButton);
			loginButton.Click += async (sender, e) => {
				account = FindViewById<EditText>(Resource.Id.accountId).Text;
				var pw = FindViewById<EditText>(Resource.Id.accountPW).Text;

				
				var json = JsonConvert.SerializeObject(new oauth(account, pw, pw + pw));

				var content = new StringContent(json,
				                                Encoding.UTF8,
				                                "application/json");
				var client = new HttpClient();
				var response = await client.PostAsync("http://koron0902.ddns.net:23456/oauth",
				                                content);
				var resString = await response.Content.ReadAsStringAsync();
				var res = JsonConvert.DeserializeObject<oauthRes>(resString);

				if(res.Result){
					var intent = new Intent(Application.Context, typeof(MainActivity));
					StartActivity(intent);
				}else{
					Toast.MakeText(ApplicationContext, "IDまたはパスワードが間違っています", ToastLength.Short).Show();
				}
			};


			{

				var permission = CheckSelfPermission(Manifest.Permission.AccessFineLocation);
				var cameraPermission = CheckSelfPermission(Manifest.Permission.Camera);
				if(permission != Android.Content.PM.Permission.Granted ||
					 cameraPermission != Permission.Granted) {
					if(ShouldShowRequestPermissionRationale(Manifest.Permission.AccessFineLocation) ||
						 ShouldShowRequestPermissionRationale(Manifest.Permission.Camera) ||
						 ShouldShowRequestPermissionRationale(Manifest.Permission.Internet)) {

					}

					ActivityCompat.RequestPermissions(this,
					                                  new string[] { Manifest.Permission.AccessFineLocation,
						Manifest.Permission.Camera,
						Manifest.Permission.Internet},
									1);
				}
			}

		}
	}
}

using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;
using BallPOoN.Droid.Fragment;

namespace BallPOoN.Droid {
	[Activity(Label = "postCommentActivity",
						Icon = "@mipmap/icon",
						Theme = "@style/appTheme")]
	public class postCommentActivity : AppCompatActivity {
		protected override void OnCreate(Bundle savedInstanceState) {

			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.post);
			// Create your application here
			var howFeeling = FindViewById<TextView>(Resource.Id.howFeeling);
			howFeeling.Text = Intent.GetStringExtra("feel");

			howFeeling.SetBackgroundColor(new Color(Intent.GetIntExtra("color", 0)));

			var comment = FindViewById<EditText>(Resource.Id.postComment);
			comment.Hint = GetString(Resource.String.postHint);
			var submit = FindViewById<Button>(Resource.Id.buttonSubmit);
			submit.Text = GetString(Resource.String.submit);

			// ToDo:
			// ユーザ名を決め打ちにしているため、これを可変にする
			submit.Click += (sender, e) => {
				var json = JsonConvert.SerializeObject(new Post(loginActivity.account, // user
				                                                System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), // post time
				                                                MainActivity.longitude, // latitude
				                                                MainActivity.latitude, // longtitude
				                                                MainActivity.altitude,
				                                                Intent.GetStringExtra("feel"), // how feeling
				                                                comment.Text)); // optional comment

				var content = new StringContent(json,
				                                Encoding.UTF8,
				                                "application/json");
				var client = new HttpClient();
				var response = client.PostAsync("http://koron0902.ddns.net:23456/post",
																				content);

				var js = "World.requestPersonalInformation('" +
				loginActivity.account + "', " +
										 MainActivity.latitude + ", " +
										 MainActivity.longitude + ");";

				//Toast.MakeText(ApplicationContext, js, ToastLength.Short).Show();
				AroundFragment.architectView.CallJavascript(js);

				Finish();
			};
		}
	}
}

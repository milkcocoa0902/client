using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using Android.Graphics;

using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BallPOoN;

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
			comment.RequestFocus();
			comment.FocusedByDefault = true;
			var submit = FindViewById<Button>(Resource.Id.buttonSubmit);
			submit.Text = GetString(Resource.String.submit);

			// ToDo:
			// ユーザ名を決め打ちにしているため、これを可変にする
			submit.Click += (sender, e) => {
				var json = JsonConvert.SerializeObject(new Post("aaaaa", // user
				                                                System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), // post time
				                                                MainActivity.longitude, // latitude
				                                                MainActivity.latitude, // longtitude
				                                                Intent.GetStringExtra("feel"), // how feeling
				                                                comment.Text)); // optional comment

				var content = new StringContent(json,
				                                Encoding.UTF8,
				                                "application/json");
				var client = new HttpClient();
				var response = client.PostAsync("http://koron0902.ddns.net:23456/post",
																				content);
				Finish();
			};
		}
	}
}

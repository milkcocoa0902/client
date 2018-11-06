
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


namespace BallPOoN.Droid.Fragment {
	public class FeelingFragment : Android.Support.V4.App.Fragment {
		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			var view = inflater.Inflate(Resource.Layout.feeling, container, false);

			var textView = view.FindViewById<TextView>(Resource.Id.textView1);
			var ButtonHappy = view.FindViewById<Button>(Resource.Id.buttonHappy);
			var ButtonSad = view.FindViewById<Button>(Resource.Id.buttonSad);
			var ButtonMoving = view.FindViewById<Button>(Resource.Id.buttonMoving);
			var ButtonHoom = view.FindViewById<Button>(Resource.Id.buttonHoom);
			var ButtonHelp = view.FindViewById<Button>(Resource.Id.buttonHelp);

			textView.Text = GetString(Resource.String.feel);
			ButtonHappy.Text = GetString(Resource.String.feelHappy);
			ButtonSad.Text = GetString(Resource.String.feelSad);
			ButtonMoving.Text = GetString(Resource.String.feelMoving);
			ButtonHoom.Text = GetString(Resource.String.feelHoom);
			ButtonHelp.Text = GetString(Resource.String.feelHelp);
			

			ButtonHappy.Click += (sender, e) => {
				var intent = new Intent(Application.Context, typeof(BallPOoN.Droid.postCommentActivity));
				intent.PutExtra("feel", GetString(Resource.String.feelHappy));
				intent.PutExtra("color", Application.Context.GetColor(Resource.Color.red));
				StartActivity(intent);
			};

			ButtonSad.Click += (sender, e) => {
				var intent = new Intent(Application.Context, typeof(BallPOoN.Droid.postCommentActivity));
				intent.PutExtra("feel", GetString(Resource.String.feelSad));
				intent.PutExtra("color", Application.Context.GetColor(Resource.Color.blue));
				StartActivity(intent);
			};

			ButtonMoving.Click += (sender, e) => {
				var intent = new Intent(Application.Context, typeof(BallPOoN.Droid.postCommentActivity));
				intent.PutExtra("feel", GetString(Resource.String.feelMoving));
				intent.PutExtra("color", Application.Context.GetColor(Resource.Color.lime));
				StartActivity(intent);
			};

			ButtonHoom.Click += (sender, e) => {
				var intent = new Intent(Application.Context, typeof(BallPOoN.Droid.postCommentActivity));
				intent.PutExtra("feel", GetString(Resource.String.feelHoom));
				intent.PutExtra("color", Application.Context.GetColor(Resource.Color.darkgray));
				StartActivity(intent);
			};

			ButtonHelp.Click += (sender, e) => {
				var intent = new Intent(Application.Context, typeof(BallPOoN.Droid.postCommentActivity));
				intent.PutExtra("feel", GetString(Resource.String.feelHelp));
				intent.PutExtra("color", Application.Context.GetColor(Resource.Color.cyan));
				StartActivity(intent);
			};

			return view;
		}
	}
}

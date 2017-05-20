using Android.App;
using Android.Widget;
using Android.OS;

namespace WidgetPractice01
{
	[Activity(Label = "@string/ApplicationName",MainLauncher = true,Icon = "@drawable/icon")]
	public class MainActivity:Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
		base.OnCreate(bundle);

		// Set our view from the "main" layout resource
		// SetContentView (Resource.Layout.Main);
		}

		protected override void OnRestart()
		{
		base.OnRestart();

		Toast.MakeText(this, "MainActivity.OnRestart", ToastLength.Short).Show();
		}

		protected override void OnStart()
		{
		base.OnStart();

		Toast.MakeText(this, "MainActivity.OnStart", ToastLength.Short).Show();
		}

		protected override void OnResume()
		{
		base.OnResume();

		Toast.MakeText(this, "MainActivity.OnResume", ToastLength.Short).Show();
		}

		

	}
}


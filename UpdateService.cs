using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WidgetPractice01
{
	/// <summary>
	/// ウィジェットのビューの更新を行うクラス
	/// ウィジェット実装する上で必須ではない
	/// </summary>
	[Service]	// AndroidManifestに追記する代わりにAttributeを設定する(xamarin)
	class UpdateService : Service
	{

		public override StartCommandResult OnStartCommand(Intent intent,[GeneratedEnum] StartCommandFlags flags,int startId)
		{

			RemoteViews updateViews = BuildUpdate();

			ComponentName widget = new ComponentName(this, Java.Lang.Class.FromType(typeof(AppWidget)).Name);
			AppWidgetManager manager = AppWidgetManager.GetInstance(this);
			manager.UpdateAppWidget(widget, updateViews);

			Toast.MakeText(this, "UpdateService:OnstartCommand", ToastLength.Long).Show();

			// とくにserviceを複数回スタートさせてないならこれで十分だと思う
			return StartCommandResult.Sticky;
		}

		public override IBinder OnBind(Intent intent)
		{
			// バインドされたくない場合はnullを返す
			return null;
		}

		public RemoteViews BuildUpdate()
		{
			RemoteViews updateViews = new RemoteViews(this.PackageName, Resource.Layout.widget_app_layout2);

			System.DateTime time = System.DateTime.Now;
			string text = string.Format("now time : {0}", time);
			updateViews.SetTextViewText(Resource.Id.Text2, text);

			// ボタンクリックでActivityの起動
			Intent WidgetIntent = new Intent(this, typeof(MainActivity));
			PendingIntent appIntent = PendingIntent.GetActivity(this, 0, WidgetIntent, 0);
			updateViews.SetOnClickPendingIntent(Resource.Id.TestButton2, appIntent);

			return updateViews;
		}
	}
}
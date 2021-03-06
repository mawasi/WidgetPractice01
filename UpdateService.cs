﻿using System;
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
	[IntentFilter(new string[] {"Update_Time"})]
	[Service]	// AndroidManifestに追記する代わりにAttributeを設定する(xamarin)
	class UpdateService : Service
	{

		public override void OnCreate()
		{
			base.OnCreate();
		}

		public override StartCommandResult OnStartCommand(Intent intent,[GeneratedEnum] StartCommandFlags flags,int startId)
		{

			RemoteViews updateViews = BuildUpdate(intent);

			ComponentName widget = new ComponentName(this, Java.Lang.Class.FromType(typeof(AppWidget)).Name);
			AppWidgetManager manager = AppWidgetManager.GetInstance(this);
			manager.UpdateAppWidget(widget, updateViews);

//			Toast.MakeText(this, "UpdateService:OnstartCommand", ToastLength.Long).Show();

			// とくにserviceを複数回スタートさせてないならこれで十分だと思う
			return StartCommandResult.Sticky;
		}

		public override IBinder OnBind(Intent intent)
		{
			// バインドされたくない場合はnullを返す
			return null;
		}

		public RemoteViews BuildUpdate(Intent intent)
		{
			RemoteViews updateViews = new RemoteViews(this.PackageName, Resource.Layout.widget_app_layout2);

			System.DateTime time = System.DateTime.Now;
			string text = $"{time}";// string.Format("now time : {0}", time);
			updateViews.SetTextViewText(Resource.Id.Text2, text);

			// ウィジェットのUI処理の登録なんかは一度やったら必要なさそうに感じるが、
			// AppWidgetProvider::OnEnabled()なんかでやろうとすると、ウィジェットを2つ立ち上げると、
			// OnEnabled()は一番最初に起動したウィジェットでしか呼ばれないので、あとに立ち上げたほうのボタンなんかをクリックしても反応しなくなる。
			// Update処理(要はここ)で通すようにしたほうがいいのかも。
#if true
			// ボタンクリックでActivityの起動
			Intent WidgetIntent = new Intent(this, typeof(MainActivity));
			PendingIntent appIntent = PendingIntent.GetActivity(this, 0, WidgetIntent, 0);
			updateViews.SetOnClickPendingIntent(Resource.Id.TestButton2, appIntent);
			// これでボタンのイメージ変えることもできる
//			updateViews.SetImageViewResource(Resource.Id.TestButton2, Resource.Drawable.widget_button_focused);
			// Intentに設定されたAction見てみる
			System.Diagnostics.Debug.WriteLine("WidgetIntent.Action = " + WidgetIntent.Action);

			// 時間書いたメッセージ部分のクリックでもActivity起動できるようにしてみる
			updateViews.SetOnClickPendingIntent(Resource.Id.Text2, appIntent);

			// ボタン押したら時計が更新されるようにする
			Intent UpdateClockIntent = new Intent();
			// ボタンを押したときにこのIntentがブロードキャストされて、それをこのクラスが直接受け取り、OnStartCommandが呼ばれる。
			UpdateClockIntent.SetAction("Update_Time");
			PendingIntent updateTimeIntent = PendingIntent.GetService(this, 0, UpdateClockIntent, 0);	// サービスクラスへ投げるインテント作成
			updateViews.SetOnClickPendingIntent(Resource.Id.TestButton3, updateTimeIntent); 
#endif
			if (!string.IsNullOrEmpty(intent.Action)) {
				if(intent.Action.Equals("Update_Time")) {
					time = System.DateTime.Now;
					text = $"{time}";// string.Format("now time : {0}", time);
					updateViews.SetTextViewText(Resource.Id.Text2, text);
//					Toast.MakeText(this, "ClockUpdate.", ToastLength.Short).Show();
				}
			}

			return updateViews;
		}
	}
}
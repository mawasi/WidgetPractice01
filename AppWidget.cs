using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Appwidget;

/// <summary>
/// ウィジェット作成参考
/// AndroidのWidgetで時計を作ってみる
/// http://woshidan.hatenablog.com/entry/2016/07/03/083000
/// </summary>

namespace WidgetPractice01
{
	/// <summary>
	/// ウィジェット実装で必須のクラス
	/// 以下のAttributeはXamarinでの記述方法
	/// Javaで実装する場合はManifest.xmlに直接記述される部分
	/// </summary>
	[BroadcastReceiver(Label = "@string/WidgetName")]
	[IntentFilter(new string[] {AppWidgetManager.ActionAppwidgetUpdate})]
	[MetaData("android.appwidget.provider", Resource="@xml/widget_practice01")]
	public class AppWidget : AppWidgetProvider
	{
		public override void OnUpdate(Context context,AppWidgetManager appWidgetManager,int[] appWidgetIds)
		{
			base.OnUpdate(context,appWidgetManager,appWidgetIds);

			context.StartService (new Intent (context, typeof (UpdateService)));
		}
	}
}
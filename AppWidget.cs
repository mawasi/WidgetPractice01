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

			// 今回程度の処理であればserviceにする意味はあまりないけど、
			// 重い処理を行う場合はOnUpdateで直接処理するのではなくServiceを使用して処理すること。
			context.StartService (new Intent (context, typeof (UpdateService)));
		}

		/// <summary>
		/// This is called when an instance the App Widget is created for the first time.
		/// </summary>
		/// <remarks>
		/// If the user adds two instances of your App Widget, this is only called the first time. 
		/// </remarks>
		/// <param name="context"></param>
		public override void OnEnabled(Context context)
		{
		base.OnEnabled(context);
		}


		/// <summary>
		/// AppWidgetManagerからのIntentを受け取って各On~Methodに処理を投げる
		/// </summary>
		/// <remarks>
		/// OnUpdateもOnEnabledなども全部まずここが呼ばれた上で、
		/// IntentのActionに合わせてOnUpdateなどに飛ばされる。
		/// 基本的な更新処理だけならOnUpdateに書くべきで、任意のコールバック処理を実装したい場合にここに追記する。
		/// </remarks>
		/// <param name="context"></param>
		/// <param name="intent"></param>
		public override void OnReceive(Context context,Intent intent)
		{
		base.OnReceive(context,intent);
		}
	}
}
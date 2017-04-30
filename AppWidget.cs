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
/// �E�B�W�F�b�g�쐬�Q�l
/// Android��Widget�Ŏ��v������Ă݂�
/// http://woshidan.hatenablog.com/entry/2016/07/03/083000
/// </summary>

namespace WidgetPractice01
{
	/// <summary>
	/// �E�B�W�F�b�g�����ŕK�{�̃N���X
	/// �ȉ���Attribute��Xamarin�ł̋L�q���@
	/// Java�Ŏ�������ꍇ��Manifest.xml�ɒ��ڋL�q����镔��
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
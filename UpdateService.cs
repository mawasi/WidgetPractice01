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
	/// �E�B�W�F�b�g�̃r���[�̍X�V���s���N���X
	/// �E�B�W�F�b�g���������ŕK�{�ł͂Ȃ�
	/// </summary>
	[Service]	// AndroidManifest�ɒǋL��������Attribute��ݒ肷��(xamarin)
	class UpdateService : Service
	{

		public override StartCommandResult OnStartCommand(Intent intent,[GeneratedEnum] StartCommandFlags flags,int startId)
		{

			RemoteViews updateViews = BuildUpdate();

			ComponentName widget = new ComponentName(this, Java.Lang.Class.FromType(typeof(AppWidget)).Name);
			AppWidgetManager manager = AppWidgetManager.GetInstance(this);
			manager.UpdateAppWidget(widget, updateViews);

			Toast.MakeText(this, "UpdateService:OnstartCommand", ToastLength.Long).Show();

			// �Ƃ���service�𕡐���X�^�[�g�����ĂȂ��Ȃ炱��ŏ\�����Ǝv��
			return StartCommandResult.Sticky;
		}

		public override IBinder OnBind(Intent intent)
		{
			// �o�C���h���ꂽ���Ȃ��ꍇ��null��Ԃ�
			return null;
		}

		public RemoteViews BuildUpdate()
		{
			RemoteViews updateViews = new RemoteViews(this.PackageName, Resource.Layout.widget_app_layout2);

			System.DateTime time = System.DateTime.Now;
			string text = string.Format("now time : {0}", time);
			updateViews.SetTextViewText(Resource.Id.Text2, text);

			// �{�^���N���b�N��Activity�̋N��
			Intent WidgetIntent = new Intent(this, typeof(MainActivity));
			PendingIntent appIntent = PendingIntent.GetActivity(this, 0, WidgetIntent, 0);
			updateViews.SetOnClickPendingIntent(Resource.Id.TestButton2, appIntent);

			return updateViews;
		}
	}
}
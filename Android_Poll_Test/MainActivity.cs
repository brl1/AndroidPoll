using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Android_Poll_Test
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }

        protected override void OnStop()
        {
/*            var intent = new Intent(Application.Context, typeof(AndroidCommsPollServiceRunner));

            if (intent != null)
            {
                var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, intent, PendingIntentFlags.CancelCurrent);

                if (pendingIntent != null)
                {
                    var alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
                    alarmManager.Cancel(pendingIntent);
                    alarmManager = null;
                }

                pendingIntent = null;
            }

            intent = null;

            base.OnStop();*/
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var intervalMs = 5000;

            var intent = new Intent(Application.Context, typeof(AndroidCommsPollServiceRunner));
            intent.PutExtra("userId", 1234);
            intent.PutExtra("pollIntervalMs", intervalMs);
            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, intent, PendingIntentFlags.CancelCurrent);
            var alarmManager = (AlarmManager)Application.Context.GetSystemService(AlarmService);

            alarmManager.SetExactAndAllowWhileIdle(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + intervalMs, pendingIntent);
        }
	}
}


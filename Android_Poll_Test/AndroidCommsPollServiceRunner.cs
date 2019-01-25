using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace Android_Poll_Test
{
    [BroadcastReceiver]
    public class AndroidCommsPollServiceRunner : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, $"Timer has triggered {DateTime.UtcNow.ToLocalTime()}", ToastLength.Short).Show();

            var intervalMs = 5000;

            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, intent, PendingIntentFlags.CancelCurrent);
            var alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);

            alarmManager.SetExactAndAllowWhileIdle(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + intervalMs, pendingIntent);
        }
    }
}
using Android.Content;
using Android.OS;

namespace Android_Poll_Test
{
    public static class WakeLocker
    {
		private static PowerManager.WakeLock _wakeLock;
		private static object _wakeLock_Lock = new object();

        public static bool HasLock()
        {
			lock (_wakeLock_Lock)
			{
				return _wakeLock != null && _wakeLock.IsHeld;
			}
        }

        public static void AcquireWakeLock(Context context)
        {
			lock (_wakeLock_Lock)
			{
				if (_wakeLock != null)
				{
					_wakeLock.Release();
				}

				var pm = (PowerManager)context.GetSystemService(Context.PowerService);
				_wakeLock = pm.NewWakeLock(WakeLockFlags.Partial | WakeLockFlags.AcquireCausesWakeup | WakeLockFlags.OnAfterRelease, "TMEvo");
				pm = null;

				_wakeLock.Acquire();
			}
        }

        public static void Release()
        {
			lock (_wakeLock_Lock)
			{
				if (_wakeLock == null) return;

				_wakeLock.Release();
				_wakeLock = null;
			}
        }
    }
}
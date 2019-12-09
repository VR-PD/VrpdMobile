using Android.App;
using Android.Provider;
using BlueNetScanner.Droid;
using System.Security;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidDevice))]

namespace BlueNetScanner.Droid
{
    [SecurityCritical()]
    public class AndroidDevice : IDevice
    {
        [SecurityCritical()]
        public string GetIdentifier()
        {
            return Settings.Secure.GetString(Application.Context.ContentResolver, Settings.Secure.AndroidId);
        }
    }
}

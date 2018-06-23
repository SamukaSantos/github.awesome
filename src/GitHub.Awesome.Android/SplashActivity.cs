
using Android.App;
using Android.OS;

namespace GitHub.Awesome.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
             Label = "GitHub Awesome",
             Icon = "@drawable/icon",
             MainLauncher = true,
             NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            System.Threading.Thread.Sleep(500);

            StartActivity(typeof(MainActivity));
        }
    }
}
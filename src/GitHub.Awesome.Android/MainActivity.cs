
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace GitHub.Awesome.Droid
{
	[Activity(Label = "GitHub Awesome", 
              Icon  = "@drawable/icon", 
              Theme = "@style/MainTheme", 
              MainLauncher = false, 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource   = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

			AppContext.Set(this);

            global::Xamarin.Forms.Forms.Init(this, bundle);

			FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            
            LoadApplication(new App());
        }
    }
}


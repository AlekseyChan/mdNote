using Android.App;
using Android.Content.PM;
using Android.OS;

namespace mdNote.Droid
{
    [Activity(Label = "mdNote.Android", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Android.Content.Intent.ActionSend }, Categories = new[] { Android.Content.Intent.CategoryDefault }, DataMimeType = "text/plain")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions)
        {
            base.OnRequestPermissionsResult(requestCode, permissions);
        }

        public void CheckPermissions()
        {
            if ((int)Build.VERSION.SdkInt < 23) return;


            //if (ApplicationContext.CheckCallingPermission(Android.Manifest.Permission.ReadExternalStorage) != Permission.Granted)
                RequestPermissions(new string[] { Android.Manifest.Permission.ReadExternalStorage }, 25);

            //if (ApplicationContext.CheckCallingPermission(Android.Manifest.Permission.WriteExternalStorage) != Permission.Granted)
                RequestPermissions(new string[] { Android.Manifest.Permission.WriteExternalStorage }, 25);
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            if (Intent.Action.Equals(Android.Content.Intent.ActionSend))
            {
                mdNote.Pages.EditorPage.DefaultContent = Intent.GetStringExtra(Android.Content.Intent.ExtraText);
            }
            CheckPermissions();
            LoadApplication(new App());
        }
    }
}
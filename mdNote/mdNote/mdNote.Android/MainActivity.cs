using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using System.Threading.Tasks;

namespace mdNote.Droid
{
    [Activity(Label = "mdNote.Android", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Android.Content.Intent.ActionSend }, Categories = new[] { Android.Content.Intent.CategoryDefault }, DataMimeType = "text/plain")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const int REQUEST_CODE_OPEN_DIRECTORY = 1;
        public async Task SelectFolderAsync()
        {
            var intent = new Intent(Intent.ActionOpenDocumentTree);
            StartActivityForResult(intent, REQUEST_CODE_OPEN_DIRECTORY);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == REQUEST_CODE_OPEN_DIRECTORY && resultCode == Result.Ok)
            {
                var folder = System.Net.WebUtility.UrlDecode(data.DataString);

                mdNote.Services.DeviceServices.LocationAdded(folder);
            }
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
            LoadApplication(new App());
        }

        private void TestWrite()
        {
            var sdCard = "/storage/0123-4567/Downloads";
            var filePath = System.IO.Path.Combine(sdCard, "test.txt");
            using (System.IO.StreamWriter writer = System.IO.File.CreateText(filePath))
            {
                writer.Write("test");
            }
        }

    }
}
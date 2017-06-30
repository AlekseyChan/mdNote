using Android.App;
using Android.Content.PM;
using Android.OS;

namespace mdNote.Droid
{
    [Activity(Label = "mdNote.Android", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Android.Content.Intent.ActionSend }, Categories = new[] { Android.Content.Intent.CategoryDefault }, DataMimeType = "text/plain")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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
//            CheckPermissions();
            LoadApplication(new App());
            TestWrite();
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

/*        public void TestWrite2(
            DocumentFile pickedDir)
        {
            Android.Provider.DocumentsProvider p = new Android.Provider.DocumentsProvider();
            try
            {
                DocumentFile file = pickedDir.createFile("image/jpeg", "try2.jpg");
                OutputStream out = getContentResolver().openOutputStream(file.getUri());
                try
                {

                    // write the image content

                }
                finally
                {
            out.close();
                }

            }
            catch (IOException e)
            {
                throw new RuntimeException("Something went wrong : " + e.getMessage(), e);
            }
        }*/
    }
}
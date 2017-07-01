using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using System.Threading.Tasks;

namespace mdNote.Droid
{
    [Activity(Label = "mdNote.Android", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Android.Content.Intent.ActionSend }, Categories = new[] { Android.Content.Intent.CategoryDefault }, DataMimeType = "*/*")]
    [IntentFilter(new[] { Android.Content.Intent.ActionSend }, Categories = new[] { Android.Content.Intent.CategoryDefault }, DataMimeType = "text/plain")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const int REQUEST_CODE_OPEN_FILE = 2;
        public const int REQUEST_CODE_SAVE_FILE = 3;
        public const int REQUEST_CODE_CREATE_FILE = 4;

        public Android.Net.Uri CurrentUri { get; set; } = null;

        public void NewFile()
        {
            CurrentUri = null;
            mdNote.Pages.MainPage.Editor.CurrentPath = System.String.Empty;
            mdNote.Pages.MainPage.Editor.SavedContent = System.String.Empty;
        }

        private void PrepareIntent(Intent intent)
        {
            intent.AddCategory(Intent.CategoryOpenable);
            if (mdNote.Settings.UseMime)
                intent.SetType("text/markdown");
            else
                intent.SetType("*/*");
        }

        public void OpenFile()
        {
            var intent = new Intent(Intent.ActionOpenDocument);
            PrepareIntent(intent);
            StartActivityForResult(intent, REQUEST_CODE_OPEN_FILE);
        }

        public async void SaveFile()
        {
            if (CurrentUri == null)
                SaveFileAs();
            else
            {
                string newContent = await mdNote.Pages.MainPage.Editor.GetCurrentContentAsync();
                WriteFileContent(newContent);
                mdNote.Pages.MainPage.Editor.SaveContent(newContent);
            }
        }

        public void SaveFileAs()
        {
            var intent = new Intent(Intent.ActionCreateDocument);
            PrepareIntent(intent);
            intent.PutExtra(Intent.ExtraTitle, mdNote.Pages.MainPage.Editor.Title);
            StartActivityForResult(intent, REQUEST_CODE_SAVE_FILE);
        }

        private string ReadFileContent()
        {
            System.IO.Stream stream = null;
            System.IO.StreamReader reader = null;
            try
            {
                stream = ContentResolver.OpenInputStream(CurrentUri);
                reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                string textLine;
                while ((textLine = reader.ReadLine()) != null)
                {
                    builder.AppendLine(textLine);
                }
                reader.Close();
                stream.Close();
                return builder.ToString();
            } finally
            {
                if (reader != null) reader.Dispose();
                if (stream != null) stream.Dispose();
            }
        }

        private void WriteFileContent(string content)
        {
            System.IO.Stream stream = null;
            System.IO.StreamWriter writer = null;
            try
            {
                stream = ContentResolver.OpenOutputStream(CurrentUri);
                writer = new System.IO.StreamWriter(stream, System.Text.Encoding.UTF8);
                writer.Write(content);
                writer.Close();
                stream.Close();
            }
            finally
            {
                if (writer != null) writer.Dispose();
                if (stream != null) stream.Dispose();
            }
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode != Result.Ok) return;

            switch (requestCode)
            {
                case REQUEST_CODE_OPEN_FILE:
                    CurrentUri = data.Data;
                    mdNote.Pages.MainPage.Editor.CurrentPath = CurrentUri.Path;
                    mdNote.Pages.MainPage.Editor.SavedContent = ReadFileContent();
                    break;
                case REQUEST_CODE_SAVE_FILE:
                    CurrentUri = data.Data;
                    mdNote.Pages.MainPage.Editor.CurrentPath = CurrentUri.Path;
                    string newContent = await mdNote.Pages.MainPage.Editor.GetCurrentContentAsync();
                    WriteFileContent(newContent);
                    mdNote.Pages.MainPage.Editor.SaveContent(newContent);
                    break;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MyTheme);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            if (Intent.Action.Equals(Android.Content.Intent.ActionSend))
            {
                /*                CurrentUri = Intent.ClipData.GetItemAt(0).Uri;
                                mdNote.Pages.EditorPage.DefaultContent = ReadFileContent();*/

                mdNote.Pages.EditorPage.DefaultContent = Intent.GetStringExtra(Android.Content.Intent.ExtraText);
            }
            LoadApplication(new App());
        }
    }
}
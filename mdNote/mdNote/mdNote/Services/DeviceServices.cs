using Xamarin.Forms;

namespace mdNote.Services
{
    public class DeviceServices
    {
        public static void NewFile()
        {
            FileSystem.NewFile();
        }

        public static void OpenFile()
        {
            FileSystem.OpenFile();
        }

        public static void SaveFile()
        {
            FileSystem.SaveFile();
        }

        public static void SaveFileAs()
        {
            FileSystem.SaveFileAs();
        }
        
        public static async void TryExit()
        {
            if (await mdNote.Pages.MainPage.Editor.CheckModificationsAsync())
                ExitService.Exit();

        }

        public static IExitService ExitService = DependencyService.Get<IExitService>();
        public static IFileSystem FileSystem = DependencyService.Get<IFileSystem>();
        public static string BaseUrl = DependencyService.Get<IBaseUrl>().Get();
    }
}

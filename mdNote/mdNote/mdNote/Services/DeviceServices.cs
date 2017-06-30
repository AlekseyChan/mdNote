using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mdNote.Services
{
    public delegate void AddLocationEventHandler(string newFolder);

    public class DeviceServices
    {
        public static event AddLocationEventHandler OnAddLocation;
        public static IFileSystem FileSystem = DependencyService.Get<IFileSystem>();
        public static string BaseUrl = DependencyService.Get<IBaseUrl>().Get();

        public static void AddLocation()
        {
            FileSystem.SelectFolderAsync();
        }

        public static void LocationAdded(string newFolder)
        {
            OnAddLocation?.Invoke(newFolder);
        }
    }
}

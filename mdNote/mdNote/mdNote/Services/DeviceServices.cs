using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mdNote.Services
{
    public class DeviceServices
    {
        public static IFileSystem FileSystem = DependencyService.Get<IFileSystem>();
        public static string BaseUrl = DependencyService.Get<IBaseUrl>().Get();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mdNote.Services;
using Xamarin.Forms;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Plugin.CurrentActivity;

[assembly: Dependency(typeof(mdNote.Droid.FileSystem))]
namespace mdNote.Droid
{
    public class FileSystem : IFileSystem
    {
        public void NewFile()
        {
            ((MainActivity)CrossCurrentActivity.Current.Activity).NewFile();
        }

        public void OpenFile()
        {
            ((MainActivity)CrossCurrentActivity.Current.Activity).OpenFile();
        }

        public void SaveFile()
        {
            ((MainActivity)CrossCurrentActivity.Current.Activity).SaveFile();
        }

        public void SaveFileAs()
        {
            ((MainActivity)CrossCurrentActivity.Current.Activity).SaveFileAs();
        }
    }
}
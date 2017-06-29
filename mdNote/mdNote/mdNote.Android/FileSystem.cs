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
using mdNote.Models;
using System.Threading.Tasks;

[assembly: Dependency(typeof(mdNote.Droid.FileSystem))]
namespace mdNote.Droid
{
    public class FileSystem : IFileSystem
    {
        private static readonly string rootDir = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);

        public async Task<Models.File> GetLocationInfoAsync(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            return new Models.File
            {
                FileKind = FileKindEnum.Location,
                DisplayName = dirInfo.Name,
                Icon = Styles.Icons.Folder,
                Description = dirInfo.FullName,
                Path = dirInfo.FullName
            };
        }

        public async Task<string> ReadFileAsync(string fileName)
        {
            return System.IO.File.ReadAllText(fileName, Encoding.UTF8);
        }

        private async Task scanDirectory(DirectoryInfo dir, bool recursive, Action<Models.File> callbackFunction)
        {
            IEnumerable<DirectoryInfo> childDirs = dir.EnumerateDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo childDir in childDirs)
            {
                callbackFunction(new Models.File
                {
                    DisplayName = childDir.Name,
                    Description = childDir.FullName,
                    Path = childDir.FullName,
                    Icon = mdNote.Styles.Icons.Folder,
                    FileKind = FileKindEnum.Folder
                });
                if (recursive)
                    scanDirectory(childDir, true, callbackFunction);
            }

            IEnumerable<FileInfo> files = dir.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                callbackFunction(new Models.File
                {
                    DisplayName = file.Name,
                    Path = file.FullName,
                    Icon = Styles.Icons.File,
                    FileKind = FileKindEnum.Text
                });
            }

        }

        public async Task ScanLocationAsync(string parentPath, bool recursive, Action<Models.File> callbackFunction)
        {
            if (string.IsNullOrEmpty(parentPath))
            {
                Java.IO.File[] externalFilesDirs = Android.App.Application.Context.GetExternalFilesDirs(null);
                for (var i = 0; i < externalFilesDirs.Count(); i++)
                {
                    Java.IO.File dir = externalFilesDirs[i];
                    string fullInternalPath = dir.AbsolutePath; // the INTERNAL disk. This is writeable.
                    string internalPathRoot = fullInternalPath.Substring(0, fullInternalPath.IndexOf("Android") - 1);
                    DirectoryInfo rootDir = new DirectoryInfo(internalPathRoot);
                    Models.File file = new Models.File
                    {
                        DisplayName = rootDir.Name,
                        Description = string.IsNullOrEmpty(parentPath) ? rootDir.FullName : null,
                        Path = rootDir.FullName,
                        Icon = mdNote.Styles.Icons.Folder,
                        FileKind = FileKindEnum.Folder
                    };
                    if (i == 0)
                        file.DisplayName = "Device";
                    else
                        file.DisplayName = "sdcard" + (i - 1).ToString();
                    callbackFunction(file);
                    if (recursive)
                        await scanDirectory(rootDir, recursive, callbackFunction);
                }
            }
            else
            {
                DirectoryInfo currentDir = new DirectoryInfo(parentPath);
                await scanDirectory(currentDir, recursive, callbackFunction);
            }

        }

        public Task<string> SelectFolderAsync()
        {
            throw new NotImplementedException();
        }


        public async Task WriteFileAsync(string fileName, string content)
        {
            System.IO.File.WriteAllText(fileName, content, Encoding.UTF8);
        }
    }
}
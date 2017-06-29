using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mdNote.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(mdNote.UWP.FileSystem))]
namespace mdNote.UWP
{
    public class FileSystem : mdNote.Services.IFileSystem
    {
        public async Task<string> ReadFileAsync(string fileName)
        {
            var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(fileName);
            return await Windows.Storage.FileIO.ReadTextAsync(file, Windows.Storage.Streams.UnicodeEncoding.Utf8);
        }

        public async Task<Models.File> GetLocationInfoAsync(string path)
        {
            var folder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(path);
            return new Models.File()
            {
                FileKind = FileKindEnum.Location,
                DisplayName = folder.DisplayName,
                Icon = Styles.Icons.Folder,
                Description = folder.Path,
                Path = folder.Path
            };
        }

        public async Task<string> SelectFolderAsync()
        {
            Windows.Storage.Pickers.FolderPicker folderPicker = new Windows.Storage.Pickers.FolderPicker();

            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");
            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(folder);
                return folder.Path;
            }
            return null;
        }

        public async Task ScanLocationAsync(string parentPath, bool recursive, Action<File> callbackFunction)
        {
            Windows.Storage.StorageFolder folder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(parentPath);
            foreach (Windows.Storage.StorageFolder child in await folder.GetFoldersAsync())
            {
                callbackFunction(new Models.File
                {
                    DisplayName = child.DisplayName,
                    Description = string.IsNullOrEmpty(parentPath) ? child.Path : null,
                    Path = child.Path,
                    Icon = mdNote.Styles.Icons.Folder,
                    FileKind = FileKindEnum.Folder
                });
                if (recursive)
                {
                    await ScanLocationAsync(child.Path, true, callbackFunction);
                }
            }

            foreach (Windows.Storage.StorageFile child in await folder.CreateFileQueryWithOptions(getQuery()).GetFilesAsync())
            {
                callbackFunction(new Models.File
                {
                    DisplayName = child.Name,
                    Path = child.Path,
                    Icon = Styles.Icons.File,
                    FileKind = FileKindEnum.Text
                });
            }
        }

        public async Task WriteFileAsync(string fileName, string content)
        {
            var folder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(System.IO.Path.GetDirectoryName(fileName));
            var file = await folder.CreateFileAsync(System.IO.Path.GetFileName(fileName), Windows.Storage.CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(file, content, Windows.Storage.Streams.UnicodeEncoding.Utf8);
        }

        private Windows.Storage.Search.QueryOptions getQuery()
        {
            List<string> fileTypeFilter = new List<string>();
            /*            fileTypeFilter.Add(".md");
                        fileTypeFilter.Add(".txt");*/
            fileTypeFilter.Add("*");
            //TODO разобраться с ограничениями файловых типов
            return new Windows.Storage.Search.QueryOptions(Windows.Storage.Search.CommonFileQuery.DefaultQuery, fileTypeFilter);
        }

    }
}

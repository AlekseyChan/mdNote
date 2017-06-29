using System;
using System.IO;
using System.Threading.Tasks;

namespace mdNote.Services
{
    public interface IFileSystem
    {
        Task<string> ReadFileAsync(string fileName);
        Task WriteFileAsync(string fileName, string content);
        Task ScanLocationAsync(string parentPath, bool recursive, Action<mdNote.Models.File> callbackFunction);
        Task<string> SelectFolderAsync();
        Task<Models.File> GetLocationInfoAsync(string path);
        /*        Task<string> SelectFolderAsync();
                Task ScanLocationAsync(string parentPath, bool recursive, Action<File> callbackFunction);
                Task<Stream> GetStreamForFileAsync(string path);*/
    }
}

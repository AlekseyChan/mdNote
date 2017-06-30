using System;
using System.IO;
using System.Threading.Tasks;

namespace mdNote.Services
{
    public interface IFileSystem
    {
        void OpenFile();
        void SaveFile();
        void SaveFileAs();
        void NewFile();
    }
}

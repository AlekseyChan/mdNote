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

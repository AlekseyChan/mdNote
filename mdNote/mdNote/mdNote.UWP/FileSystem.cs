using System;
using System.Collections.Generic;
using Xamarin.Forms;

[assembly: Dependency(typeof(mdNote.UWP.FileSystem))]
namespace mdNote.UWP
{
    public class FileSystem : mdNote.Services.IFileSystem
    {
        public Windows.Storage.StorageFile CurrentFile { get; set; } = null;

        public async void OpenFile()
        {
            Windows.Storage.Pickers.FileOpenPicker picker = new Windows.Storage.Pickers.FileOpenPicker();

            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            picker.FileTypeFilter.Add("*");
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".md");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file!=null)
            {
                mdNote.Pages.MainPage.Editor.SavedContent = await Windows.Storage.FileIO.ReadTextAsync(file);
                mdNote.Pages.MainPage.Editor.CurrentPath = file.Path;
                CurrentFile = file;
            }
        }

        public async void SaveFile()
        {
            if (CurrentFile==null)
                SaveFileAs();
            else
            {
                string newContent = await mdNote.Pages.MainPage.Editor.GetCurrentContentAsync();
                await Windows.Storage.FileIO.WriteTextAsync(CurrentFile, newContent, Windows.Storage.Streams.UnicodeEncoding.Utf8);
                mdNote.Pages.MainPage.Editor.SaveContent(newContent);
            }
        }

        public async void SaveFileAs()
        {
            Windows.Storage.Pickers.FileSavePicker picker = new Windows.Storage.Pickers.FileSavePicker();

            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            picker.FileTypeChoices.Add("Markdown", new List<string>() { ".md" });
            picker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            Windows.Storage.StorageFile file = await picker.PickSaveFileAsync();
            if (file == null) return;

            mdNote.Pages.MainPage.Editor.CurrentPath = file.Path;
            string newContent = await mdNote.Pages.MainPage.Editor.GetCurrentContentAsync();
            await Windows.Storage.FileIO.WriteTextAsync(file, newContent, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            mdNote.Pages.MainPage.Editor.SaveContent(newContent);
            CurrentFile = file;
        }

        public async void SaveEditor()
        {
            Windows.Storage.Pickers.FileSavePicker picker = new Windows.Storage.Pickers.FileSavePicker();

            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            picker.FileTypeChoices.Add("html", new List<string>() { ".html" });
            Windows.Storage.StorageFile file = await picker.PickSaveFileAsync();
            if (file == null) return;

            string newContent = mdNote.Pages.MainPage.Editor.generateHtml();
            await Windows.Storage.FileIO.WriteTextAsync(file, newContent, Windows.Storage.Streams.UnicodeEncoding.Utf8);
        }

        public void NewFile()
        {
            CurrentFile = null;
            mdNote.Pages.MainPage.Editor.CurrentPath = System.String.Empty;
            mdNote.Pages.MainPage.Editor.RefreshSavedContent(System.String.Empty);
        }
    }
}

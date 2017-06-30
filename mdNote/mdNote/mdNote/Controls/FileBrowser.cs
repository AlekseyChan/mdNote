using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace mdNote.Controls
{
    public delegate void FileTapEventHandler(object sender, Models.File file, EventArgs e);

    public class FileBrowser : ListView, IDisposable
    {
        public event FileTapEventHandler OnTextTap;
        public event FileTapEventHandler OnUnknownTap;

        public Boolean AllowNewFavorities { get; set; }

        private bool isPathWritable = false;
        public bool IsPathWritable
        {
            get => isPathWritable;
            set
            {
                isPathWritable = value;
                OnPropertyChanged("IsPathWritable");
            }
        }

        private string currentPath = String.Empty;
        public string CurrentPath
        {
            get => currentPath;
            protected set
            {
                currentPath = value;
                OnPropertyChanged("CurrentPath");
                IsPathWritable = !string.IsNullOrWhiteSpace(currentPath);
            }
        }

        public bool IsInRoot { get { return string.IsNullOrEmpty(CurrentPath); } }

        public FileBrowser()
        {
            CurrentPath = null;

            HasUnevenRows = true;

            FileTemplateSelector templateSelector = new FileTemplateSelector();
            templateSelector.OnFolderClick += OnFolderClick;
            templateSelector.OnLocationClick += OnLocationClick;
            templateSelector.OnRemoveLocationClick += OnRemoveLocationClick;
            templateSelector.OnServiceClick += OnServiceClick;
            templateSelector.OnTextClick += OnTextClick;
            templateSelector.OnUnknownClick += OnUnknownClick;
            ItemTemplate = templateSelector;

            Services.DeviceServices.OnAddLocation += DeviceServices_OnAddLocation;
        }

        public void Dispose()
        {
            Services.DeviceServices.OnAddLocation -= DeviceServices_OnAddLocation;
        }

        private void DeviceServices_OnAddLocation(string newFolder)
        {
            if (string.IsNullOrEmpty(newFolder)) return;
            Settings.LibraryFolders = newFolder + ";" + Settings.LibraryFolders;
            ScanFolderAsync();
        }

        private void OnRemoveLocationClick(object sender, EventArgs e)
        {
            Settings.LibraryFolders = Settings.LibraryFolders.Replace(((Models.File)((MenuItem)sender).BindingContext).Path + ";", "");
            ScanFolderAsync();
        }

        protected Stack<string> history { get; private set; } = new Stack<string>();

        public async void ScanFolderAsync()
        {
            BeginRefresh();
            List<Models.File> catalog = new List<Models.File>();
            try
            {
                if (IsInRoot)
                {
                    catalog.Add(Models.File.AddLocation);
                    //TODO Это избранное, но добавить его можно только в Windows...надо придумать что-то получше
                    string[] folders = Settings.LibraryFolders.Split(';');
                    foreach (string folder in folders)
                    {
                        if (String.IsNullOrWhiteSpace(folder)) continue;
                        catalog.Add(await Services.DeviceServices.FileSystem.GetLocationInfoAsync(folder));
                    }
                }
                else
                {
                    catalog.Add(Models.File.ParentFolder);
                }

                if (!string.IsNullOrEmpty(CurrentPath))
                    await Services.DeviceServices.FileSystem.ScanLocationAsync(CurrentPath, false, (Models.File file) =>
                        {
                            catalog.Add(file);
                        });
            }
            finally
            {
                EndRefresh();
                ItemsSource = null;
                ItemsSource = catalog;
                GC.Collect();
            }
        }

        private Models.File getFileFromViewCell(object sender)
        {
            return ((Models.File)((ViewCell)sender).BindingContext);
        }

        private void OnUnknownClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnTextClick(object sender, EventArgs e)
        {
            Models.File file = getFileFromViewCell(sender);
            OnTextTap?.Invoke(sender, file, e);
        }

        private void OnServiceClick(object sender, EventArgs e)
        {
            Models.File file = getFileFromViewCell(sender);
            if (file.Path.Equals(".."))
            {
                if (history.Count == 0)
                    CurrentPath = null;
                else
                    CurrentPath = history.Pop();
                ScanFolderAsync();
            }
            if (file.Path.Equals("+"))
            {
                Services.DeviceServices.AddLocation();
            }
        }

        private void OnLocationClick(object sender, EventArgs e)
        {
            history.Push(CurrentPath);
            CurrentPath = getFileFromViewCell(sender).Path;
            ScanFolderAsync();
        }

        private void OnFolderClick(object sender, EventArgs e)
        {
            history.Push(CurrentPath);
            CurrentPath = getFileFromViewCell(sender).Path;
            ScanFolderAsync();
        }
    }
}
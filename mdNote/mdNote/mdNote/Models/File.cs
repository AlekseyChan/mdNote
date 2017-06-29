using System;
using System.Collections.Generic;
using System.Text;
using mdNote.Helpers;
using mdNote.Controls;

namespace mdNote.Models
{
    public enum FileKindEnum { Unknown, Folder, Location, Service, Text}

    public class File: ObservableObject
    {
        public static File ParentFolder = new Models.File
        {
            DisplayName = "..",
            Icon = mdNote.Styles.Icons.UpFolder,
            Description = "Go to parent folder",
            FileKind = Models.FileKindEnum.Service,
            Path = ".."
        };

        public static File AddLocation = new Models.File
        {
            DisplayName = "Add location",
            Icon = mdNote.Styles.Icons.OpenFolder,
            FileKind = Models.FileKindEnum.Service,
            Path = "+"
        };

        string displayName = string.Empty;
        public string DisplayName
        {
            get { return displayName; }
            set { SetProperty(ref displayName, value); }
        }

        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        string path = string.Empty;
        public string Path
        {
            get { return path; }
            set { SetProperty(ref path, value); }
        }

        string icon = string.Empty;
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }

        FileKindEnum fileKind = FileKindEnum.Unknown;
        public FileKindEnum FileKind
        {
            get { return fileKind; }
            set { SetProperty(ref fileKind, value); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using mdNote.Models;

namespace mdNote.Controls
{
    public class FileTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UnknownTemplate { get; set; }
        public DataTemplate ServiceTemplate { get; set; }
        public DataTemplate FolderTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate LocationTemplate { get; set; }

        public event EventHandler OnFolderClick;
        public event EventHandler OnTextClick;
        public event EventHandler OnLocationClick;
        public event EventHandler OnRemoveLocationClick;
        public event EventHandler OnServiceClick;
        public event EventHandler OnUnknownClick;

        public FileTemplateSelector()
        {
            FolderTemplate = new DataTemplate(() =>
            {
                FileButton folderButton = new FileButton();
                ViewCell folderCell = new ViewCell
                {
                    View = folderButton
                };
                folderCell.Tapped += (s, e) => { OnFolderClick?.Invoke(s, e); };
                return folderCell;
            });

            ServiceTemplate = new DataTemplate(() =>
            {
                FileButton serviceButton = new FileButton();
                ViewCell cell = new ViewCell()
                {
                    View = serviceButton
                };
                cell.Tapped += (s, e) => { OnServiceClick?.Invoke(s, e); };
                return cell;
            });

            LocationTemplate = new DataTemplate(() =>
            {
                FileButton locationButton = new FileButton();
                ViewCell locationCell = new ViewCell
                {
                    View = locationButton
                };

                MenuItem menu = new MenuItem()
                {
                    Text = "Remove location",
                };
                menu.Clicked += (s, e) => {
                    OnRemoveLocationClick?.Invoke(s, e);
                };

                locationCell.ContextActions.Add(menu);
                //locationCell.AddAction("more...", Icons.More, UI.Styles.MenuFontSize, ShowMenu, Color.Default);
                locationCell.Tapped += (s, e) =>
                {
                    OnLocationClick?.Invoke(s, e);
                };
                return locationCell;
            });

            TextTemplate = new DataTemplate(() =>
             {
                 FileButton fileButton = new FileButton();
                 ViewCell fileCell = new ViewCell
                 {
                     View = fileButton
                 };
                //fileCell.AddAction("more...", Icons.More, UI.Styles.MenuFontSize, ShowMenu, Color.Default);
                fileCell.Tapped += (s, e) => { OnTextClick?.Invoke(s, e); };
                 return fileCell;
             });

            UnknownTemplate = new DataTemplate(() =>
            {
                FileButton fileButton = new FileButton();
                ViewCell fileCell = new ViewCell
                {
                    View = fileButton
                };
                fileCell.Tapped += (s, e) =>
                {
                    OnUnknownClick?.Invoke(s, e);
                };
                return fileCell;
            });

        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item != null)
            {
                switch (((File)item).FileKind)
                {
                    case FileKindEnum.Folder:
                        return FolderTemplate;
                    case FileKindEnum.Location:
                        return LocationTemplate;
                    case FileKindEnum.Text:
                        return TextTemplate;
                    case FileKindEnum.Service:
                        return ServiceTemplate;
                }
            }
            return UnknownTemplate;
        }

    }
}

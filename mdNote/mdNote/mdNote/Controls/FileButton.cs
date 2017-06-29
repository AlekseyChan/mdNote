using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mdNote.Controls
{
    public class FileButton : IconButton
    {
        public FileButton()
        {
            Padding = mdNote.Styles.Styles.FileListPadding;
            FontSize = mdNote.Styles.Styles.FileListFontSize;
            this.SetBinding(FileButton.TextProperty, "DisplayName");
            this.SetBinding(FileButton.IconProperty, "Icon");
            this.SetBinding(FileButton.DetailProperty, "Comment");
        }
    }
}

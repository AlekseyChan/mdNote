using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mdNote.Models;

namespace mdNote.Models
{
    public enum IconMenuItemKind { Separator, Caption, Command, Entry, Custom };

    public class IconMenuItem : mdNote.Helpers.ObservableObject
    {
        private IconMenuItemKind kind = IconMenuItemKind.Command;
        public IconMenuItemKind Kind
        {
            get { return kind; }
            set { SetProperty(ref kind, value); }
        }

        private string text = string.Empty;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        string comment = string.Empty;
        public string Comment
        {
            get { return comment; }
            set { SetProperty(ref comment, value); }
        }

        string icon = string.Empty;
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }

        object userData = null;
        public object UserData
        {
            get { return userData; }
            set { SetProperty(ref userData, value); }
        }

        Action<object> command = null;
        public Action<object> Command
        {
            get { return command; }
            set { SetProperty(ref command, value); }
        }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value); }
        }
    }
}
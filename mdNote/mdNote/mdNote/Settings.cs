using System;
using System.Collections.Generic;
using System.Text;

namespace mdNote
{
    class Settings
    {
        public static string LibraryFolders
        {
            get
            {
                if (App.Current.Properties.ContainsKey("LibraryFolders"))
                    return (string)App.Current.Properties["LibraryFolders"];
                return "";
            }
            set
            {
                App.Current.Properties["LibraryFolders"] = value;
                App.Current.SavePropertiesAsync();
            }
        }

        public static bool GetValue(string key, bool defValue = true)
        {
            if (App.Current.Properties.ContainsKey(key))
                return (bool)App.Current.Properties[key];
            return defValue;
        }

        public static void SetValue(string key, bool value)
        {
            App.Current.Properties[key] = value;
            App.Current.SavePropertiesAsync();
        }

        public static bool AutoPreview
        {
            get => GetValue("AutoPreview", false);
            set => SetValue("AutoPreview", value);
        }
    }
}

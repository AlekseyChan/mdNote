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

        public static string GetValue(string key, string defValue = null)
        {
            if (App.Current.Properties.ContainsKey(key))
                return (string)App.Current.Properties[key];
            return defValue;
        }

        public static void SetValue(string key, object value)
        {
            App.Current.Properties[key] = value;
            App.Current.SavePropertiesAsync();
        }

        public static bool AutoPreview
        {
            get => GetValue("AutoPreview", false);
            set => SetValue("AutoPreview", value);
        }

        public static bool UseMime
        {
            get => GetValue("UseMime", false);
            set => SetValue("UseMime", value);
        }

        public static string FontFamily
        {
            get => GetValue("FontFamily", "sans-serif");
            set => SetValue("FontFamily", value);
        }

        public static string FontSize
        {
            get => GetValue("FontSize", "12pt");
            set => SetValue("FontSize", value);
        }

        public static string TextColor
        {
            get => GetValue("TextColor", "#000000");
            set => SetValue("TextColor", value);
        }

        public static string BackgroundColor
        {
            get => GetValue("BackgroundColor", "#c0c0c0");
            set => SetValue("BackgroundColor", value);
        }

        public static string ViewTextColor
        {
            get => GetValue("TextColor", "#000000");
            set => SetValue("TextColor", value);
        }

        public static string ViewBackgroundColor
        {
            get => GetValue("ViewBackgroundColor", "#fff6cc");
            set => SetValue("ViewBackgroundColor", value);
        }
    }
}

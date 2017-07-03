using Xamarin.Forms;

namespace mdNote.Styles
{
    public partial class Icons
    {
        public static string FontFamily
        {
            get
            {
                return Device.OnPlatform("FontAwesome", "fontawesome.ttf#FontAwesome", "Segoe MDL2 Assets");
            }
        }
        public static double ZoomFactor
        {
            get { return Device.OnPlatform(1, 1, 1.06); }
        }

        public static string Folder
        {
            get { return Device.OnPlatform("\uF114", "\uF114", "\uE8B7"); }
        }

        public static string Location
        {
            get { return Device.OnPlatform("\uF097", "\uF097", "\uE249"); }
        }
        public static string UpFolder
        {
            get { return Device.OnPlatform(null, "\uF148", "\uE8DA"); }
        }
        public static string Settings
        {
            get { return Device.OnPlatform("\uF013", "\uF013", "\uE713"); }
        }
        public static string More
        {
            get { return Device.OnPlatform(null, "\uF142", "\uE712"); }
        }
        public static string OpenFolder
        {
            get { return Device.OnPlatform(null, "\uF115", "\uED43"); }
        }
        public static string MoveToFolder
        {
            get { return Device.OnPlatform(null, null, "\uE8DE"); }
        }
        public static string Cancel
        {
            get { return Device.OnPlatform(null, "\uF00D", "\uE711"); }
        }

        public static string OpenFile
        {
            get { return Device.OnPlatform(null, "\uF115", "\uE1A5"); }
        }

        public static string NewFile
        {
            get { return Device.OnPlatform(null, "\uF016", "\uE160"); }
        }

        public static string File
        {
            get { return Device.OnPlatform(null, "\uF0F6", "\uE160"); }
        }

        public static string SaveFile
        {
            get { return Device.OnPlatform(null, "\uF0C7", "\uE105"); }
        }
        public static string SaveAsFile
        {
            get { return Device.OnPlatform(null, "\uF0C5", "\uE28F"); }
        }
        public static string Preview
        {
            get { return Device.OnPlatform(null, "\uF096", "\uE295"); }
        }
        public static string Edit { get { return Device.OnPlatform(null, "\uF044", "\uE104"); } }

        public static string Help { get => Device.OnPlatform(null, "\uF128", "\uE11B"); }

        public static string Info { get => Device.OnPlatform(null, "\uF129", "\uE946"); }
        public static string Exit { get => Device.OnPlatform(null, "\uF08B", "\uEF2C"); }
    }
}

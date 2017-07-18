using Xamarin.Forms;

namespace mdNote.Styles
{
    public partial class Icons
    {
        public static string FontFamily
        {
            get
            {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
                return Device.OnPlatform("FontAwesome", "fontawesome.ttf#FontAwesome", "Segoe MDL2 Assets");
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            }
        }
        public static double ZoomFactor
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(1, 1, 1.06); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }

        public static string Folder
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform("\uF114", "\uF114", "\uE8B7"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }

        public static string Location
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform("\uF097", "\uF097", "\uE249"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }
        public static string UpFolder
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(null, "\uF148", "\uE8DA"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }
        public static string Settings
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform("\uF013", "\uF013", "\uE713"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }
        public static string More
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(null, "\uF142", "\uE712"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }
        public static string OpenFolder
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(null, "\uF115", "\uED43"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }
        public static string MoveToFolder
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(null, null, "\uE8DE"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }
        public static string Cancel
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(null, "\uF00D", "\uE711"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }

        public static string OpenFile
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(null, "\uF115", "\uE1A5"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }

        public static string NewFile
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(null, "\uF016", "\uE160"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }

        public static string File
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(null, "\uF0F6", "\uE160"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }

        public static string SaveFile
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(null, "\uF0C7", "\uE105"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }
        public static string SaveAsFile
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(null, "\uF0C5", "\uE28F"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }
        public static string Preview
        {
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
            get { return Device.OnPlatform(null, "\uF096", "\uE295"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        }
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        public static string Edit { get { return Device.OnPlatform(null, "\uF044", "\uE104"); } }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'

#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        public static string Help { get => Device.OnPlatform(null, "\uF128", "\uE11B"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'

#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        public static string Info { get => Device.OnPlatform(null, "\uF129", "\uE946"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
#pragma warning disable CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
        public static string Exit { get => Device.OnPlatform(null, "\uF08B", "\uEF2C"); }
#pragma warning restore CS0618 // 'Device.OnPlatform<T>(T, T, T)" является устаревшим: 'Use switch(RuntimePlatform) instead.'
    }
}

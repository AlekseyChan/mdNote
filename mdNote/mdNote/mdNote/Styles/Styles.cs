using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mdNote.Styles
{
    //TODO Разделить на sizes и colors
    //TODO учесть в app.xaml
    //TODO сделать загрузку из файла настроек
    //TODOревизия всех файлов на предмет констант
    public static class Styles
    {
        #region sizes
        public static Thickness FileListPadding = new Thickness(20, 10);
        public static Thickness MenuPadding = new Thickness(20, 10);
        public static double FileListFontSize = 16;

        public static double DefaultButtonSize = Device.OnPlatform(40, 16, 16);
        public static double BigButtonSize = Device.OnPlatform(40, 16, 40);

        public static double TrackInfoFontSize = 14;

        public static double MenuFontSize = 15;
        public static double MenuIconGap = 27;
        public static double MenuCaptionFontSize = 10.5;

        public static Thickness ControlsPadding = new Thickness(20, 10);
        public static double ControlsSpacing = 10;
        //        public static Thickness ControlsPadding = new Thickness(16, 14);
        public static Thickness MenuSeparatorMargin = new Thickness(10, 0);
        #endregion

        #region colors
        public static Color ButtonColor = Color.Accent;

        public static Color TextAccentColor = Color.White;
        public static Color BackgroundAccentColor = Color.Accent.WithLuminosity(Color.Accent.Luminosity / 3);

        public static Color ToolbarColor = TextAccentColor;
        public static Color ToolbarBackgroundColor = BackgroundAccentColor;

        public static Color MenuBackgroundColor = Color.FromHex("004f88");
        public static Color MenuTextColor = Color.FromHex("fafafa");
        public static Color MenuSeparatorColor = Color.FromHex("808080");
        public static Color MenuCaptionBackgroundColor = Color.FromHex("004f88");
        public static Color MenuCaptionColor = Color.FromHex("C0C0C0");
        #endregion
    }
}

using Xamarin.Forms;                                                                                                     

namespace mdNote.Styles
{
    public class AppTitle
    {
        public static Thickness Padding = Device.OnPlatform(0, new Thickness(20, 110, 20, 10), Styles.Menu.ButtonPadding);
        public static Color TextColor = Color.WhiteSmoke;
        public static Color ShadowColor = Color.FromHex("51243E");
        public static Color BackgroundColor = Device.OnPlatform(Color.Default, Color.FromHex("B24F88"), Styles.Menu.BackgroundColor);
        public static double TextSize = 36;
        public static int ShadowTilt = 2;
    }
}

using Xamarin.Forms;

namespace mdNote.Styles
{
    public class AppTitle
    {
        public static Thickness Padding = new Thickness(10, 110, 10, 10);
        public static Color TextColor = Color.WhiteSmoke;
        public static Color ShadowColor = Color.FromHex("004f88");
        public static Color BackgroundColor = Color.FromHex("2080FF");
        public static double TextSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
        public static int ShadowTilt = 2;
    }
}

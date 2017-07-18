using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace mdNote.UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ShareTargetPage : Page
    {
        public ShareTargetPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.Frame.Navigate(typeof(MainPage));
/*            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                Pages.MainPage.Editor.SetTextAsync(e.Parameter.ToString());
            });
  */      }
    }
}

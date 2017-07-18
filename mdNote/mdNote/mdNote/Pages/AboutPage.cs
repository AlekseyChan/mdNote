using Xamarin.Forms;

namespace mdNote.Pages
{
    public class AboutPage : ContentPage
    {
        public const string AppSite = "https://aleksey.chan.github.io/mdNote";
        public AboutPage()
        {
            Title = "About...";
            Label label = new Label()
            {
                Text = AppSite,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0)
            };
            TapGestureRecognizer tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped +=(s,e)=> {
                Device.OpenUri(new System.Uri(AppSite));

            };
            label.GestureRecognizers.Add(tapRecognizer);
            Content = new StackLayout
            {
                Children = {
                    new mdNote.Controls.AppTitleView()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    },
                    new Label { Text = "Author: Chan Aleksey" ,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(0, 20, 0, 0)

                    },
                    label,
                }
            };
        }
    }
}
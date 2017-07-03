using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace mdNote.Pages
{
    public class AboutPage : ContentPage
    {
        public AboutPage()
        {
            Title = "About...";
            Label label = new Label();
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
                    new Label { Text = "https://github.com",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(0, 20, 0, 0)},
                }
            };
        }
    }
}
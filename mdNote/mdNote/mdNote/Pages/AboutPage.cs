using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace mdNote.Pages
{
	public class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			Content = new StackLayout {
				Children = {
                    new mdNote.Controls.AppTitleView()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    },
					new Label { Text = "Author: Chan Aleksey" },
                    new Label { Text = "Project page: https://github.com" },
                }
            };
		}
	}
}
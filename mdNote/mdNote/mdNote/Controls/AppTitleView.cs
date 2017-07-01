using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace mdNote.Controls
{
    public class AppTitleView : ContentView
    {
        public event EventHandler OnTap;
        private TapGestureRecognizer tapRecognizer;
        public AppTitleView()
        {
            BackgroundColor = Styles.AppTitle.BackgroundColor;
            Padding = Styles.AppTitle.Padding;
            Label shadow = new Label
            {
                FontSize = Styles.AppTitle.TextSize,
                Text = "mdNote",
                FontAttributes = FontAttributes.Bold,
                TextColor = Styles.AppTitle.ShadowColor,
                TranslationX = Styles.AppTitle.ShadowTilt,
                TranslationY = Styles.AppTitle.ShadowTilt
            };
            Label mainTitle = new Label
            {
                FontSize = Styles.AppTitle.TextSize,
                Text = "mdNote",
                FontAttributes = FontAttributes.Bold,
                TextColor = Styles.AppTitle.TextColor
            };
            

            Content = new Grid()
            {
                Children = { shadow, mainTitle }
            };

            tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += TapRecognizer_Tapped;
            GestureRecognizers.Add(tapRecognizer);            

        }

        private void TapRecognizer_Tapped(object sender, EventArgs e)
        {
            OnTap?.Invoke(this, e);
        }
    }

}
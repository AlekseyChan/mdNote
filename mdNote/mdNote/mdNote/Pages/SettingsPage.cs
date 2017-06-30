using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace mdNote.Pages
{
    public class SettingsPage : ContentPage
    {
        private Switch initSwitch(bool value)
        {
            return new Switch
            {
                IsToggled = value,
                Margin = new Thickness(0, Styles.Controls.ControlsSpacing)
            };
        }

        private Switch initSwitch(string key, bool defValue)
        {
            var result = new Switch
            {
                IsToggled = Settings.GetValue(key, defValue),
                Margin = new Thickness(0, Styles.Controls.ControlsSpacing),
            };
            result.Toggled += (s, e) => { Settings.SetValue(key, result.IsToggled); };
            return result;
        }

        public SettingsPage()
        {
            Title = "Settings";

            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = Styles.Controls.ControlsPadding,
                    Children = {
                        new Label { Text = "Automatic switch to preview mode" },
                        initSwitch("AutoPreview", false),
                    }
                }
            };
        }
    }
}
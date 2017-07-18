using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace mdNote.Pages
{
    public class SettingsPage : ContentPage
    {
        private Picker fontPicker;

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

        private Entry initEntry(string key, string defValue)
        {
            var result = new Entry()
            {
                Text = Settings.GetValue(key, defValue),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            result.TextChanged += (s, e) =>
            {

                Settings.SetValue(key, result.Text);
            };
            return result;
        }

        public SettingsPage()
        {
            Title = "Settings";

            var ControlsStack = new StackLayout
            {
                Padding = Styles.Controls.ControlsPadding,
            };
            ControlsStack.Children.Add(new Label { Text = "Automatic switch to preview mode" });
            ControlsStack.Children.Add(initSwitch("AutoPreview", Settings.AutoPreview));
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                ControlsStack.Children.Add(new Label { Text = "Use text/markdown MIME type" });
                ControlsStack.Children.Add(initSwitch("UseMime", Settings.UseMime));
            }
            fontPicker = new Picker()
            {
                Title = "Font family",
                ItemsSource = new List<string> { "serif", "sans-serif", "monospace" }
            };
            int i = fontPicker.ItemsSource.IndexOf(Settings.FontFamily);
            if (i >= 0)
                fontPicker.SelectedIndex = i;
            fontPicker.SelectedIndexChanged += FontPicker_SelectedIndexChanged;
            if (Device.RuntimePlatform.Equals(Device.Android))
                ControlsStack.Children.Add(new Label { Text = "Font family" });
            ControlsStack.Children.Add(fontPicker);

            ControlsStack.Children.Add(new Label { Text = "Font size" });
            ControlsStack.Children.Add(initEntry("FontSize", Settings.FontSize));

            ControlsStack.Children.Add(new Label { Text = "Edit mode text color" });
            ControlsStack.Children.Add(initEntry("TextColor", Settings.TextColor));

            ControlsStack.Children.Add(new Label { Text = "Edit mode background" });
            ControlsStack.Children.Add(initEntry("BackgroundColor", Settings.BackgroundColor));

            ControlsStack.Children.Add(new Label { Text = "View mode text color" });
            ControlsStack.Children.Add(initEntry("ViewTextColor", Settings.ViewTextColor));

            ControlsStack.Children.Add(new Label { Text = "View mode background" });
            ControlsStack.Children.Add(initEntry("ViewBackgroundColor", Settings.ViewBackgroundColor));

            Content = new ScrollView
            {
                Content = ControlsStack
            };
        }

        private void FontPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fontPicker.SelectedItem != null)
                Settings.FontFamily = fontPicker.SelectedItem.ToString();
        }
    }
}
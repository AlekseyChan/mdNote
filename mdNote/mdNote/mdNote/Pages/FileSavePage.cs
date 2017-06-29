using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mdNote.Controls;
using Xamarin.Forms;

namespace mdNote.Pages
{
    public class FileSavePage : ContentPage
    {

        FileBrowser browser;
        Entry fileNameEntry;
        Button saveButton;
        public Action<string> OnSelectFile { get; set; }

        public async void ApplySelectionAsync()
        {
            string fileName = fileNameEntry.Text;
            if (String.IsNullOrWhiteSpace(fileName)) return;
            if (String.IsNullOrWhiteSpace(System.IO.Path.GetExtension(fileName)))
                fileName += ".md";
            if (String.IsNullOrWhiteSpace(System.IO.Path.GetDirectoryName(fileName)))
                fileName = System.IO.Path.Combine(browser.CurrentPath, fileName);

            OnSelectFile?.Invoke(fileName);
        }

        public FileSavePage()
        {
            browser = new FileBrowser()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            browser.OnTextTap += Browser_OnTextTap;

            fileNameEntry = new Entry()
            {
                Placeholder = "Enter file name",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                
                Margin = new Thickness(5)
            };
            fileNameEntry.Completed += (s, e) => { ApplySelectionAsync(); };

            saveButton = new Button()
            {
                Text = "Save",
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(5)
            };
            saveButton.BindingContext = browser;
            saveButton.SetBinding(Button.IsEnabledProperty, "IsPathWritable");
            saveButton.Clicked += (s, e) =>
            {
                ApplySelectionAsync();
            };

            Content = new StackLayout
            {
                Children = {
                    new StackLayout{
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                            fileNameEntry,
                            saveButton
                        }
                    },
                    browser
                }
            };
            Title = "Save file as...";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            browser.ScanFolderAsync();
        }

        private async void Browser_OnTextTap(object sender, Models.File file, EventArgs e)
        {
            if (await DisplayAlert("Confirm action", file.DisplayName + " already exists. Replace it?", "Yes", "No"))
            {
                fileNameEntry.Text = file.DisplayName;
                ApplySelectionAsync();
            }
        }
    }
}
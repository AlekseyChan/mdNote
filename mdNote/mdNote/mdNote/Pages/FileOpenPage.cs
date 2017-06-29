using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mdNote.Controls;
using Xamarin.Forms;

namespace mdNote.Pages
{
	public class FileOpenPage : ContentPage
	{

        FileBrowser browser;
        public Action<string> OnSelectFile { get; set; }

		public FileOpenPage ()
		{
            browser = new FileBrowser() {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            browser.OnTextTap += Browser_OnTextTap;
            Content = browser;
            Title = "Open file...";
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            browser.ScanFolderAsync();
        }

        private void Browser_OnTextTap(object sender, Models.File file, EventArgs e)
        {
            OnSelectFile?.Invoke(file.Path);
        }
    }
}
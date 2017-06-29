using System;
using System.Collections.Generic;
using System.Linq;
using mdNote.Models;
using mdNote.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mdNote.Pages
{
    public class EditorPage : ContentPage
    {
        #region webview support
        Controls.AdvWebView webView;
        HtmlWebViewSource htmlSource;

        private Func<string, Task<string>> _evaluateJavascript;
        public Func<string, Task<string>> EvaluateJavascript
        {
            get { return _evaluateJavascript; }
            set { _evaluateJavascript = value; }
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.Equals("about:blank")) return;
            e.Cancel = true;
            Device.OpenUri(new Uri(e.Url));
        }

        private void RefreshWebView()
        {
            htmlSource.BaseUrl = DeviceServices.BaseUrl;
            htmlSource.Html = generateHtml();
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                webView.Focus();
                webView.Source = htmlSource;
            });
        }
        #endregion

        #region html generation
        private string generateStyles()
        {
            return @"
<style>
    body {
        font-size: 12pt;
        font-family: 'Proxima Nova','Arial','Helvetica Neue',sans-serif;
        padding: 0;
        margin: 0;
    }

    textarea {
        padding: 0;
        border: 0;
        width: 100%;
        height: 100%;
    }

    h1 {
        font-size: 16pt;
        font-weight: bold;
    }
    h2 {
        font-size: 14pt;
        font-weight: bold;
    }
    h3 {
        font-size: 12pt;
        font-weight: bold;
    }
    h4 {
        font-size: 10pt;
        font-weight: bold;
    }
    h5 {
        font-size: 8pt;
        font-weight: bold;
    }
</style>";
        }

        private string generateOptions()
        {
            return @"
            autoDownloadFontAwesome: false,
            autofocus: false,
            element: document.getElementById('editor'),
            promptURLs: false,
            spellChecker: false,
            toolbar: false,
            status: false," +
            "initialValue:" + ConvertedContent + ",";
        }

        private string generateHtml()
        {
            string basicHtml = @"
<html>

<head>
    <meta charset='utf-8'>
	<link rel='stylesheet' href='simplemde.min.css'>
    <script src = 'jquery.min.js'></script>
    <script src = 'simplemde.min.js'></script>
    <script src = 'highlight.min.js'></script>
	<link rel='stylesheet' href='github.min.css'>
    {0}
</head>

<body>
    <textarea id='editor' name='editor'></textarea>
    <script>
        var htmlDecode = function(value) {{
            return $('<div/>').html(value).text();
        }};

        var simplemde=new SimpleMDE({{
            {1}
        }});
        
        {2}
    </script>                           
</body>
</html>";
            var previewActivation = "";
            isPreview = false;
            if (Settings.AutoPreview)
            {
                previewActivation = "simplemde.togglePreview();";
                isPreview = true;
            }
            SyncPreviewMenu();

            var result = String.Format(basicHtml, generateStyles(), generateOptions(), previewActivation);

            return result;
        }
        #endregion

        #region Content
        private string currentPath = String.Empty;
        public string CurrentPath
        {
            get => currentPath;
            set
            {
                currentPath = value;
                Title = String.IsNullOrEmpty(currentPath) ? "untitled.md" : System.IO.Path.GetFileName(currentPath);
                ((MainPage)App.Current.MainPage).Detail.Title = Title;
            }
        }

        private string savedContent = String.Empty;
        public string SavedContent
        {
            get => savedContent;
            set
            {
                savedContent = value;
                RefreshWebView();
            }
        }

        public string ConvertedContent
        {
            get => "htmlDecode('" + System.Net.WebUtility.HtmlEncode(System.Text.RegularExpressions.Regex.Escape(savedContent)) + "')";
        }

        private bool isPreview = false;
        public bool IsPreview
        {
            get => isPreview;
            set
            {
                isPreview = value;
                SyncPreviewMenu();
            }
        }

        private async Task<string> GetCurrentContentAsync()
        {
            return await webView.TryEval("simplemde.value();");
        }

        private async Task<bool> IsModifiedAsync()
        {
            return SavedContent.Equals(await GetCurrentContentAsync());
        }

        #endregion

        #region file operations
        private FileOpenPage openDialog;
        public FileOpenPage OpenDialog
        {
            get
            {
                if (openDialog == null)
                {
                    openDialog = new FileOpenPage();
                    openDialog.OnSelectFile = (path) => { OpenFileAsync(path); };
                }
                return openDialog;
            }
        }

        private FileSavePage saveDialog;
        public FileSavePage SaveDialog
        {
            get
            {
                if (saveDialog == null)
                {
                    saveDialog = new FileSavePage();
                    saveDialog.OnSelectFile = (path) => { SaveFileAsAsync(path); };
                }
                return saveDialog;
            }
        }

        public async Task<bool> CheckModificationsAsync()
        {
            //TODO Здесь надо проеврять, быи ли изменения и предлагать их сохранять
            //            if (await IsModifiedAsync())
            //          {
            //if (!(await DisplayAlert(Title, "You have unsaved changes. Continue?", "Yes", "No"))
            //        }
            return true;
        }

        public async void NewFileAsync()
        {
            if (!await CheckModificationsAsync()) return;
            CurrentPath = String.Empty;
            SavedContent = String.Empty;
        }

        public async Task SetTextAsync(string newText)
        {
            if (!await CheckModificationsAsync()) return;
            CurrentPath = String.Empty;
            SavedContent = newText;

            await Navigation.PopToRootAsync();
        }

        public static string DefaultContent = string.Empty;

        public async Task OpenFileAsync(string path)
        {
            if (!await CheckModificationsAsync()) return;
            CurrentPath = path;
            SavedContent = await DeviceServices.FileSystem.ReadFileAsync(path);
            await Navigation.PopToRootAsync();
        }

        public async void SaveFileAsync()
        {
            if (String.IsNullOrEmpty(CurrentPath))
                await Navigation.PushAsync(SaveDialog, true);
            else
                await SaveFileAsAsync(CurrentPath);
        }

        public async Task SaveFileAsAsync(string path)
        {
            string newContent = await GetCurrentContentAsync();
            await DeviceServices.FileSystem.WriteFileAsync(path, newContent);
            savedContent = newContent;
            CurrentPath = path;
            await Navigation.PopToRootAsync();
        }
        #endregion

        #region Commands
        public void SyncPreviewMenu()
        {
            if (IsPreview)
            {
                PreviewCommand.Text = "Edit mode";
                PreviewCommand.Icon = Styles.Icons.Edit;
            }
            else
            {
                PreviewCommand.Text = "View mode";
                PreviewCommand.Icon = Styles.Icons.Preview;
            }
            MainPage.Menu.Refresh();
        }

        public IconMenuItem NewCommand { get; private set; }
        public IconMenuItem SaveCommand { get; private set; }
        public IconMenuItem SaveAsCommand { get; private set; }
        public IconMenuItem OpenCommand { get; private set; }
        public IconMenuItem PreviewCommand { get; private set; }

        public void InitCommands()
        {
            NewCommand = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Command,
                Text = "New",
                Icon = Styles.Icons.File,
                Command = (o) => { NewFileAsync(); }
            };

            OpenCommand = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Command,
                Text = "Open...",
                Icon = Styles.Icons.OpenFile,
                Command = (o) => { Navigation.PushAsync(OpenDialog, true); }
            };

            SaveCommand = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Command,
                Text = "Save",
                Icon = Styles.Icons.SaveFile,
                Command = (o) => { SaveFileAsync(); }
            };

            SaveAsCommand = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Command,
                Text = "Save as...",
                Icon = Styles.Icons.SaveAsFile,
                Command = (o) => { Navigation.PushAsync(SaveDialog, true); }
            };

            PreviewCommand = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Command,
                Text = "View mode",
                Icon = Styles.Icons.Preview,
                Command = (o) =>
                {
                    IsPreview = !IsPreview;
                    if (isPreview)
                        webView.TryEval("if (!simplemde.isPreviewActive()) simplemde.togglePreview();");
                    else
                        webView.TryEval("if (simplemde.isPreviewActive()) simplemde.togglePreview();");
                    SyncPreviewMenu();
                }
            };
        }
        #endregion

        public EditorPage()
        {
            InitCommands();

            webView = new Controls.AdvWebView();
            webView.Source = "about:blank";

            webView.Navigating += WebView_Navigating;

            htmlSource = new HtmlWebViewSource();

            this.Padding = 0;
            savedContent = DefaultContent;
            Content = webView;
            Title = "untitled.md";
            this.Focused += EditorPage_Focused;
        }

        private void EditorPage_Focused(object sender, FocusEventArgs e)
        {
            webView.Focus();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!webView.Source.Equals(htmlSource))
                RefreshWebView();
            MainPage.Menu.Refresh();
            webView.Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect();
        }
    }
}
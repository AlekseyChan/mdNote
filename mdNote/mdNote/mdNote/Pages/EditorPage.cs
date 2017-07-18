using System;
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

        public void RefreshWebView()
        {
            htmlSource.BaseUrl = DeviceServices.BaseUrl;
            htmlSource.Html = generateHtml();
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                webView.Source = null;
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
        font-size: " + Styles.Editor.FontSize + @";
        font-family: " + Styles.Editor.FontFamily + @";
        padding: 0;
        margin: 0;
        background: " + Styles.Editor.BackgroundColor + @";
        color: " + Styles.Editor.TextColor + @";
    }

    td {
        font-size: " + Styles.Editor.FontSize + @";
        font-family: " + Styles.Editor.FontFamily + @";
    }

    textarea {
        padding: 0;
        border: 0;
        width: 100%;
        height: 100%;
        background: " + Styles.Editor.BackgroundColor + @";
        color: " + Styles.Editor.TextColor + @";
    }

    .CodeMirror {
        background: " + Styles.Editor.BackgroundColor + @";
        color: " + Styles.Editor.TextColor + @";
        border: 0;
    }

    .editor-preview,.editor-preview-side{
        background: " + Styles.Editor.ViewBackgroundColor + @";
        color: " + Styles.Editor.ViewTextColor + @";
    }

    h1 {
        font-size: " + Styles.Editor.H1Size + @";
        font-weight: bold;
    }
    .CodeMirror .CodeMirror-code .cm-header-1 {
        font-size: " + Styles.Editor.H1Size + @";
        line-height: " + Styles.Editor.H1Size + @";
    }
    h2 {
        font-size: " + Styles.Editor.H2Size + @";
        font-weight: bold;
    }
    .CodeMirror .CodeMirror-code .cm-header-2 {
        font-size: " + Styles.Editor.H2Size + @";
        line-height: " + Styles.Editor.H2Size + @";
    }
    h3 {
        font-size: " + Styles.Editor.H3Size + @";
        font-weight: bold;
    }
    .CodeMirror .CodeMirror-code .cm-header-3 {
        font-size: " + Styles.Editor.H3Size + @";
        line-height: " + Styles.Editor.H3Size + @";
    }
    h4 {
        font-size: " + Styles.Editor.H4Size + @";
        font-weight: bold;
    }
    .CodeMirror .CodeMirror-code .cm-header-4 {
        font-size: " + Styles.Editor.H4Size + @";
        line-height: " + Styles.Editor.H4Size + @";
    }
    h5 {
        font-size: " + Styles.Editor.H5Size + @";
        font-weight: bold;
    }
    .CodeMirror .CodeMirror-code .cm-header-5 {
        font-size: " + Styles.Editor.H5Size + @";
        line-height: " + Styles.Editor.H5Size + @";
    }
</style>";
        }

        private string generateOptions()
        {
            return @"
            forceSync: true,
            autoDownloadFontAwesome: false,
            autofocus: false,
            element: document.getElementById('editor'),
            promptURLs: false,
            spellChecker: false,
            toolbar: false,
            status: false," +
            "initialValue:" + ConvertedContent + ",";
        }

        public string generateHtml()
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

        public void RefreshSavedContent(string newValue) {
            savedContent = newValue;
            RefreshWebView();
        }

        public string ConvertedContent
        {
            get => String.IsNullOrEmpty(savedContent) ? "''" :
                "htmlDecode('" + System.Net.WebUtility.HtmlEncode(System.Text.RegularExpressions.Regex.Escape(savedContent)) + "')";
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

        public async Task<string> GetCurrentContentAsync()
        {
            return await webView.TryEval("simplemde.value();");
        }

        private async Task<bool> IsModifiedAsync()
        {
            string currentContent = await GetCurrentContentAsync();
            return !SavedContent.Equals(currentContent);
        }

        public void SaveContent(string newContent)
        {
            savedContent = newContent;
        }

        #endregion

        #region file operations
        public async Task<bool> CheckModificationsAsync()
        {
            if (await IsModifiedAsync())
            {
                if (await DisplayAlert("Unsaved changes", "Are you sure you want to continue? You will lose any unsaved changes", "Continue", "Cancel"))
                    return true;
                else
                    return false;
            }

            return true;
        }

        public async Task SetTextAsync(string newText)
        {
            if (!await CheckModificationsAsync()) return;
            CurrentPath = String.Empty;
            SavedContent = newText;

            await Navigation.PopToRootAsync();
        }

        public static string DefaultContent = string.Empty;

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
                Command = async (o) =>
                {
                    if (await CheckModificationsAsync())
                        DeviceServices.NewFile();
                }
            };

            OpenCommand = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Command,
                Text = "Open...",
                Icon = Styles.Icons.OpenFile,
                Command = async (o) =>
                {
                    if (await CheckModificationsAsync())
                        DeviceServices.OpenFile();
                }
            };

            SaveCommand = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Command,
                Text = "Save",
                Icon = Styles.Icons.SaveFile,
                Command = (o) =>
                {
                    DeviceServices.SaveFile();
                }
            };

            SaveAsCommand = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Command,
                Text = "Save as...",
                Icon = Styles.Icons.SaveAsFile,
                Command = (o) => { DeviceServices.SaveFileAs(); }
            };

            PreviewCommand = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Command,
                Text = "View mode",
                Icon = Styles.Icons.Preview,
                Command = async (o) =>
                {
                    IsPreview = !IsPreview;
                    if (isPreview)
                        await webView.TryEval("if (!simplemde.isPreviewActive()) simplemde.togglePreview();");
                    else
                        await webView.TryEval("if (simplemde.isPreviewActive()) simplemde.togglePreview();");
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

        protected override bool OnBackButtonPressed()
        {
            if (Navigation.NavigationStack.Count() > 1) return base.OnBackButtonPressed();
            DeviceServices.TryExit();
            return false;
        }
        protected override void OnDisappearing()
        {
                base.OnDisappearing();
        }
    }
}
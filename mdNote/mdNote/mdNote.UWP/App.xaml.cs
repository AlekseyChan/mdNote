using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace mdNote.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        private void InitXamarin(IActivatedEventArgs e)
        {
            /*List<Assembly> assembliesToInclude = new List<Assembly>();

            assembliesToInclude.Add(typeof(mdNote.UWP.BaseUrl).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(mdNote.UWP.FileSystem).GetTypeInfo().Assembly);

            Xamarin.Forms.Forms.Init(e, assembliesToInclude);*/

            Xamarin.Forms.Forms.Init(e);

            Xamarin.Forms.DependencyService.Register<mdNote.UWP.BaseUrl>();
            Xamarin.Forms.DependencyService.Register<mdNote.UWP.ExitService>();
            Xamarin.Forms.DependencyService.Register<mdNote.UWP.FileSystem>();
        }

        private async void OpenFile(Windows.Storage.StorageFile file)
        {
            mdNote.Pages.MainPage.Editor.SavedContent = await Windows.Storage.FileIO.ReadTextAsync(file, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            mdNote.Pages.MainPage.Editor.CurrentPath = file.Path;
            ((FileSystem)mdNote.Services.DeviceServices.FileSystem).CurrentFile = file;
        }

        protected async void OpenFile(string fileName)
        {
            if (String.IsNullOrEmpty(fileName)) return;
            OpenFile(await Windows.Storage.StorageFile.GetFileFromPathAsync(fileName.Replace("\"", "")));
        }

        protected void ActivationSequence(IActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                InitXamarin(e);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e);
            }
            // Ensure the current window is active
            
            Window.Current.Activate();
            
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            ActivationSequence(e);
            if (!String.IsNullOrEmpty(e.Arguments))
                OpenFile(e.Arguments);
        }

        protected override async void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            if (args.Kind == ActivationKind.ShareTarget)
            {
                args.ShareOperation.ReportStarted();
                string Text = String.Empty;

                if (args.ShareOperation.Data.Contains(StandardDataFormats.WebLink))
                    Text = (await args.ShareOperation.Data.GetWebLinkAsync()).ToString();
                if (args.ShareOperation.Data.Contains(StandardDataFormats.Text))
                    Text = (await args.ShareOperation.Data.GetTextAsync()).ToString();
                /*if (args.ShareOperation.Data.Contains(StandardDataFormats.Html))
                    Text = (await args.ShareOperation.Data.GetHtmlFormatAsync()).ToString();*/

                if (Pages.MainPage.Editor == null)
                {
                    InitXamarin(args);

                    var rootFrame = new Frame();
                    Pages.EditorPage.DefaultContent = Text;
                    rootFrame.Navigate(typeof(MainPage));
                    Window.Current.Content = rootFrame;
                    Window.Current.Activate();
                }
                else
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        Pages.MainPage.Editor.SetTextAsync(Text);
                    });
                    args.ShareOperation.ReportCompleted();
                }


            }
        }

        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            base.OnFileActivated(args);

            if (Window.Current.Content == null)
                ActivationSequence(args);

            foreach (Windows.Storage.IStorageItem file in args.Files)
            {
                OpenFile(file as Windows.Storage.StorageFile);
            }
        }
        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}

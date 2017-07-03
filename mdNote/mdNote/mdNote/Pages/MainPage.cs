using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mdNote.Styles;
using Xamarin.Forms;

namespace mdNote.Pages
{
    public class MainPage : MasterDetailPage
    {
        public static EditorPage Editor { get; set; }
        public static Controls.MenuView Menu { get; set; }
        //TODO Check using as static
        public static NavigationPage Navigator;
        public SettingsPage Settings { get; set; } = null;
        public AboutPage About { get; set; } = null;
        public HelpPage HelpPage { get; set; } = null;
        public Controls.AppTitleView AboutCommand { get; set; }

        public MainPage()
        {
            Editor = new EditorPage();

            Menu = new Controls.MenuView();
            AboutCommand = new Controls.AppTitleView();
            AboutCommand.OnTap += (s, e) =>
            {
                if (About == null) About = new AboutPage();
                Detail.Navigation.PushAsync(About);
            };
            if (Device.RuntimePlatform.Equals(Device.Android))
                Menu.SetHeader(AboutCommand);
            Menu.AddMenuItem(Editor.NewCommand);
            Menu.AddMenuItem(Editor.OpenCommand);
            Menu.AddMenuItem(Editor.SaveCommand);
            Menu.AddMenuItem(Editor.SaveAsCommand);
            Menu.AddSeparator();
            Menu.AddMenuItem(Editor.PreviewCommand);
            Menu.AddSeparator();
            Menu.AddMenuItem("Settings", null, Icons.Settings, (o) =>
            {
                if (Settings == null) Settings = new SettingsPage();
                Detail.Navigation.PushAsync(Settings);
            }, null);
            Menu.AddSeparator();
            Menu.AddMenuItem("Markdown help", null, Icons.Help, (o) =>
            {
                if (HelpPage == null) HelpPage = new HelpPage();
                Detail.Navigation.PushAsync(HelpPage);
            }, null);
            Menu.AddMenuItem("About...", null, Icons.Info, (o) =>
            {
                if (About == null) About = new AboutPage();
                Detail.Navigation.PushAsync(About);
            }, null);
            Menu.AddSeparator();
            Menu.AddMenuItem("Exit", null, Icons.Exit, (o) =>
            {
                Services.DeviceServices.TryExit();
            }, null);

            Menu.OnMenuTap += (s, e) => { IsPresented = false; };

            Master = new ContentPage { Title = "mdNote", Content = Menu };
            Navigator = new NavigationPage(Editor);
            Navigator.Pushed += (s, e) => { IsPresented = false; };
            Navigator.Popped += (s, e) => { IsPresented = false; };
            Detail = Navigator;
            MasterBehavior = MasterBehavior.Popover;
        }

    }
}
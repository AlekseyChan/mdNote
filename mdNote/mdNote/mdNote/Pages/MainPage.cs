using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using mdNote.Styles;
using mdNote.Models;

namespace mdNote.Pages
{
    public class MainPage : MasterDetailPage
    {
        public static EditorPage Editor { get; set; }
        public static Controls.MenuView Menu { get; set; }
        public SettingsPage Settings { get; set; } = null;

        public static NavigationPage Navigator;

        public MainPage()
        {

            Editor = new EditorPage();

            Menu = new Controls.MenuView();
            Menu.AddMenuItem(Editor.NewCommand);
            Menu.AddMenuItem(Editor.OpenCommand);
            Menu.AddMenuItem(Editor.SaveCommand);
            Menu.AddMenuItem(Editor.SaveAsCommand);
            Menu.AddSeparator();
            Menu.AddMenuItem(Editor.PreviewCommand);
            Menu.AddSeparator();
            Menu.AddMenuItem(Editor.InitViewCommand);
            Menu.AddMenuItem("Settings", null, Icons.Settings, (o) =>
                {
                    if (Settings == null) Settings = new SettingsPage();
                    Detail.Navigation.PushAsync(Settings);
                }, null);

            Master = new ContentPage { Content = Menu, Title = "mdNote" };
            Navigator = new NavigationPage(Editor);
            Navigator.Pushed += (s, e) => { IsPresented = false; };
            Navigator.Popped += (s, e) => { IsPresented = false; };
            Detail = Navigator;
            MasterBehavior = MasterBehavior.Popover;
            Menu.OnMenuTap += (s, e) => { IsPresented = false; };
        }
    }
}
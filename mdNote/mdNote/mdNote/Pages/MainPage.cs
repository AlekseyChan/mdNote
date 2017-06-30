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
            Menu.AddMenuItem("Settings", null, Icons.Settings, (o) =>
            {
                if (Settings == null) Settings = new SettingsPage();
                Detail.Navigation.PushAsync(Settings);
            }, null);
            Menu.AddMenuItem("Permissions", null, Icons.Settings, async (o) =>
            {
                /*var status = await Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)*/
                    var status = (await Plugin.Permissions.CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Storage))[Plugin.Permissions.Abstractions.Permission.Storage];
                await DisplayAlert("Results", status.ToString(), "OK");
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
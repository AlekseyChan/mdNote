using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mdNote.Models;
using Xamarin.Forms;

namespace mdNote.Controls
{
    public class MenuView : ContentView
    {
        public event EventHandler OnMenuTap;
        #region layout
        private StackLayout fixedHeader;
        private StackLayout fixedFooter;

        protected ListView menuView;
        #endregion

        #region menu editor
        public List<IconMenuItem> MenuItems { get; private set; }

        public void AddViewToFixedHeader(View view)
        {
            fixedHeader.Children.Add(view);
        }

        public void AddViewToFixedFooter(View view)
        {
            fixedFooter.Children.Add(view);
        }

        public void Refresh()
        {
            BeginMenuChange();
            CommitMenuChange();
        }

        public void BeginMenuChange()
        {
            menuView.ItemsSource = null;
        }

        public void CommitMenuChange()
        {
            menuView.ItemsSource = MenuItems;
        }

        public IconMenuItem AddSeparator()
        {
            IconMenuItem item = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Separator
            };
            MenuItems.Add(item);
            return item;
        }

        public IconMenuItem AddSeparator(string title)
        {
            IconMenuItem item = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Caption,
                Text = title
            };
            MenuItems.Add(item);
            return item;
        }

        public IconMenuItem AddMenuItem(string title, string comment, string icon, Action<object> command, object userData)
        {
            IconMenuItem item = new IconMenuItem()
            {
                Kind = IconMenuItemKind.Command,
                Text = title,
                Comment = comment,
                Icon = icon,
                Command = command,
                UserData = userData
            };

            MenuItems.Add(item);
            return item;
        }

        public void AddMenuItem(IconMenuItem item)
        {
            MenuItems.Add(item);
        }

        #endregion

        public MenuView()
        {
            MenuItems = new List<IconMenuItem>();

            menuView = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = new mdNote.Controls.IconMenuTemplateSelector(),
                ItemsSource = MenuItems
            };
            menuView.ItemTapped += MenuView_ItemTapped;

            fixedHeader = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, Device.OnPlatform(40, 20, 0), 0, 0)
            };
            fixedFooter = new StackLayout { Orientation = StackOrientation.Vertical };

            Grid rootLayout = new Grid
            {
                RowDefinitions = {
                    new RowDefinition{ Height = GridLength.Auto },
                    new RowDefinition { },
                    new RowDefinition{ Height = GridLength.Auto}
                }
            };
            rootLayout.Children.Add(fixedHeader, 0, 0);
            rootLayout.Children.Add(menuView, 0, 1);
            rootLayout.Children.Add(fixedFooter, 0, 2);

            Content = rootLayout;
        }

        private void MenuView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            IconMenuItem item = (IconMenuItem)e.Item;
            if ((item == null) || (item.Kind == IconMenuItemKind.Separator)) return;
            item?.Command(item.UserData);
            OnMenuTap?.Invoke(sender, e);
        }
    }
}
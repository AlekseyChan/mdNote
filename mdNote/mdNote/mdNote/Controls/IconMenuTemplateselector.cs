using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using mdNote.Models;

namespace mdNote.Controls
{
    public class IconMenuTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SeparatorTemplate { get; set; }
        public DataTemplate CommandTemplate { get; set; }
        public DataTemplate CaptionTemplate { get; set; }
        public DataTemplate EntryTemplate { get; set; }
        public DataTemplate CustomTemplate { get; set; }

        public IconMenuTemplateSelector()
        {
            CommandTemplate = new DataTemplate(() =>
            {
                IconButton command = new IconButton();
                command.SetBinding(IconButton.TextProperty, "Text");
                command.SetBinding(IconButton.DetailProperty, "Comment");
                command.SetBinding(IconButton.IconProperty, "Icon");
                command.SetBinding(IconButton.IsVisibleProperty, "IsEnabled");

                command.TextColor = Styles.Styles.MenuTextColor;
                command.FontSize = 15;
                command.IconGap = 27;
                command.FixedIconGap = false;
                command.Padding = new Thickness(16, 14);
                return new ViewCell { View = command };
            });
            SeparatorTemplate = new DataTemplate(() =>
            {
                return new ViewCell
                {
                    View = new BoxView
                    {
                        HeightRequest = 1,
                        BackgroundColor = Styles.Styles.MenuSeparatorColor,
                        Margin = new Thickness(5, 0, 5, 0)
                    }
                };
            });
            CaptionTemplate = new DataTemplate(() =>
            {
                StackLayout separator = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };
                Label caption = new Label
                {
                    FontSize = 15,
                    Margin = new Thickness(5, 10, 5, 0)
                };
                caption.SetBinding(Label.TextProperty, "Text");
                BoxView line = new BoxView
                {
                    HeightRequest = 1,
                    BackgroundColor = Color.Black,
                    Margin = new Thickness(5, 0, 5, 0)
                };
                separator.Children.Add(caption);
                separator.Children.Add(line);
                return new ViewCell { View = separator };
            });
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item != null)
            {
                switch (((IconMenuItem)item).Kind)
                {
                    case IconMenuItemKind.Command:
                        return CommandTemplate;
                    case IconMenuItemKind.Separator:
                        return SeparatorTemplate;
                    case IconMenuItemKind.Caption:
                        return CaptionTemplate;
                    case IconMenuItemKind.Entry:
                        return EntryTemplate;
                    case IconMenuItemKind.Custom:
                        return CustomTemplate;
                }
            }
            return null;
        }
    }
}

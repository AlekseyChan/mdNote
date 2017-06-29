using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mdNote.Styles;

namespace mdNote.Controls
{
    public class IconButton : ContentView
    {
        #region appearance properties
        private double fontSize = 15;
        public double FontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
                updateControls();
            }
        }

        public new Thickness Padding
        {
            get { return gridLayout.Padding; }
            set { gridLayout.Padding = value; }
        }

        private double detailsFactor = 0.70;
        public double DetailsFactor
        {
            get { return detailsFactor; }
            set
            {
                detailsFactor = value;
                updateControls();
            }
        }

        private bool fixedIconGap = false;
        public bool FixedIconGap
        {
            get { return fixedIconGap; }
            set
            {
                fixedIconGap = value;
                updateControls();
            }
        }

        private double iconGap = 27;
        public double IconGap
        {
            get { return iconGap; }
            set
            {
                iconGap = value;
                updateControls();
            }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(IconButton), Color.Default);
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set
            {
                SetValue(TextColorProperty, value);
                textLabel.TextColor = value;
                detailLabel.TextColor = value;
                iconLabel.TextColor = value;
            }
        }
        #endregion

        #region data properties
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(IconButton), null);
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                updateControls();
            }
        }

        public static readonly BindableProperty DetailProperty = BindableProperty.Create("Detail", typeof(string), typeof(IconButton), null);
        public string Detail
        {
            get { return (string)GetValue(DetailProperty); }
            set
            {
                SetValue(DetailProperty, value);
                updateControls();
            }
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create("Icon", typeof(string), typeof(IconButton), null);
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set
            {
                SetValue(IconProperty, value);
                updateControls();
            }
        }
        #endregion

        #region controls
        protected Label iconLabel;
        protected Label textLabel;
        protected Label detailLabel;
        protected Grid gridLayout;

        protected virtual void updateControls()
        {
            iconLabel.FontSize = fontSize * Icons.ZoomFactor;
            textLabel.FontSize = fontSize;
            detailLabel.FontSize = fontSize * detailsFactor;
            detailLabel.IsVisible = !string.IsNullOrEmpty(Detail);

            iconLabel.Text = Icon;
            textLabel.Text = Text;
            detailLabel.Text = Detail;

            if (!fixedIconGap)
                gridLayout.ColumnSpacing = fontSize;
            else
                gridLayout.ColumnSpacing = iconGap;
        }
        #endregion

        public IconButton()
        {
            iconLabel = new Label()
            {
                FontFamily = Icons.FontFamily,
                VerticalOptions = LayoutOptions.Center
            };

            textLabel = new Label()
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                LineBreakMode = LineBreakMode.NoWrap
            };

            detailLabel = new Label()
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                LineBreakMode = LineBreakMode.NoWrap
            };

            gridLayout = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { }
                },
                RowDefinitions = {
                    new RowDefinition {Height = GridLength.Auto },
                },
            };
            gridLayout.Children.Add(iconLabel, 0, 0);
            StackLayout labelsStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { textLabel, detailLabel },
            };
            gridLayout.Children.Add(labelsStack, 1, 0);
            Content = gridLayout;
            updateControls();
        }

        protected override void OnBindingContextChanged()
        {
            updateControls();
            base.OnBindingContextChanged();
        }
    }
}

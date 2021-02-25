using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WebClient
{
    public class Item
    {
        public Grid Body => body;
        public DataElements Data
        {
            get => data;
            set
            {
                image.Source = value.Image;
                //for (int index = 0; index < value.Images.Length - buttons.Count; index++)
                //{
                //    Button button = new Button
                //    {
                //        Height = 10,
                //        Width = 10,
                //        Margin = new System.Windows.Thickness(2, 0, 2, 0)
                //    };
                //    contentButtons.Children.Add(button);
                //    buttons.Add(button);
                //    index = 0;
                //}
                //for (int index = 0; index < buttons.Count - value.Images.Length; index++)
                //{
                //    Button button = buttons[0];
                //    contentButtons.Children.Remove(button);
                //    buttons.RemoveAt(0);
                //    index = 0;
                //}
                //for (int index = 0; index < value.Images.Length; index++)
                //{
                //    Button button = buttons[index];
                //    ImageSource imageSource = value.Images[index];
                //    ///TODO: 100% будет ошибка
                //    button.Click += (object sender, RoutedEventArgs e) =>
                //    {
                //        if (selected != null)
                //        {
                //            selected.IsEnabled = true;
                //        }
                //        button.IsEnabled = false;
                //        //image.Source = imageSource;
                //    };
                //}
                text.Text = $"{value.Title}\n" +
                    $"{value.Price} {value.Currency}";
                data = value;
            }
        }

        private DataElements data;
        private Button selected = null;
        
        private readonly Grid body = null;
        private readonly Image image = null;
        private readonly StackPanel contentButtons = null;
        private readonly TextBlock text = null;
        private readonly List<Button> buttons = null;

        public Item(DataElements data)
        {
            body = new Grid();
            {
                body.Children.Add
                (
                    new Grid
                    {
                        Background = Brushes.Black
                    }
                );

                Grid markup = new Grid();
                {
                    markup.ColumnDefinitions.Add(new ColumnDefinition { Width = new System.Windows.GridLength(1) });
                    markup.ColumnDefinitions.Add(new ColumnDefinition());
                    markup.ColumnDefinitions.Add(new ColumnDefinition { Width = new System.Windows.GridLength(1) });

                    markup.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1) });
                    markup.RowDefinitions.Add(new RowDefinition());
                    markup.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1) });

                    Grid background = new Grid();
                    {
                        background.Background = Brushes.White;
                        Grid.SetRow(background, 1);
                        Grid.SetColumn(background, 1);

                        markup.Children.Add(background);
                    }

                    StackPanel content = new StackPanel();
                    {
                        content.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        Grid.SetRow(content, 1);
                        Grid.SetColumn(content, 1);

                        image = new Image();
                        {
                            image.Height = 110f;
                            image.Width = 110f;
                            content.Children.Add(image);
                        }

                        contentButtons = new StackPanel();
                        {
                            contentButtons.Orientation = Orientation.Horizontal;
                            contentButtons.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                            content.Children.Add(contentButtons);
                        }

                        text = new TextBlock();
                        {
                            text.Text = "Наименование товара 1500 рублей";
                            text.Width = 150;
                            text.TextWrapping = System.Windows.TextWrapping.Wrap;
                            text.FontSize = 12;
                            text.TextAlignment = System.Windows.TextAlignment.Center;
                            content.Children.Add(text);
                        }

                        markup.Children.Add(content);
                    }
                    buttons = new List<Button>();
                    body.Children.Add(markup);
                }
            }
            this.Data = data;
        }
        public struct DataElements
        {
            public string Title => title;
            public int Price => price;
            public string Currency => currency;
            public ImageSource Image => image;

            private readonly string title;
            private readonly int price;
            private readonly string currency;
            private readonly ImageSource image;

            public DataElements(string title, int price, string currency, string image)
            {
                this.title = title;
                this.price = price;
                this.currency = currency;
                this.image = StringToBitmapSource(title,image);
            }
        }
        private static BitmapSource StringToBitmapSource(string title, string text)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(text);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    var decoder = BitmapDecoder.Create(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                    return decoder.Frames[0];
                }
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}

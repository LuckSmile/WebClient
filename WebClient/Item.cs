using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WebClient
{
    public class Item
    {
        public Grid Body => body;
        private readonly Grid body = null;
        private readonly Image image = null;
        private readonly StackPanel contentButtons = null;
        private readonly TextBlock text = null;
        public Item()
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
                    
                    body.Children.Add(markup);
                }
            }
        }
    }
}

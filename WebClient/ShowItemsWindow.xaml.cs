using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebClient.Layout;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace WebClient
{
    /// <summary>
    /// Логика взаимодействия для ShowItemsWindow.xaml
    /// </summary>
    public partial class ShowItemsWindow : Window
    {
        private readonly GridLayoutGroup layoutGroup = null;
        public ShowItemsWindow()
        {
            InitializeComponent();
            layoutGroup = new GridLayoutGroup(SubjectsContent, new Vector2(30, 100f), new Vector2(5, 5));
            for (int index = 0; index < 10; index++)
            {
                Grid subject = new Grid
                {
                    Background = Brushes.BurlyWood
                };
                layoutGroup.Add(subject);
            }
        }

        private void InputSizeCell(object sender, TextChangedEventArgs e)
        {
            MessageBox.Show(e.Source.ToString());
        }
        private void InputSpacing(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = e.Source as TextBox;
            if (int.TryParse(textBox.Text, out int number))
            {
                number = number <= 0 ? 1 : number;
                if (textBox == SpacingX)
                {
                    if (layoutGroup.Spacing.x != number)
                        layoutGroup.Spacing = new Vector2(number, layoutGroup.Spacing.y);
                }
                else if (textBox == SpacingY)
                {
                    if (layoutGroup.Spacing.y != number)
                        layoutGroup.Spacing = new Vector2(layoutGroup.Spacing.x, number);
                }
            }
            else
            {
                textBox.Text = textBox.Text.Length - 1 >= 0 ? textBox.Text.Remove(textBox.Text.Length - 1) : "";
            }
            textBox.SelectionStart = textBox.Text.Length;
        }
    }
}

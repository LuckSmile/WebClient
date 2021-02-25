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
using System.IO;

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
            layoutGroup = new GridLayoutGroup(SubjectsContent, new Vector2(160, 180f), new Vector2(5, 5));
            //SQL.Database database = new SQL.Database(@"Data Source=DESKTOP-KAQ3KMV\SQLEXPRESS;Initial Catalog=BD;Integrated Security=True");
            SQL.Database database = new SQL.Database(@"Server=DESKTOP-KAQ3KMV;Database=BD;User Id=admin;Password=admin");
            //SQL.Database database = new SQL.Database(@"Data Source=192.168.137.149, 1433;Initial Catalog=BD;Integrated Security=True");
            SQL.Line[] products = database.Reader("Product");
            for(int index = 0; index < products.Length; index++)
            {
                SQL.Line product = products[index];
                Item item = new Item(new Item.DataElements(title:product.Columns["Title"].Value, 
                                                            price:int.Parse(product.Columns["Cost"].Value.Remove(product.Columns["Cost"].Value.IndexOf(','))), 
                                                            currency:"руб", 
                                                            image:product.Columns["MainImagePath"].Value));
                layoutGroup.Add(item.Body);
            }
            //for (int index = 0; index < 10; index++)
            //{
            //    Item item = new Item(new Item.DataElements("123", 120, "рублей", new ImageSource[] { source }));
            //    layoutGroup.Add(item.Body);
            //}

            SizeCellX.Text = "" + layoutGroup.CellSize.x;
            SizeCellY.Text = "" + layoutGroup.CellSize.y;
            SpacingX.Text = "" + layoutGroup.Spacing.x;
            SpacingY.Text = "" + layoutGroup.Spacing.y;
            Constraint.Text = "" + layoutGroup.Constraint;
        }

        private void InputSizeCell(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = e.Source as TextBox;
            if (int.TryParse(textBox.Text, out int number))
            {
                number = number <= 0 ? 1 : number;
                if (textBox == SizeCellX)
                {
                    if (layoutGroup.CellSize.x != number)
                        layoutGroup.CellSize = new Vector2(number, layoutGroup.CellSize.y);
                }
                else if (textBox == SizeCellY)
                {
                    if (layoutGroup.CellSize.y != number)
                        layoutGroup.CellSize = new Vector2(layoutGroup.CellSize.x, number);
                }
            }
            else
            {
                textBox.Text = textBox.Text.Length - 1 >= 0 ? textBox.Text.Remove(textBox.Text.Length - 1) : "";
            }
            textBox.SelectionStart = textBox.Text.Length;
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
        private void InputConstraint(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = e.Source as TextBox;
            if (int.TryParse(textBox.Text, out int number))
            {
                number = number <= 0 ? 1 : number;
                if (layoutGroup.Constraint != number)
                    layoutGroup.Constraint = number;
            }
            else
            {
                textBox.Text = textBox.Text.Length - 1 >= 0 ? textBox.Text.Remove(textBox.Text.Length - 1) : "";
            }
            textBox.SelectionStart = textBox.Text.Length;
        }
    }
}

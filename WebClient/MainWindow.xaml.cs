using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SQL;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database database = new Database(@"Data Source=.\SQLEXPRESS;Initial Catalog=WEB;Integrated Security=True");

            string[] columns = database.GetColumn("Users");
            for (int index = 0; index < columns.Length; index++)
            {
                MessageBox.Show(columns[index]);
            }
        }
    }
}

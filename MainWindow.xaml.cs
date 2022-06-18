using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KingITProject.Model;
namespace KingITProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Regex login = new Regex("/A[0-9,A-Z,a-z]/z");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

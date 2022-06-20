using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
namespace KingITProject.Pages.ManagerC
{
    /// <summary>
    /// Логика взаимодействия для EditMallList.xaml
    /// </summary>
    public partial class EditMallList : Page
    {
        MainWindow main;
        mall current;
        public EditMallList(MainWindow _main)
        {
            InitializeComponent();
            main = _main;
            current = new mall();

        }
        public EditMallList(mall _current, MainWindow _main)
        {
            InitializeComponent();
            current = _current;
        }
        private void ChangeImage(object sender, RoutedEventArgs e)
        {

        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            main.frame.Navigate(new MallList(main));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            main.frame.Navigate(new MallList(main));
        }
    }
}

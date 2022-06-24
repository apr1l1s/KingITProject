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
    /// Логика взаимодействия для EditHallList.xaml
    /// </summary>
    public partial class EditHallList : Page
    {
        MainWindow main;
        hall currentHall;
        mall currentMall;
        public EditHallList(MainWindow _main, mall _currentMall)
        {
            InitializeComponent();
            main = _main;
            currentMall = _currentMall;
            FillStatusBox();
        }
        public EditHallList(MainWindow _main, hall _currentHall, mall _currentMall)
        {
            InitializeComponent();
            main = _main;
            currentHall = _currentHall;
            currentMall = _currentMall;
            FillStatusBox();
        }
        private void FillStatusBox()
        {
            StatusBox.Items.Add("Арендован");
            StatusBox.Items.Add("Свободен");
            StatusBox.Items.Add("Забронирован");
            StatusBox.Items.Add("...");
        }
        private void FillBoxes()
        {

        }
        private void FillCurrent()
        {

        }
        private void Filtering(int floor, string hall_number, decimal area, decimal value_added_factor, decimal cost, string status_title)
        {

        }
        private void Exit(object sender, RoutedEventArgs args)
        {
            main.frame.Navigate(new HallsList(main, currentMall));
        }
        private void Save(object sender, RoutedEventArgs args)
        {
            main.frame.Navigate(new HallsList(main, currentMall));
        }
    }
}

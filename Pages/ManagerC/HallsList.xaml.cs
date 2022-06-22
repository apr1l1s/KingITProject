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
    /// Логика взаимодействия для HallsList.xaml
    /// </summary>
    public partial class HallsList : Page
    {
        MainWindow main;
        mall currentMall;
        public HallsList(MainWindow _main, mall _currentMall)
        {
            InitializeComponent();
            main = _main;
            currentMall = _currentMall;
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            try
            {
                using(var db = new KingITDBEntities(main.connectionName))
                {
                    DG.ItemsSource = (from h in db.getHalls(currentMall.mall_id) select h).ToList();
                }
            }
            catch { }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            main.frame.Navigate(new MallList(main));
        }
        private void Add(object sender, RoutedEventArgs e)
        {

        }
        private void Edit(object sender, RoutedEventArgs e)
        {

        }
        private void Delete(object sender, RoutedEventArgs e)
        {

        }
        private void DG_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }
}

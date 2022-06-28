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

namespace KingITProject.Pages.ManagerA
{
    /// <summary>
    /// Логика взаимодействия для TenantsList.xaml
    /// </summary>
    public partial class TenantsList : Page
    {
        MainWindow main;
        Regex regex = new Regex(@"^[\w\-\s\d]*$");
        public TenantsList(MainWindow _main)
        {
            InitializeComponent();
            main = _main;
            FillDG("");
        }
        private void FillDG(string s)
        {
            try
            {
                using(var db = new KingITDBEntities(main.connectionName))
                {
                    if (regex.IsMatch(TitleBox.Text))
                    {
                        DG.ItemsSource = (from t in db.tenants
                                          where t.title.Contains(TitleBox.Text)
                                          select t).ToList();
                    }
                    else
                    {
                        DG.ItemsSource = (from t in db.tenants
                                          select t).ToList();
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            main.frame.Navigate(new LoginPage(main));
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                using(var db = new KingITDBEntities(main.connectionName))
                {
                    var selectedTenant = db.tenants.Find(((tenant)DG.SelectedItem).tenant_id);
                    var page = new EditTenantsList(main, selectedTenant);
                    page.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FillDG(TitleBox.Text);

        }
        private void Add(object sender, RoutedEventArgs e)
        {
            var page = new EditTenantsList(main);
            page.ShowDialog();
            FillDG(TitleBox.Text);
        }
        private void TitleBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillDG(TitleBox.Text);
        }
        private void DG_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DG.SelectedIndex != -1)
            {
                EditButton.IsEnabled = true;
            }
            else
            {
                EditButton.IsEnabled = false;
            }
        }
        private void Stat(object sender, RoutedEventArgs e)
        {
            var page = new MallsStat(main);
            page.ShowDialog();
        }
    }
}

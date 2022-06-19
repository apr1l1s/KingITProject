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
    /// Логика взаимодействия для MallList.xaml
    /// </summary>
    public partial class MallList : Page
    {
        MainWindow mainWindow;
        public MallList(MainWindow main)
        {
            mainWindow = main;
            InitializeComponent();
            CB.Items.Add("План");
            CB.Items.Add("Строительство");
            CB.Items.Add("Реализация");
            CB.Items.Add("...");
            FillCB("");
            
        }
        private void FillCB(string address)
        {
            if (CB.SelectedIndex == -1 || CB.SelectedIndex == 3)
            {
                try
                {
                    using (KingITDBEntities db = new KingITDBEntities())
                    {

                        if (address == "")
                        {
                            DG.ItemsSource = (from m in db.getMalls() select m).ToList();
                        }
                        else
                        {
                            DG.ItemsSource = (from m in db.getMalls() where m.address.Contains(address) select m).ToList();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Подключение к базе данных вызвало сбой:\n" + ex.Message);
                }
            }
            else
            {
                try
                {
                    using (KingITDBEntities db = new KingITDBEntities())
                    {

                        if (address == "")
                        {
                            DG.ItemsSource = (from m in db.getMalls() 
                                              where m.status_title == CB.SelectedValue.ToString() 
                                              select m).ToList();
                        }
                        else
                        {
                            DG.ItemsSource = (from m in db.getMalls() 
                                              where m.address.Contains(address) && m.status_title == CB.SelectedValue.ToString() 
                                              select m).ToList();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Подключение к базе данных вызвало сбой:\n" + ex.Message);
                }
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new LoginPage(mainWindow));
        }

        private void AddressChanged(object sender, TextChangedEventArgs e)
        {
            FillCB(AddressBox.Text);
        }

        private void StatusChanged(object sender, SelectionChangedEventArgs e)
        {
            FillCB(AddressBox.Text);
        }
    }
}

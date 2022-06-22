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
            FillAddressCB();
            FillStatusCB();
            FillDG("", "");
            
        }
        private void FillStatusCB()
        {
            StatusCB.Items.Add("План");
            StatusCB.Items.Add("Строительство");
            StatusCB.Items.Add("Реализация");
            StatusCB.Items.Add("...");
        }
        private void FillAddressCB()
        {
            try
            {
                using (KingITDBEntities db = new KingITDBEntities(mainWindow.connectionName))
                {
                    var list = (from m in db.getMalls() select m.address).Distinct().ToList();
                    list.Add("...");
                    AddressCB.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Подключение к базе данных вызвало сбой:\n" + ex.Message);
            }
        }
        private void FillDG(string address, string status_title)
        {
            try
            {
                using (KingITDBEntities db = new KingITDBEntities(mainWindow.connectionName))
                {

                    if (address == "" && status_title == "")
                    {
                        DG.ItemsSource = (from m in db.getMalls() 
                                          select m).ToList();
                    }
                    else
                    {
                        if (address != "" && status_title == "")
                        {
                            DG.ItemsSource = (from m in db.getMalls() 
                                              where m.address == address 
                                              select m).ToList();
                        } else
                        {
                            if (address == "" && status_title != "")
                            {
                                DG.ItemsSource = (from m in db.getMalls() 
                                                  where m.status_title == status_title 
                                                  select m).ToList();
                            } else
                            {
                                if (address != "" && status_title != "")
                                {
                                    DG.ItemsSource = (from m in db.getMalls() 
                                                      where m.status_title == status_title &&
                                                      m.address == address
                                                      select m).ToList();
                                }
                            }
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Подключение к базе данных вызвало сбой:\n" + ex.Message);
            }
        }
        private void AddressChanged(object sender, TextChangedEventArgs e)
        {
            var address = (AddressCB.SelectedIndex == -1 || Convert.ToString(AddressCB.SelectedValue) == "...") ? "" : AddressCB.SelectedValue.ToString();
            var status = (StatusCB.SelectedIndex == -1 || StatusCB.SelectedIndex == 3) ? "" : StatusCB.SelectedValue.ToString();
            FillDG(address, status);
        }
        private void StatusChanged(object sender, SelectionChangedEventArgs e)
        {
            var address = (AddressCB.SelectedIndex == -1 || Convert.ToString(AddressCB.SelectedValue) == "...") ? "" : AddressCB.SelectedValue.ToString();
            var status = (StatusCB.SelectedIndex == -1 || StatusCB.SelectedIndex == 3) ? "" : StatusCB.SelectedValue.ToString();
            FillDG(address, status);
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            var selectedObj = (getMalls_Result)DG.SelectedItem;
            try
            {
                using (KingITDBEntities db = new KingITDBEntities(mainWindow.connectionName))
                {
                    mall deletedObj = (from m in db.malls
                                       where m.mall_id == selectedObj.mall_id
                                       select m).FirstOrDefault();
                    deletedObj.status_id = 3;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Подключение к базе данных вызвало ошибку:\n" + ex.Message);
            }
            var address = (AddressCB.SelectedIndex == -1 || Convert.ToString(AddressCB.SelectedValue) == "...") ? "" : AddressCB.SelectedValue.ToString();
            var status = (StatusCB.SelectedIndex == -1 || StatusCB.SelectedIndex == 3) ? "" : StatusCB.SelectedValue.ToString();
            FillDG(address, status);
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            var selectedObj = (getMalls_Result)DG.SelectedItem;
            try
            {
                using (KingITDBEntities db = new KingITDBEntities(mainWindow.connectionName))
                {
                    mall editedObj = (from m in db.malls
                                       where m.mall_id == selectedObj.mall_id
                                       select m).FirstOrDefault();
                    mainWindow.frame.Navigate(new EditMallList(editedObj, mainWindow));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Подключение к базе данных вызвало ошибку:\n" + ex.Message);
            }
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new EditMallList(mainWindow));
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new LoginPage(mainWindow));
        }
        private void Open(object sender, RoutedEventArgs e)
        {
            var selectedObj = (getMalls_Result)DG.SelectedItem;
            try
            {
                using (KingITDBEntities db = new KingITDBEntities(mainWindow.connectionName))
                {
                    mall editedObj = (from m in db.malls
                                      where m.mall_id == selectedObj.mall_id
                                      select m).FirstOrDefault();
                    mainWindow.frame.Navigate(new HallsList(mainWindow, editedObj));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Подключение к базе данных вызвало ошибку:\n" + ex.Message);
            }
        }
        private void DG_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DelButton.IsEnabled = true;
            EditButton.IsEnabled = true;
            HallButton.IsEnabled = true;
        }
    }
}

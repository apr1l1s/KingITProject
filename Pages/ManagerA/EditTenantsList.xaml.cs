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
using System.Windows.Shapes;
using KingITProject.Model;

namespace KingITProject.Pages.ManagerA
{
    /// <summary>
    /// Логика взаимодействия для EditTenantsList.xaml
    /// </summary>
    public partial class EditTenantsList : Window
    {
        MainWindow main;
        tenant currentTenant;
        Regex title = new Regex(@"^[\w\-\s\d]*$");
        Regex phone = new Regex(@"^\+7\(\d{3}\)\d{3}\-[0-9]{2}\-[0-9]{2}$");
        Regex address = new Regex(@"^[\w\-\s\d\.\,]*$");
        public EditTenantsList(MainWindow _main)
        {
            InitializeComponent();
            main = _main;
        }
        public EditTenantsList(MainWindow _main, tenant _tenant)
        {
            InitializeComponent();
            main = _main;
            currentTenant = _tenant;
            FillBoxes();
        }
        private void FillBoxes()
        {
            if (currentTenant != null)
            {
                TitleBox.Text = currentTenant.title;
                NumberBox.Text = currentTenant.contact_phone.Trim(' ');
                AddressBox.Text = currentTenant.address;
            }
        }
        private void FillCurrent()
        {
            if(title.IsMatch(TitleBox.Text) && phone.IsMatch(NumberBox.Text) && address.IsMatch(AddressBox.Text))
            {
                currentTenant.title = TitleBox.Text;
                currentTenant.contact_phone = NumberBox.Text;
                currentTenant.address = AddressBox.Text.Trim(' ');
            }
            else
            {
                MessageBox.Show("Неправильный формат");
                currentTenant = null;
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            main.frame.Navigate(new TenantsList(main));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                using(var db = new KingITDBEntities(main.connectionName))
                {
                    if (currentTenant == null)
                    {
                        currentTenant = new tenant() { tenant_id = (from t in db.tenants 
                                                                    orderby t.tenant_id descending 
                                                                    select t.tenant_id).First() + 1 };
                        FillCurrent();
                        if (currentTenant != null)
                        {
                            db.tenants.Add(currentTenant);
                            db.SaveChanges();
                            this.Close();
                        }
                            
                    }
                    else
                    {
                        FillCurrent();
                        if (currentTenant != null)
                        {
                            var edited = (from t in db.tenants
                                          where t.tenant_id == currentTenant.tenant_id
                                          select t).First();
                            if (edited != null)
                            {
                                edited.title = currentTenant.title;
                                edited.address = currentTenant.address;
                                edited.contact_phone = currentTenant.contact_phone.Trim(' ');
                                db.SaveChanges();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка 2");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ошибка 1");
                        }
                    }
                }
            } catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }            
        }
    }
}

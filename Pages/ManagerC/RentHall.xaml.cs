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
using System.Windows.Shapes;
using KingITProject.Model;

namespace KingITProject.Pages.ManagerC
{
    /// <summary>
    /// Логика взаимодействия для RentHall.xaml
    /// </summary>
    public partial class RentHall : Window
    {
        MainWindow main;
        hall currentHall;
        mall currentMall;


        public RentHall(MainWindow _main, hall _currentHall, mall _currentMall)
        {
            InitializeComponent();
            main = _main;
            currentHall = _currentHall;
            currentMall = _currentMall;
            FillTenantBox();
            blockDates();
        }
        private void FillTenantBox()
        {
            try
            {
                using(var db = new KingITDBEntities(main.connectionName))
                {
                    TenantBox.ItemsSource = (from t in db.tenants select t.title).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void blockDates()
        {
            try
            {
                using(var db = new KingITDBEntities())
                {
                    var lastdate = new CalendarDateRange() { End = DateTime.MinValue };
                    foreach (rent current in db.rents)
                    {
                        DateEnd.BlackoutDates.Add(new CalendarDateRange() { Start = current.start_date, End = current.end_date });
                        DateStart.BlackoutDates.Add(new CalendarDateRange() { Start = current.start_date, End = current.end_date });
                        if (lastdate.End < current.end_date) lastdate.End = current.end_date;
                    }
                    DateStart.DisplayDate = lastdate.End;
                    DateEnd.DisplayDate = lastdate.End;
                }
            }
            catch { }
            
        }
        private void Exit(object sender, RoutedEventArgs args)
        {
            this.Close();
        }
        private void Rent(object sender, RoutedEventArgs args)
        {
            try
            {
                using (var db = new KingITDBEntities(main.connectionName))
                {
                    var start = DateStart.SelectedDate.Value;
                    var end = DateEnd.SelectedDate.Value;
                    MessageBox.Show(DateTime.Now.DayOfYear.ToString());
                    var tenant_id = (from t in db.tenants 
                                     where t.title == TenantBox.SelectedValue.ToString() 
                                     select t.tenant_id).First();
                     db.rentHall(currentHall.hall_id,(start.DayOfYear == DateTime.Now.DayOfYear) ? true : false,
                     currentHall.hall_number, currentHall.mall_id, start, end, tenant_id,main.emploer_id);
                    db.SaveChanges();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
    }
}

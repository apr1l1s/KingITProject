using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            FillBoxes();
        }
        private void FillStatusBox()
        {
            StatusBox.Items.Add("Арендован");
            StatusBox.Items.Add("Свободен");
            StatusBox.Items.Add("Забронирован");
        }
        private void FillBoxes()
        {
            FloorBox.Text = Convert.ToString(currentHall.floor);
            NumberBox.Text = currentHall.hall_number;
            AreaBox.Text = Convert.ToString(currentHall.area);
            FactorBox.Text = Convert.ToString(currentHall.value_added_factor);
            CostBox.Text = Convert.ToString(currentHall.cost);
            switch (currentHall.status){
                case 5:
                    StatusBox.SelectedIndex = 1;
                    break;
                case 6:
                    StatusBox.SelectedIndex = 0;
                    break;
                case 7:
                    StatusBox.SelectedIndex = 2;
                    break;
            }
        }
        private void FillCurrent()
        {
            currentHall.floor = Convert.ToInt32(FloorBox.Text);
            currentHall.hall_number = NumberBox.Text;
            currentHall.area = Convert.ToDecimal(AreaBox.Text);
            currentHall.value_added_factor = Convert.ToDecimal(FactorBox.Text);
            currentHall.cost = Convert.ToDecimal(CostBox.Text);
            switch (StatusBox.SelectedIndex)
            {
                case 1:
                    currentHall.status = 5;
                    break;
                case 2:
                    currentHall.status = 6;
                    break;
                case 0:
                    currentHall.status = 7;
                    break;
            }
        }
        private void Exit(object sender, RoutedEventArgs args)
        {
            main.frame.Navigate(new HallsList(main, currentMall));
        }
        private void Save(object sender, RoutedEventArgs args)
        {
            try
            {
                using(var db = new KingITDBEntities())
                {
                    if (currentHall == null) //Если добавление
                    {
                        currentHall = new hall(); //новый павильон
                        currentHall.hall_id = (from h in db.halls
                                               orderby h.hall_id descending
                                               select h.hall_id).FirstOrDefault();
                        currentHall.hall_id += 1;
                        currentHall.mall_id = currentMall.mall_id;
                        FillCurrent(); //заполненый павильон
                        var selected = (from h in db.halls
                                   where h.hall_id == currentHall.hall_id
                                   select h).FirstOrDefault();
                        if (selected == null) //павильон не повторяется
                        {
                            db.halls.Add(currentHall);
                            db.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show($"{selected.hall_id} {selected.hall_number} /// {currentHall.hall_id} {currentHall.hall_number}");
                            throw new Exception("Уже есть такой павильон");
                        }
                    }
                    else
                    {
                        FillCurrent();
                        var selected = (from h in db.halls
                                        where h.hall_id == currentHall.hall_id
                                        select h).FirstOrDefault();
                        selected.hall_number = currentHall.hall_number;
                        selected.area = currentHall.area;
                        selected.status = currentHall.status;
                        selected.floor = currentHall.floor;
                        selected.value_added_factor = currentHall.value_added_factor;
                        selected.cost = currentHall.cost;
                        db.SaveChanges();
                    }
                    
                }

            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            main.frame.Navigate(new HallsList(main, currentMall));
        }
    }
}

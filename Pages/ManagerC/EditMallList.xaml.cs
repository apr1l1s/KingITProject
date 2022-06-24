using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KingITProject.Model;
using KingITProject.Tools;
using Microsoft.Win32;

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
            FillStatusCB();
        }
        public EditMallList(mall _current, MainWindow _main)
        {
            InitializeComponent();
            main = _main;
            current = _current;
            FillBoxes();
            FillStatusCB();
        }
        private void FillStatusCB()
        {
            StatusBox.Items.Add("План");
            StatusBox.Items.Add("Строительство");
            StatusBox.Items.Add("Реализация");
            StatusBox.Items.Add("...");
        }
        private bool FillCurrentMall()
        {
            try
            {
                using(var db = new KingITDBEntities(main.connectionName))
                {
                    if (AddressBox.Text.Trim().Length > 0 &&
                                        AddressBox.Text.Length <= 100 &&
                                        TitleBox.Text.Trim().Length > 0 &&
                                        TitleBox.Text.Length <= 100 &&
                                        Convert.ToDecimal(FactorBox.Text) >= (decimal)0.1 &&
                                        Convert.ToDecimal(FactorBox.Text) < (decimal)9999.9 &&
                                        Convert.ToDecimal(CostBox.Text) >= (decimal)0 &&
                                        Convert.ToInt16(FloorsBox.Text) >= 0 &&
                                        Convert.ToInt16(FloorsBox.Text) <= 10 &&
                                        Convert.ToInt16(HallssBox.Text) > 0 &&
                                        Convert.ToInt16(HallssBox.Text) < 100
                                        )
                    {
                        current.value_added_factor = Convert.ToDecimal(FactorBox.Text);
                        current.address = AddressBox.Text;
                        current.cost = Convert.ToDecimal(CostBox.Text);
                        switch (StatusBox.SelectedValue)
                        {
                            case "План":
                                current.status_id = 1;
                                break;
                            case "Строительство":
                                current.status_id = 2;
                                break;
                            case "Реализация":
                                current.status_id = 4;
                                break;
                        }

                        current.floors_count = Convert.ToInt16(FloorsBox.Text);
                        current.halls_count = Convert.ToInt16(HallssBox.Text);
                        current.title = TitleBox.Text;
                        return true;
                    }
                }
                
            }
            catch
            {
                
            }
            return false;
        }
        private void FillBoxes()
        {
            TitleBox.Text = current.title;
            AddressBox.Text = current.address;
            CostBox.Text = Convert.ToString(current.cost);
            FactorBox.Text = Convert.ToString(current.value_added_factor);
            HallssBox.Text = Convert.ToString(current.halls_count);
            FloorsBox.Text = Convert.ToString(current.floors_count);
            switch(current.status_id){
                case 1:
                    StatusBox.SelectedIndex = 0;
                    break;
                case 2:
                    StatusBox.SelectedIndex = 1;
                    break;
                case 4:
                    StatusBox.SelectedIndex = 2;
                    break;
            }
            ImageMall.Source = Conventer.BytesToImage(current.icon);
        }
        private void ChangeImage(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Title = "Select Picture";
                dialog.Filter = "Image Files (*.jpg)|*.jpg";
                dialog.ShowDialog();
                FileInfo picture = new FileInfo(dialog.FileName);
                if (System.IO.Path.GetExtension(picture.FullName) == ".jpg")
                {
                    current.icon = KingITProject.Tools.Conventer.ImageToBytes(dialog.FileName);
                    ImageMall.Source = KingITProject.Tools.Conventer.BytesToImage(current.icon);
                }
                else
                {
                    throw new Exception("Ошибка загрузки картинки");
                }  
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            main.frame.Navigate(new MallList(main));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            FillCurrentMall();
            try
            {
                using(var db = new KingITDBEntities(main.connectionName))
                {
                    
                    var searched = (from m in db.malls where m.mall_id == current.mall_id select m).FirstOrDefault();
                    if (searched != null)
                    {
                        searched.title = current.title;
                        searched.status_id = current.status_id;
                        searched.halls_count = current.halls_count;
                        searched.cost = current.cost;
                        searched.floors_count = current.floors_count;
                        searched.address = current.address;
                        searched.icon = current.icon;
                        searched.value_added_factor = current.value_added_factor;
                        //MessageBox.Show($"{current.mall_id}: {current.title}, {current.status_id}, {current.halls_count}, {current.floors_count}, {current.value_added_factor}, {current.cost}");
                        db.SaveChanges();
                    }
                    else
                    {
                        current.mall_id = (from m in db.malls orderby m.mall_id descending select m.mall_id).FirstOrDefault() + 1;
                        db.malls.Add(current);
                        //MessageBox.Show($"{current.mall_id}: {current.title}, {current.status_id}, {current.halls_count}, {current.floors_count}, {current.value_added_factor}, {current.cost}");
                        db.SaveChanges();
                    }
                }
                main.frame.Navigate(new MallList(main));
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}

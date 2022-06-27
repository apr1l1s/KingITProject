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

namespace KingITProject.Pages.ManagerA
{
    /// <summary>
    /// Логика взаимодействия для MallsStat.xaml
    /// </summary>
    public partial class MallsStat : Window
    {
        public MallsStat()
        {
            InitializeComponent();
            FillCB();
            FillDG(Convert.ToString(MallBox.SelectedValue));
        }
        private void FillCB()
        {
            try
            {
                using (var db = new KingITDBEntities())
                {
                    MallBox.ItemsSource = (from m in db.statMall()
                                      select m.title).ToList();
                }
            }
            catch { }
        }
        private void FillDG(string s)
        {
            try
            {
                using(var db = new KingITDBEntities())
                {
                    if (s != "")
                    {
                        DG.ItemsSource = (from m in db.statMall()
                                          where m.title == s
                                          select m).ToList();
                    }
                    else
                    {
                        DG.ItemsSource = (from m in db.statMall()
                                          select m).ToList();
                    }
                }
            }
            catch { }
        }

        private void MallBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillDG(Convert.ToString(MallBox.SelectedValue));
        }
    }
}

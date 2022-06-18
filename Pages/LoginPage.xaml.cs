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

namespace KingITProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        int Attemps = 0;
        Regex mail = new Regex(@"\A[a-z,0-9]{4,20}?@[a-z]{2,8}?\.(ru|com)\z");
        Regex pas = new Regex(@"\A[a-z,A-Z,0-9]{8,16}?\z");
        Regex login = new Regex(@"^[0-9,A-Z,a-z]{1,10}@[A-Z,a-z]{1,10}\.[A-Z,a-z]{2,4}$");
        Regex password = new Regex(@"^[0-9,A-Z,a-z]{4,9}$");
        public LoginPage()
        {
            InitializeComponent();
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            if (Attemps >= 3) { 
                ShowCaptcha(); 
            }
            else {
                try
                {
                    if(mail.IsMatch(LogBox.Text))
                    {
                        if(pas.IsMatch(PassBox.Password))
                        {
                            if(CheckUser(LogBox.Text, PassBox.Password))
                            {
                                MessageBox.Show("Вы вошли");
                            } else
                            {
                                MessageBox.Show("Неправильный логин или пароль");
                                Attemps++;
                            }
                        }
                        else MessageBox.Show("Неподходящий формат пароля");
                    } else MessageBox.Show("Неподходящий формат логина");
                    
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ShowCaptcha() { Attemps = 0; }
        private bool CheckUser(string login, string pass)
        {
            using (KingITDBEntities db = new KingITDBEntities())
            {
                var current = (from emp in db.employers
                               where emp.login.ToLower() == login.ToLower() &&
                               emp.password == pass
                               select emp).FirstOrDefault();
                return (current != null) ? true : false;
            }
        }
    }
}

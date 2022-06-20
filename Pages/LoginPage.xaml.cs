using System;
using System.Collections.Generic;
using System.Drawing;
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
        MainWindow mainWindow;
        int Attemps = 0;
        Regex mail = new Regex(@"\A[A-z,0-9]{4,20}?@[A-z]{2,8}?\.[A-z]{2,3}\z");
        Regex pas = new Regex(@"\A[A-z,0-9]{4,16}?\z");
        public LoginPage(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
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
                            var UserType = CheckUser(LogBox.Text, PassBox.Password);
                            if (UserType != 0)
                            {
                                MessageBox.Show("Вы вошли");
                                
                                switch (UserType)
                                {
                                    case 3:
                                        mainWindow.frame.Navigate(new ManagerC.MallList(mainWindow));
                                        break;
                                }
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
        public void ShowCaptcha() {
            var captcha = new Captcha();
            captcha.ShowDialog();
            Attemps = 0; 
        }
        private int CheckUser(string login, string pass)
        {
            using (KingITDBEntities db = new KingITDBEntities())
            {
                var current = (from emp in db.employers
                               where emp.login.ToLower() == login.ToLower() &&
                               emp.password == pass
                               select emp).FirstOrDefault();
                return (current != null) ? current.post_id : 0;
            }
        }
    }
}

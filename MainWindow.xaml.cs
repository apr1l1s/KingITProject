using System.Windows;
namespace KingITProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string connectionName = "name=KingITDBEntitiesD";
        public MainWindow()
        {
            InitializeComponent();
            frame.Navigate(new KingITProject.Pages.LoginPage(this));
        }
    }
}

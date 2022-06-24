using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

namespace KingITProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        string text;
        public Captcha()
        {
            InitializeComponent();
            CaptchaImage.Source = ToBitmapImage(CreateImage(200, 200));
        }
        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();
            Bitmap result = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(result);

            text = String.Empty;
            string ALF = "7890QWERTYUIPASDFGHJKLZXCVBNM";
            while (text.Length != 5)
            {
                if (rnd.Next(1, 5) == 2)
                {
                    text += ALF[rnd.Next(1, ALF.Length-1)];
                }
            }
            List<System.Drawing.Brush> brushes = new List<System.Drawing.Brush>();
            brushes.Add(System.Drawing.Brushes.Tomato);
            brushes.Add(System.Drawing.Brushes.Violet);
            brushes.Add(System.Drawing.Brushes.Blue);
            brushes.Add(System.Drawing.Brushes.Yellow);
            var pen = new System.Drawing.Pen(brushes[rnd.Next(0, 3)],(float)3);
            var font = new Font("Arial", 36);
            g.DrawString(text, font, brushes[rnd.Next(0, 3)], new PointF(10, 100));
            g.DrawLine(pen, 10, 120+rnd.Next(3,20), 200, 120 + rnd.Next(3, 20));
            return result;
        }
        public BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }
        private void Refresh(object sender, RoutedEventArgs e)
        {
            CaptchaImage.Source = ToBitmapImage(CreateImage(200, 200));
        }
        private void Success(object sender, RoutedEventArgs e)
        {
            if (CaptchaBox.Text.ToLower() == text.ToLower()) { 
                this.Close();
            }
            else CaptchaImage.Source = ToBitmapImage(CreateImage(200, 200));
        }
    }
}

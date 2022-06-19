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
            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);
            //Вычислим позицию текста
            int Xpos = 10;
            int Ypos = 10;
            //Добавим различные цвета ддя текста
            System.Drawing.Brush[] colors = {
                System.Drawing.Brushes.Black,
                System.Drawing.Brushes.Red,
                System.Drawing.Brushes.RoyalBlue,
                System.Drawing.Brushes.Green,
                System.Drawing.Brushes.Yellow,
                System.Drawing.Brushes.White,
                System.Drawing.Brushes.Tomato,
                System.Drawing.Brushes.Sienna,
                System.Drawing.Brushes.Pink
            };

            //Добавим различные цвета линий
            System.Drawing.Pen[] colorpens = {
                 Pens.Black,
                 Pens.Red,
                 Pens.RoyalBlue,
                 Pens.Green,
                 Pens.Yellow,
                 Pens.White,
                 Pens.Tomato,
                 Pens.Sienna,
                 Pens.Pink
            };

            //Делаем случайный стиль текста
            System.Drawing.FontStyle[] fontstyle = {
                System.Drawing.FontStyle.Bold,
                System.Drawing.FontStyle.Italic,
                System.Drawing.FontStyle.Regular,
                System.Drawing.FontStyle.Strikeout,
                System.Drawing.FontStyle.Underline
            };

            //Добавим различные углы поворота текста
            Int16[] rotate = { 1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6 };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);

            //Пусть фон картинки будет серым
            g.Clear(System.Drawing.Color.Gray);

            //Делаем случайный угол поворота текста
            g.RotateTransform(rnd.Next(rotate.Length));

            //Генерируем текст
            text = String.Empty;
            string ALF = "7890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i) text += ALF[rnd.Next(ALF.Length)];

            //Нарисуем сгенирируемый текст
            g.DrawString(text,
            new Font("Arial", 28, fontstyle[rnd.Next(fontstyle.Length)]),
            colors[rnd.Next(colors.Length)],
            new PointF(Xpos, Ypos));

            //Добавим немного помех
            //Линии из углов
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
            new System.Drawing.Point(0, 0),
            new System.Drawing.Point(Width - 1, Height - 1));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
            new System.Drawing.Point(0, Height - 1),
            new System.Drawing.Point(Width - 1, 0));

            //Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, System.Drawing.Color.White);

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

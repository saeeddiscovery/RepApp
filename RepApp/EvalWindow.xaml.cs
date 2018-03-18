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
using System.IO;
using System.Windows.Interop;

namespace RepApp
{
    /// <summary>
    /// Interaction logic for EvalWindow.xaml
    /// </summary>
    public partial class EvalWindow : Window
    {
        public int imgIdx = 0;
        public List<string> imgList;
        public EvalWindow()
        {
            InitializeComponent();
        }

        private void EvalWin_Closed(object sender, EventArgs e)
        {
            //Application.Current.Shutdown();
        }

        private void image_Loaded(object sender, RoutedEventArgs e)
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string folderName = appPath + "\\تصاویر";
            if (Directory.Exists(folderName)==false)
                {
                MessageBox.Show("لطفا عکس ها را در پوشه ''تصاویر'' قرار دهید", "پوشه ''تصاویر'' پیدا نشد", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                this.Close();
            }
            imgList = Directory.GetFiles(folderName, "*.*", SearchOption.AllDirectories).ToList();
            image.Source = new BitmapImage(new Uri(imgList[imgIdx]));
            tb_Index.Text = "1 / " + imgList.Count.ToString(); 
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            if (imgIdx < imgList.Count-1)
            {
                imgIdx += 1;
                if (btn_Prev.IsEnabled == false) btn_Prev.IsEnabled = true;
                image.Source = new BitmapImage(new Uri(imgList[imgIdx]));
                tb_Index.Text = (imgIdx+1).ToString() + " / " + imgList.Count.ToString();
            }
            else
            {
                MessageBox.Show("پرسشنامه به اتمام رسید\nسپاسگذاریم ", "پایان ارزیابی", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                btn_Next.IsEnabled = false;
                //this.Close();
            }
        }

        private void btn_Prev_Click(object sender, RoutedEventArgs e)
        {
            imgIdx -= 1;
            if (imgIdx > 0)
            {
                if (btn_Next.IsEnabled == false) btn_Next.IsEnabled = true;
                image.Source = new BitmapImage(new Uri(imgList[imgIdx]));
                tb_Index.Text = (imgIdx+1).ToString() + " / " + imgList.Count.ToString();
            }
            else
            {
                image.Source = new BitmapImage(new Uri(imgList[imgIdx]));
                tb_Index.Text = (imgIdx+1).ToString() + " / " + imgList.Count.ToString();
                btn_Prev.IsEnabled = false;
            }
        }
    }
}

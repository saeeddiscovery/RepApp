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
using CsvHelper;

namespace RepApp
{
    /// <summary>
    /// Interaction logic for EvalWindow.xaml
    /// </summary>
    public partial class EvalWindow : Window
    {
        private static string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private static string folderPath = appPath + "/نتایج";
        private static string filePath = folderPath + "/survey.csv";
        private static int currId = 0;
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




        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            var csv = new StringBuilder();
            currId += 1;

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

        private void Win_Eval_Loaded(object sender, RoutedEventArgs e)
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string folderName = appPath + "\\تصاویر";
            imgList = Directory.GetFiles(folderName, "*.*", SearchOption.AllDirectories).ToList();
            if (Directory.Exists(folderName) == false)
            {
                MessageBox.Show("لطفا عکس ها را در پوشه ''تصاویر'' قرار دهید", "پوشه ''تصاویر'' پیدا نشد", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                this.Close();
            }

            image.Source = new BitmapImage(new Uri(imgList[imgIdx]));
            tb_Index.Text = "1 / " + imgList.Count.ToString();

            if (Directory.Exists(folderPath) == false)
                Directory.CreateDirectory(folderPath);

            if (File.Exists(filePath) == false)
            {
                var csv = new StringBuilder();
                string title = "ID";
                for (int i = 1; i <= imgList.Count; i++)
                {
                    if (i < imgList.Count)
                        title = title + ',';
                    title = title + i.ToString();
                }
                csv.AppendLine(title);
                File.WriteAllText(filePath, csv.ToString());
            }
            else
            {
                using (TextReader fileReader = File.OpenText(filePath))
                {
                    var csv = new CsvReader(fileReader);
                    var records = csv.GetRecords<dynamic>();
                    currId = records.Count<dynamic>();
                }
            }
        }
    }
}

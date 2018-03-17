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

namespace Eval
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
            string folderName = appPath + "\\Images";
            if (Directory.Exists(folderName)==false)
                {
                MessageBox.Show("لطفا عکس ها را در پوشه images قرار دهید", "پوشه images پیدا نشد", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            imgList = Directory.GetFiles(folderName, "*.*", SearchOption.AllDirectories).ToList();
            image.Source = new BitmapImage(new Uri(imgList[imgIdx]));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            imgIdx += 1;
            if (imgIdx < imgList.Count)
            {
                image.Source = new BitmapImage(new Uri(imgList[imgIdx]));
            }
            else
            {
                MessageBox.Show("پایان ارزیابی");
                this.Close();
            }
            
        }
    }
}

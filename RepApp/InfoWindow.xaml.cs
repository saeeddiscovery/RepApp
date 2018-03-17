using System;
using System.Collections.Generic;
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

namespace RepApp
{
    /// <summary>
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var csv = new StringBuilder();
            
            var name = tb_name.Text;
            var age = tb_age.Text;
            var gender = "";
            if (rb_female.IsChecked == true) gender = "زن";
            else if (rb_male.IsChecked == true) gender = "مرد";
            var height = tb_height.Text;
            var weight = tb_weight.Text;
            var education = cb_education.Text;
            var regime = "";
            if (rb_regime_yes.IsChecked == true) regime = "بله";
            else if (rb_regime_no.IsChecked == true) regime = "خیر";
            var surgery = "";
            if (rb_surgery_yes.IsChecked == true) surgery = "بله";
            else if (rb_surgery_no.IsChecked == true) surgery = "خیر";
            var hunger = tb_hunger.Text;
            var need = tb_need.Text;
            var fatigue = tb_fatigue.Text;
            var sleepy = tb_sleepy.Text;
            var lastMeal = tb_lastMeal.Text;
            var PMS = tb_PMS.Text;

            var title = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                "نام", "سن", "جنسیت", "قد", "وزن", "تحصیلات", "رژیم", "جراحی", "گرسنگی", "ولع", "خستگی", "خواب آلودگی", "از آخرین وعده", "قاعدگی");
            var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                name, age, gender, height, weight, education, regime, surgery, hunger, need, fatigue, sleepy, lastMeal, PMS);
            csv.AppendLine(title);
            csv.AppendLine(newLine);
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string folderPath = appPath + "/نتایج";
            string filePath = folderPath + "/info.csv";
            if (Directory.Exists(folderPath) == false)
                Directory.CreateDirectory(folderPath);
            File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8);

            EvalWindow evalWin = new EvalWindow();
            evalWin.Show();
            this.Close();
        }

        private void rb_female_Checked(object sender, RoutedEventArgs e)
        {
            lbl_PMS.IsEnabled = true;
            tb_PMS.IsEnabled = true;
        }

        private void rb_female_Unchecked(object sender, RoutedEventArgs e)
        {
            lbl_PMS.IsEnabled = false;
            tb_PMS.IsEnabled = false;
        }
    }
}

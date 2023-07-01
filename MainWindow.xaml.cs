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
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace ImageCopierWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void selectDirectory(TextBox tb) {
            using (System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                folderDialog.SelectedPath = "C:\\";

                System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();
                if (result.ToString() == "OK")
                    tb.Text = folderDialog.SelectedPath;
            }
        }
        private void btnTo_Click(object sender, RoutedEventArgs e)
        {
            selectDirectory(tbTo);
        }

        private void btnFrom_Click(object sender, RoutedEventArgs e)
        {
            selectDirectory(tbFrom);
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

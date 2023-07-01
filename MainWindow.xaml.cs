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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;
using TextBox = System.Windows.Controls.TextBox;

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

        private string pathFrom;
        private string pathTo;
        int index;
        private bool isLoaded = false;
        private string[] images;

        private void selectDirectory(TextBox tb) {
            using (System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                folderDialog.SelectedPath = "C:\\";

                System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();
                if (result.ToString() == "OK")
                    tb.Text = folderDialog.SelectedPath;
            }
        }
        private string[] findImages(string directoryPath)
        {
            string[] searchPatterns = { "*.jpg", "*.jpeg", "*.png" };
            string[] imageFilePaths = searchPatterns.SelectMany(pattern => Directory.EnumerateFiles(directoryPath, pattern, SearchOption.AllDirectories)).ToArray();
            return imageFilePaths;
        }

        private void setImageTo(int i)
        {
            //pbImage. = images[i];
            Uri fileUri = new Uri(images[i]);
            pbImage.Source = new BitmapImage(fileUri);
            try
            {
                lblImageName.Content = Path.GetFileName(images[i]);

            }
            catch (IndexOutOfRangeException) {
                MessageBox.Show("There are no images in the From Directory or invalid index");
            }
            tbImageNum.Text = (i + 1).ToString();
            index = i;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(tbFrom.Text) && Directory.Exists(tbTo.Text))
            {
                pathFrom = tbFrom.Text;
                pathTo = tbTo.Text;
                images = findImages(pathFrom);
                lblImageCount.Content = "/" + images.Length;
                setImageTo(0);
                isLoaded = true;
            }
            else
            {
                MessageBox.Show("Directory not valid");
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

        private void nextImage()
        {
            setImageTo((index + 1) % images.Length);
        }

        private void prevImage()
        {
            setImageTo((index - 1 + images.Length) % images.Length);
        }

        private void copyImage()
        {
            try
            {
                File.Copy(images[index], Path.Combine(pathTo, Path.GetFileName(images[index])));
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while copying the file: " + ex.Message);
            }
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            prevImage();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            nextImage();
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            copyImage();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                prevImage();
            }
            else if (e.Key == Key.Right)
            {
                nextImage();
            }
            else if (e.Key == Key.Z)
            {
                copyImage();
            }
        }
        private void tbImageNum_LostFocus(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                int newIndex;
                bool tryParse = int.TryParse(tbImageNum.Text, out newIndex);
                if (tryParse)
                {
                    setImageTo(newIndex - 1);
                }
            }
        }

    }
}

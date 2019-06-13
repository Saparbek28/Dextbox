using Dropbox;
using Dropbox.Api.Files;
using Microsoft.Win32;
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
using Dropbox.Api;
using System.IO;

namespace GHJ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string token = "eJEuos5Pk0AAAAAAAAAAQWUgrNi2CTqV_pxoSDrOAz29LR8G11awuxzwT60Rxxe3";

        public MainWindow()
        {
            InitializeComponent();
        }


        private async void GetFiles()
        {
            using (var dropbox = new DropboxClient("eJEuos5Pk0AAAAAAAAAAQWUgrNi2CTqV_pxoSDrOAz29LR8G11awuxzwT60Rxxe3"))
            {
                var list = await dropbox.Files.ListFolderAsync(string.Empty);
                filesListBox.Items.Clear();
                foreach (var item in list.Entries.Where(i => i.IsFile))
                {
                    filesListBox.Items.Add(item.Name);
                }
            }
        }

        private async void LoadClick(object sender, RoutedEventArgs e)
        {
            using (var dropbox = new DropboxClient("eJEuos5Pk0AAAAAAAAAAQWUgrNi2CTqV_pxoSDrOAz29LR8G11awuxzwT60Rxxe3"))
            {
                OpenFileDialog file = new OpenFileDialog();
                try
                {
                    file.ShowDialog();
                    await dropbox.Files.UploadAsync($"/{file.SafeFileName}", null, false, null, false, null, false, file.OpenFile());
                }
                catch (Exception)
                {
                    MessageBox.Show("Error. Please try again");
                    return;
                }
                GetFiles();
            }
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}

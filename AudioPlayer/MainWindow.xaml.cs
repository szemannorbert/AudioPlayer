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
using Microsoft.Win32;

namespace AudioPlayer
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

        String[] paths, filename;
        MediaPlayer mp = new MediaPlayer();

        private void listBoxSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBoxSongs.ItemsSource = paths.Select(x => x[1]);
            mp.Source = new Uri((string)listBoxSongs.ItemsSource);
            mp.Play();
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //mp.Volume = (double)volumeSlider.Value();
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            mp.Play();
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            mp.Pause();
        }

        private void statusBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            statusBar.Maximum = mp.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (string s in listBoxSongs.SelectedItems)
            {
                listBoxSongs.SelectedItems.Remove(s);
            }
        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == true)
            {
                filename = ofd.SafeFileNames;
                paths = ofd.FileNames;
                foreach (var item in filename)
                {
                    listBoxSongs.Items.Add(filename);
                }
            }
        }
    }
}

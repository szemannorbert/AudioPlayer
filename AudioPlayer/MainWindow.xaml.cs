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
using System.Windows.Threading;

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
            volumeSlider.Value = 0.5;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        String[] paths, filename;
        List<string> songs = new List<string>();
        MediaPlayer mp = new MediaPlayer();


        void timer_Tick(object sender, EventArgs e)
        {
            
            
                if ((mp.Source != null) && (mp.NaturalDuration.HasTimeSpan))
                {
                    statusBar.Value = mp.Position.TotalSeconds;
                    timelabel.Content = String.Format("{0} / {1}", mp.Position.ToString(@"mm\:ss"), mp.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                }
                else
                    timelabel.Content = "00:00 / 00:00";
            }
        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == true)
            {
                filename = ofd.SafeFileNames;
                paths = ofd.FileNames;
                for (int i = 0; i < ofd.FileNames.Length; i++)
                {
                    listBoxSongs.Items.Add(filename);
                    songs.Add(ofd.FileNames[i]);
                }
                listBoxSongs.ItemsSource = paths.Select(x => x[1]);
                listBoxSongs.SelectedIndex = 0;
                mp.Open(new Uri(songs[listBoxSongs.SelectedIndex]));
            }
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

            if (statusBar.Value == statusBar.Maximum)
            {
                mp.Open(new Uri(songs[listBoxSongs.SelectedIndex + 1]));
                mp.Play();
                listBoxSongs.SelectedIndex += 1;
            }

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (string s in listBoxSongs.SelectedItems)
            {
                listBoxSongs.SelectedItems.Remove(s);
            }
        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            mp.Open(new Uri(songs[listBoxSongs.SelectedIndex - 1]));
            mp.Play();
            listBoxSongs.SelectedIndex -= 1;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            mp.Open(new Uri(songs[listBoxSongs.SelectedIndex + 1]));
            mp.Play();
            listBoxSongs.SelectedIndex += 1;
        }
    }
}

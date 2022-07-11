using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Pesmarica.Database;

namespace Pesmarica.View
{
    /// <summary>
    /// Interaction logic for ShowSongView.xaml
    /// </summary>
    public partial class ShowSongView : UserControl
    {
        public ShowSongView()
        {
            InitializeComponent();
            
            if (GLOBALS.SELECTED_SONG != null) 
            {
                Song song = GLOBALS.SELECTED_SONG;
                //List<Song> song = SQLiteDataAccess.LoadSongs(GLOBALS.SELECTED_SONG_ID);
                List<Artist> artists = SQLiteDataAccess.LoadArtists(song.artist_id);
                ArtistSelected.Text = "Izvodjac: " + artists[0].artist_name;
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(song.artwork);
                src.EndInit();
                ImageSelected.Source = src;
                TitleSelected.Text = "Naziv: " + song.title;
                GenreSelected.Text = "Zanr: " + song.genre;
                ChordsTextBox.Text = song.title + "\n\n\n";
                string[] lyricsInRows = song.lyrics.Split('\n');
                List<string>lyricsAndChords = new List<string>();
                string pattern = @"\[(.*?)]";
                Regex rg = new Regex(pattern);
                
                //string[] chords = rg.Matches(lyricsInRows);
                for (int i = 0; i < lyricsInRows.Length; i++) 
                {
                    string str = lyricsInRows[i];
                    int startIndex = 0;
                    int endIndex = 0;
                    int lastEndIndex = 0;
                    for (int j = 0; j < str.Length; j++) 
                    {
                        //Trace.WriteLine("\n" + str[j]);
                        if (str[j].Equals('['))
                        {
                            startIndex = j;
                            //Trace.WriteLine("Akord");
                        }
                        else if (str[j].Equals(']')) 
                        {
                            endIndex = j;
                            for (int k = 0; k < startIndex - lastEndIndex; k++) 
                            {
                                ChordsTextBox.Text = ChordsTextBox.Text + " ";
                            }
                            lastEndIndex = endIndex;
                            ChordsTextBox.Text = ChordsTextBox.Text + str.Substring(startIndex + 1, endIndex - startIndex - 1);

                        }
                    }
                    ChordsTextBox.Text = ChordsTextBox.Text + "\n" + rg.Replace(str, "");
                }

            }
        }

        private void UpdateSongButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSongStackPanel.Visibility = Visibility.Hidden;
            UpdateSongStackPanel.Visibility = Visibility.Visible;

            SongNameTextBox.Text = GLOBALS.SELECTED_SONG.title;
            ArtistNameTextBox.Text = SQLiteDataAccess.LoadArtists(GLOBALS.SELECTED_SONG.artist_id)[0].artist_name;
            GenreTextBox.Text = GLOBALS.SELECTED_SONG.genre;
            YearTextBox.Text = "" + GLOBALS.SELECTED_SONG.year;
            LoadImageButton.Content = GLOBALS.SELECTED_SONG.artwork.Split('/')[GLOBALS.SELECTED_SONG.artwork.Split('/').Length - 1];
            ChordsTextBox.IsEnabled = true;
            ChordsTextBox.Text = GLOBALS.SELECTED_SONG.lyrics;

            BackBtn.Visibility = Visibility.Hidden;
            CancelBtn.Visibility = Visibility.Visible;
            SaveUpdatesBtn.Visibility = Visibility.Visible;

        }

        public void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Odaberite naslovnu stranu";
            op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (op.ShowDialog() == true)
            {
                char sep = '\\';
                string[] splt = op.FileName.Split(sep);
                GLOBALS.SELECTED_SONG.artwork = splt[splt.Length - 1];
                MessageBox.Show("Uspešno ste učitali naslovnu stranu: " + GLOBALS.SELECTED_SONG.artwork);
                LoadImageButton.Content = GLOBALS.SELECTED_SONG.artwork;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            GLOBALS.SELECTED_SONG = null;
        }

        private void SaveUpdatesBtn_Click(object sender, RoutedEventArgs e)
        {
            Song song = new Song();
            song.song_id = GLOBALS.SELECTED_SONG.song_id;
            song.artwork = (string)LoadImageButton.Content;
            song.year = int.Parse(YearTextBox.Text);
            song.genre = GenreTextBox.Text;
            song.title = SongNameTextBox.Text;
            song.lyrics = ChordsTextBox.Text;
            if (SongNameTextBox.Text.Trim().Equals("") || ArtistNameTextBox.Text.Trim().Equals("") || YearTextBox.Text.Trim().Equals("") || GenreTextBox.Text.Trim().Equals("") || LoadImageButton.Content.Equals("") || ChordsTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Nisu popunjena sva polja!");
                return;
            }

            SQLiteDataAccess.updateSong(song);
            MessageBox.Show("Uspesno ste promenili pesmu!");

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            SQLiteDataAccess.DeleteSong(GLOBALS.SELECTED_SONG);
        }
    }
}

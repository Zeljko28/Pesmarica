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
using Pesmarica.Database;
using System.Text.RegularExpressions;

namespace Pesmarica.View
{
    /// <summary>
    /// Interaction logic for AddSongView.xaml
    /// </summary>
    public partial class AddSongView : UserControl
    {
        Song song = new Song();

        public AddSongView()
        {
            InitializeComponent();
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
                song.artwork = splt[splt.Length - 1];
                MessageBox.Show("Uspešno ste učitali naslovnu stranu: " + song.artwork);
                LoadImageButton.Content = song.artwork;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Zelite da obrisete unete podatke?", "AddSongView", MessageBoxButton.YesNo);

            switch (res) 
            {
                case MessageBoxResult.Yes:
                    //MessageBox.Show("Brisemo sve iz textBoxova");
                    AddLyricsTextBox.Text = "";
                    ArtistNameTextBox.Text = "";
                    GenreTextBox.Text = "";
                    YearTextBox.Text = "";
                    SongNameTextBox.Text = "";
                    LoadImageButton.Content = "";

                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void AddSongButton_Click(object sender, RoutedEventArgs e)
        {
            Song song = new Song();
            if (SongNameTextBox.Text.Trim().Equals("") || ArtistNameTextBox.Text.Trim().Equals("") || YearTextBox.Text.Trim().Equals("") || GenreTextBox.Text.Trim().Equals("") || LoadImageButton.Content.Equals("") || AddLyricsTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Niste popunili sva polja!");
                return;
            }
            song.title = SongNameTextBox.Text;
            if (SQLiteDataAccess.LoadArtists(-1, ArtistNameTextBox.Text) == null) 
            {
                Artist artist = new Artist();
                artist.artist_name = ArtistNameTextBox.Text;
                SQLiteDataAccess.saveArtist(artist);
            }
            song.artist_id = SQLiteDataAccess.LoadArtists(-1, ArtistNameTextBox.Text)[0].artist_id;
            song.artwork = LoadImageButton.Content.ToString();
            song.year = int.Parse(YearTextBox.Text);
            song.lyrics = AddLyricsTextBox.Text;
            song.genre = GenreTextBox.Text;

            SQLiteDataAccess.saveSong(song);
            MessageBox.Show("Uspesno ste dodali pesmu!");

        }

        private void AddLyricsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string[] txt = AddLyricsTextBox.Text.Split();
            /*AddLyricsTextBox.Clear();
            string regEx = @"(\[.?+\])";
            Regex re = new Regex(regEx);
            for (int i = 0; i < txt.Length; i++) 
            {
                if (re.IsMatch(txt[i])) 
                {
                    txt[i] = "\b" + txt[i] + "\b0";
                }
                AddLyricsTextBox.Text = AddLyricsTextBox.Text + txt[i] + " ";
            }*/

        }
    }
}

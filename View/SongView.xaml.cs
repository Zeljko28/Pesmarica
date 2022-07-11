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
using Pesmarica;
using Pesmarica.Core;
using Pesmarica.Database;
using Pesmarica.ViewModel;
using System.Diagnostics;

namespace Pesmarica.View
{
    /// <summary>
    /// Interaction logic for SongView.xaml
    /// </summary>
    /// 

    //public event EventHandler<> SomethingHasChanged;

    public partial class SongView : UserControl
    {
        public SongView()
        {
            InitializeComponent();
            OpenSongButton.Visibility = Visibility.Hidden;
            DeleteSongButton.Visibility = Visibility.Hidden;

            List<Song> songs;
            songs = SQLiteDataAccess.LoadSongs();
            populateList(songs);
            SortComboBox.ItemsSource = new List<string> {"Naslov", "Izvodjac", "Godina"};
        }

        public void populateList(List<Song> songs)
        {
            ListViewAllSongs.ItemsSource = songs;
        }

        private void ListViewAllSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Song song = (Song)ListViewAllSongs.SelectedItem;
            if (song == null) { return; }
            List<Artist> artists = SQLiteDataAccess.LoadArtists(song.artist_id);
            ArtistSelected.Text = "Izvodjac: " + artists[0].artist_name;
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(song.artwork);
            src.EndInit();
            ImageSelected.Source = src;
            TitleSelected.Text = "Naziv: " + song.title;
            GenreSelected.Text = "Zanr: " + song.genre;

            OpenSongButton.Visibility = Visibility.Visible;
            DeleteSongButton.Visibility = Visibility.Visible;
            //OpenSongButton.Command.CanExecute(true);
        }

        public void OpenSongButton_Click(object sender, RoutedEventArgs e)
        {
            Song song = (Song)ListViewAllSongs.SelectedItem;
            if(song == null)
            {
                MessageBox.Show("Niste odabrali pesmu iz liste!");
                return;
            }
            GLOBALS.SELECTED_SONG = song;
            MessageBox.Show(song.artwork);
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sort = (string)SortComboBox.SelectedItem;
            if (sort.Equals("Naslov")) { sort = "title"; }
            else if (sort.Equals("Izvodjac")) { sort = "artist_id"; }
            else if (sort.Equals("Godina")) { sort = "year"; }
            ListViewAllSongs.UnselectAll();
            List<Song> songs;
            songs = SQLiteDataAccess.LoadSongs(-1, "", sort);
            populateList(songs);

        }

        private void DeleteSongButton_Click(object sender, RoutedEventArgs e)
        {
            Song song = (Song)ListViewAllSongs.SelectedItem;
            SQLiteDataAccess.DeleteSong(song);
        }
    }
}

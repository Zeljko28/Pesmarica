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

namespace Pesmarica.View
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();

            if (GLOBALS.SEARCH != "")
            {
                List<Song> songs;
                SearchTextBox.Text = GLOBALS.SEARCH;
                songs = SQLiteDataAccess.LoadSongs(-1, GLOBALS.SEARCH);
                populateList(songs);
            }
            else
            {
                List<Song> songs;
                songs = SQLiteDataAccess.LoadSongs();
                populateList(songs);
            }
        }

        public void populateList(List<Song> songs)
        {
            ListViewAllSongs.ItemsSource = songs;
        }

        private void ListViewAllSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Song song = (Song)ListViewAllSongs.SelectedItem;
            GLOBALS.SELECTED_SONG = song;
            //OpenSongButton.Command.CanExecute(true);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Song> songs;
            songs = SQLiteDataAccess.LoadSongs(-1, SearchTextBox.Text);
            populateList(songs);

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<Song> songs;
            songs = SQLiteDataAccess.LoadSongs(-1, SearchTextBox.Text);
            populateList(songs);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pesmarica.Core;

namespace Pesmarica.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SearchViewCommand { get; set; }
        public RelayCommand SongViewCommand { get; set; }
        public RelayCommand AddSongViewCommand { get; set; }
        public RelayCommand ShowSongViewCommand { get; set; }
        public RelayCommand ProfileViewCommand { get; set; }
        public RelayCommand ShowUsersViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public SearchViewModel SearchVM { get; set; }
        public SongViewModel SongVM { get; set; }
        public AddSongViewModel AddSongVM { get; set; }
        public ShowSongViewModel ShowSongVM { get; set; }
        public ProfileViewModel ProfileVM { get; set; }
        public ShowUsersViewModel ShowUsersVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            SearchVM = new SearchViewModel();
            SongVM = new SongViewModel();
            AddSongVM = new AddSongViewModel();
            ShowSongVM = new ShowSongViewModel();
            ProfileVM = new ProfileViewModel();
            ShowUsersVM = new ShowUsersViewModel();

            CurrentView = HomeVM;


            HomeViewCommand = new RelayCommand(o => {
                CurrentView = HomeVM;
            });

            SearchViewCommand = new RelayCommand(o => {
                CurrentView = SearchVM;
            });

            SongViewCommand = new RelayCommand(o => {
                CurrentView = SongVM;
            });

            AddSongViewCommand = new RelayCommand(o => {
                CurrentView = AddSongVM;
            });

            ShowSongViewCommand = new RelayCommand(o => {
                CurrentView = ShowSongVM;
            });

            ProfileViewCommand = new RelayCommand(o => {
                CurrentView = ProfileVM;
            });

            ShowUsersViewCommand = new RelayCommand(o => {
                CurrentView = ShowUsersVM;
            });
        }

    }
}

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

namespace Pesmarica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(User user)
        {
            GLOBALS.USERNAME = user.GetUsername();
            GLOBALS.PASSWORD = user.GetPassword();
            GLOBALS.TYPE = user.GetUserType();
            InitializeComponent();

            if (user.GetUserType().ToLower() == "regular")
            {
                HomeBtn.Visibility = Visibility.Visible;
                SearchBtn.Visibility = Visibility.Visible;
                MusicBtn.Visibility = Visibility.Visible;
                HomeBtn.IsSelected = true;
                UsersBtn.IsSelected = false;
            }
            else if (user.GetUserType().ToLower() == "administrator")
            {
                UsersBtn.Visibility = Visibility.Visible;
                HomeBtn.IsSelected = false;
                UsersBtn.IsSelected = true;
            }
        }

        private void _this_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ToggleBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (NavBarMenu != null && NavBarMenu.Width == 70)
            {
                NavBarMenu.Width = 250;
            }
            else if(NavBarMenu != null && NavBarMenu.Width == 250)
            {
                NavBarMenu.Width = 70;
            }
            */
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e) 
        {
            Close();
        }

        private void ToggleBtn_Checked(object sender, RoutedEventArgs e)
        {
            //bg.Opacity = 0.2;
        }

        private void ToggleBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            //bg.Opacity = 0.9;
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            ((ListViewItem)sender).Background = Brushes.LightGray;
            ((ListViewItem)sender).Opacity = 0.6;
            if (((ListViewItem)sender).Name.Equals("HomeBtn"))
            {
                HomeRadio_Click(HomeRadio, null);
            }
            if (((ListViewItem)sender).Name.Equals("SearchBtn"))
            {
                SearchRadio_Click(SearchRadio, null);
            }
            if (((ListViewItem)sender).Name.Equals("MusicBtn"))
            {
                MusicRadio_Click(MusicRadio, null);
            }
            if (((ListViewItem)sender).Name.Equals("ProfileBtn"))
            {
                ProfileRadio_Click(ProfileRadio, null);
            }
            if (((ListViewItem)sender).Name.Equals("LogoutBtn"))
            {
                LogoutRadio_Click(LogoutRadio, null);
            }
            if (((ListViewItem)sender).Name.Equals("UsersBtn"))
            {
                UsersRadio_Click(UsersRadio, null);
            }
        }

        private void ListViewItem_Unselected(object sender, RoutedEventArgs e)
        {
            ((ListViewItem)sender).Background = Brushes.Transparent;
            ((ListViewItem)sender).Opacity = 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MusicRadio_Click(object sender, RoutedEventArgs e)
        {
            ICommand command;
            command = ((Button)sender).Command;
            ((Button)sender).Command.Execute(command);

            MusicBtn.Background = Brushes.LightGray;
            MusicBtn.Opacity = 0.6;

            ListViewItem_Unselected(HomeBtn, null);
            ListViewItem_Unselected(SearchBtn, null);
            ListViewItem_Unselected(ProfileBtn, null);
            ListViewItem_Unselected(LogoutBtn, null);
        }
        private void ProfileRadio_Click(object sender, RoutedEventArgs e)
        {
            ICommand command;
            command = ((Button)sender).Command;
            ((Button)sender).Command.Execute(command);

            ProfileBtn.Background = Brushes.LightGray;
            ProfileBtn.Opacity = 0.6;

            ListViewItem_Unselected(HomeBtn, null);
            ListViewItem_Unselected(SearchBtn, null);
            ListViewItem_Unselected(MusicBtn, null);
            ListViewItem_Unselected(LogoutBtn, null);
            ListViewItem_Unselected(UsersBtn, null);
        }

        private void SearchRadio_Click(object sender, RoutedEventArgs e)
        {
            ICommand command;
            command = ((Button)sender).Command;
            ((Button)sender).Command.Execute(command);

            SearchBtn.Background = Brushes.LightGray;
            SearchBtn.Opacity = 0.6;

            ListViewItem_Unselected(HomeBtn, null);
            ListViewItem_Unselected(MusicBtn, null);
            ListViewItem_Unselected(ProfileBtn, null);
            ListViewItem_Unselected(LogoutBtn, null);
        }

        private void HomeRadio_Click(object sender, RoutedEventArgs e)
        {
            if(HomeRadio == null) { return; }
            ICommand command;
            command = ((Button)sender).Command;
            ((Button)sender).Command.Execute(command);

            HomeBtn.Background = Brushes.LightGray;
            HomeBtn.Opacity = 0.6;

            ListViewItem_Unselected(MusicBtn, null);
            ListViewItem_Unselected(SearchBtn, null);
            ListViewItem_Unselected(ProfileBtn, null);
            ListViewItem_Unselected(LogoutBtn, null);
        }

        private void LogoutRadio_Click(object sender, RoutedEventArgs e)
        {
            LogoutBtn.Background = Brushes.LightGray;
            LogoutBtn.Opacity = 0.6;
            

            ListViewItem_Unselected(HomeBtn, null);
            ListViewItem_Unselected(SearchBtn, null);
            ListViewItem_Unselected(ProfileBtn, null);
            ListViewItem_Unselected(MusicBtn, null);
            ListViewItem_Unselected(UsersBtn, null);


            GLOBALS.USERNAME = "";
            GLOBALS.PASSWORD = "";
            GLOBALS.TYPE = "";

            LogIn window = new LogIn();
            this.Close();
            window.Show();
        }

        private void AddSongBtnHidden() 
        {
        
        }

        public static void AddSongClick() 
        {
            
        }

        private void UsersRadio_Click(object sender, RoutedEventArgs e)
        {
            if (HomeRadio == null) { return; }
            ICommand command;
            command = ((Button)sender).Command;
            ((Button)sender).Command.Execute(command);

            UsersBtn.Background = Brushes.LightGray;
            UsersBtn.Opacity = 0.6;

            ListViewItem_Unselected(ProfileBtn, null);
            ListViewItem_Unselected(LogoutBtn, null);
        }
    }
}

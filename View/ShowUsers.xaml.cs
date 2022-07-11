using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ShowUsers.xaml
    /// </summary>
    public partial class ShowUsers : UserControl
    {
        public ShowUsers()
        {
            InitializeComponent();

            List<RegularUser> users = SQLiteDataAccess.LoadUsers();
            for (int i = 0; i < users.Count; i++) 
            {
                Trace.WriteLine(users[i].regularUserId);
            }
            populateList(users);
        }

        public void populateList(List<RegularUser> users)
        {
            listViewStack.ItemsSource = users;
        }

        private void getSelectedUser(object sender, SelectionChangedEventArgs e)
        {
            RegularUser regular = (RegularUser)listViewStack.SelectedItems[0];
            GLOBALS.SELECTED_USER_ID = regular.regularUserId;
        }

        private void listViewStack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Zelite da obrisete korisnika?", "ShowUsers", MessageBoxButton.YesNo);

            switch (res)
            {
                case MessageBoxResult.Yes:
                    SQLiteDataAccess.DeleteUser(GLOBALS.SELECTED_USER_ID);

                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}

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
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        UserFactory factory = new UserFactory();
        User user;
        public ProfileView()
        {
            InitializeComponent();

            user = factory.CreateUser(GLOBALS.TYPE.ToLower());
            user.SetUsername(GLOBALS.USERNAME);
            user.SetPassword(GLOBALS.PASSWORD);
            user.SetUserType(GLOBALS.TYPE.ToLower());

            passwordText.Password = user.GetPassword();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordText.Password.Equals("")) { MessageBox.Show("Niste uneli lozinku!"); }
            else
            {
                user.SetPassword(passwordText.Password);
                SQLiteDataAccess.updateUsersPassword(user);
                MessageBox.Show("Lozinka uspešno promenjena!");
            }
        }
    }
}

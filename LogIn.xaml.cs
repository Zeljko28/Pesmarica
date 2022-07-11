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
using System.Windows.Shapes;
using Pesmarica.Database;
using Pesmarica;

namespace Pesmarica
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        private void lblRegistration_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SignUp window = new SignUp();
            this.Close();
            window.Show();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GrantAcces(User user)
        {
            MainWindow window = new MainWindow(user);
            this.Close();
            window.Show();
        }

        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            UserFactory factory = new UserFactory();
            User regular = factory.CreateUser("regular");
            User admin = factory.CreateUser("administrator");

            regular.SetUsername(usernameText.Text);
            regular.SetPassword(passwordText.Password.ToString());
            regular.SetUserType("regular");

            admin.SetUsername(usernameText.Text);
            admin.SetPassword(passwordText.Password.ToString());
            admin.SetUserType("administrator");

            if (SQLiteDataAccess.checkIfExist(regular))
            {
                GrantAcces(regular);
            }
            else if (SQLiteDataAccess.checkIfExist(admin))
            {
                GrantAcces(admin);
            }
            else
            {
                MessageBox.Show("Netačno korisničko ime ili lozinka! Molimo pokušajte ponovo.");
            }
        }
    }
}

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

namespace Pesmarica
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
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

        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            RegularUser r = new RegularUser();
            r.SetUserType("regular");
            r.name = nameText.Text.ToString();
            r.lastname = lastnameText.Text;
            r.username = usernameText.Text.ToString();
            r.password = passwordText.Password;
            

            Administrator admin = new Administrator();
            admin.SetUserType("administrator");
            admin.username = r.username;




            if (r.name.Equals("") || r.lastname.Equals("") || r.username.Equals("") || r.password.Equals(""))
            {
                MessageBox.Show("Niste uneli sve podatke!");
            }

            else if (SQLiteDataAccess.checkIfUsernameExists(admin))
            {
                MessageBox.Show("Korisnik sa ovim korisnickim imenom već postoji!");
            }

            else if (!SQLiteDataAccess.checkIfUsernameExists(r))
            {
                SQLiteDataAccess.saveUser(r);
                LogIn window = new LogIn();
                this.Close();
                window.Show();
            }

            else
            {
                MessageBox.Show("Student sa ovim podacima već postoji!"); //Korisnik vec postoji
            }

        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            LogIn window = new LogIn();
            this.Close();
            window.Show();
        }

    }
}

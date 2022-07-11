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
using Pesmarica.ViewModel;

namespace Pesmarica.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //ICommand comm;
            //comm = this.AddSong.Command;
            //MessageBox.Show("" + comm);
            //AddSong.Command.Execute(null);
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GLOBALS.SEARCH = "Riblja Čorba";

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GLOBALS.SEARCH = "Prljavo kazalište";

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GLOBALS.SEARCH = "Parni Valjak";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            GLOBALS.SEARCH = "Hladno pivo";
        }
    }
}

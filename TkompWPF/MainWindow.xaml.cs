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

namespace TkompWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataAccess da;
        public MainWindow()
        {
            InitializeComponent();
            da = new DataAccess();
        }

        private void DBTestButton_Click(object sender, RoutedEventArgs e) {

            DBGetDataButton.IsEnabled = false;
            dataGrid.ItemsSource = null;

            if (LoginText.Text == string.Empty || PasswordText.Password == string.Empty) {
                MessageBox.Show("Proszę uzupełnić pole Login/Hasło");
                return;
            }

            da.Login = LoginText.Text;
            da.Password = PasswordText.Password;
            PasswordText.Password = string.Empty;
            bool Successful = da.SetConnection();

            if (Successful) {
                MessageBox.Show("Połączenie powiodło się!","Test połączenia");
                DBGetDataButton.IsEnabled = true;
            };
            

        }

        private void DBGetDataButton_Click(object sender, RoutedEventArgs e) {
            dataGrid.ItemsSource = (da.GetData()).DefaultView;
        }
    }
}

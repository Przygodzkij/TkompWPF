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
        DataAccess dataAccess;
        public MainWindow()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
        }

        private void TestButton_Click(object sender, RoutedEventArgs e) {

            GetDataButton.IsEnabled = false;
            dataGrid.ItemsSource = null;

            if (LoginText.Text == string.Empty || PasswordText.Password == string.Empty) {
                MessageBox.Show("Proszę uzupełnić pole Login/Hasło","Błąd logowania");
                return;
            }

            
            bool Successful = dataAccess.VerifyConnection(LoginText.Text, PasswordText.Password);
            PasswordText.Password = string.Empty;
            if (Successful) {
                MessageBox.Show("Połączenie powiodło się!","Test połączenia");
                GetDataButton.IsEnabled = true;
            };
            

        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e) {
            dataGrid.ItemsSource = (dataAccess.GetData()).DefaultView;
        }
    }
}

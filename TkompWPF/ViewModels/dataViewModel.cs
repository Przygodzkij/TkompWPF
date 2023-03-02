using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using TkompWPF.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.ComponentModel;

namespace TkompWPF.ViewModels
{
    public class dataViewModel:viewModelBase
    {
        DataAccess dataAccess;
        

        private DataTable sqlDataTable;

        public DataTable SqlDataTable
        {
            get { return sqlDataTable; }
            set { sqlDataTable = value; OnPropertyChanged("SqlDataTable"); }
        }


        private string _login;
        private string _password;
        public string Login { get { return _login; } set { _login = value; OnPropertyChanged("Login"); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged("Password"); } }
        public dataViewModel() {

            dataAccess = new DataAccess();
            sqlDataTable = new DataTable();
            _login = string.Empty;
            _password = string.Empty;
        }

        

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {

            GetDataButton.IsEnabled = false;
            dataGrid.ItemsSource = null;

            if (LoginText.Text == string.Empty || PasswordText.Password == string.Empty)
            {
                MessageBox.Show("Proszę uzupełnić pole Login/Hasło", "Błąd logowania");
                return;
            }


            bool Successful = dataAccess.VerifyConnection(LoginText.Text, PasswordText.Password);
            PasswordText.Password = string.Empty;
            if (Successful)
            {
                MessageBox.Show("Połączenie powiodło się!", "Test połączenia");
                GetDataButton.IsEnabled = true;
            };


        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            SqlDataTable = dataAccess.GetData(Login, Password);
        }




    }
}

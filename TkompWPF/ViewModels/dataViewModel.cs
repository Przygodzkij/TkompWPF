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
using System.Security;
using System.Net;
using System.Reflection.Metadata;

namespace TkompWPF.ViewModels
{
    public class DataViewModel: INotifyPropertyChanged {

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region DataAccess 
        DataAccessModel dataAccess;
        private NetworkCredential _credentials;

        public NetworkCredential Credentials {
            get { return _credentials; }
            set { _credentials = value; OnPropertyChanged("Credentials"); }
        }

        private DataTable sqlDataTable;

        public DataTable SqlDataTable
        {
            get { return sqlDataTable; }
            set { sqlDataTable = value; OnPropertyChanged("SqlDataTable"); }
        }
        #endregion

        #region Commands

        private RelayCommand _checkConnectionCommand;

        public RelayCommand CheckConnectionCommand {
            get { return _checkConnectionCommand; }
        }

        private RelayCommand _loadDataCommand;

        public RelayCommand LoadDataCommand {
            get { return _loadDataCommand; }
            set { _loadDataCommand = value; }
        }

        #endregion

        #region UI 
        private string _message;

        public string Message {
            get { return _message; }
            set { _message = value; OnPropertyChanged("Message"); }
        }

        private bool enableDataLoading;

        public bool EnableDataLoading {
            get { return enableDataLoading; }
            set { enableDataLoading = value; OnPropertyChanged("EnableDataLoading"); }
        }
        #endregion

        public DataViewModel() {

            dataAccess = new DataAccessModel();
            sqlDataTable = new DataTable();
            Credentials = new NetworkCredential();
            _checkConnectionCommand = new RelayCommand(CheckConnection);
            _loadDataCommand = new RelayCommand(LoadData);
            EnableDataLoading = false;
        }
        public void ClearData() {
            SqlDataTable.Clear();
        }

        //Loads data from DataAccess
        public void LoadData() {
            ClearData();
            try {
                SqlDataTable = dataAccess.GetData(Credentials);
                Message = "Pobrano dane!";
            }catch (Exception ex) {
                Message = ex.Message;
            }
        }

        //Check if given credentials are valid, and test connection.
        public void CheckConnection() {

            ClearData();
            EnableDataLoading = false;

            if (Credentials.UserName == string.Empty || Credentials.Password == null) {
                Message = "Proszę uzupełnić pole Login/Hasło";
                return;
            }

            try {
                if(dataAccess.VerifyCredentials(Credentials)) {
                    Message = "Połączenie powiodło się!";
                    EnableDataLoading = true;
                }
                    
            }
            catch (Exception ex) {
                Message = ex.Message;
            }
        }
    }
}

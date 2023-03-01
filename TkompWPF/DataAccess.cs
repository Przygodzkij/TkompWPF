using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using System.Windows;
using System.Data.Common;
using System.Windows.Controls;

namespace TkompWPF {
    internal class DataAccess {

        private string _login = string.Empty;
        private string _password = string.Empty;
        private static string _connectionServer = @"127.0.0.1";
        private static string _connectionPort = @"1433";
        private static string _connectionDatabase = @"DevData";
       
        public string Login { get { return _login; } set { _login = value; } }
        public string Password { get { return _password; } set { _password = value; } }

        private SqlConnection GetConnection() {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder() {
                DataSource = _connectionServer,
                InitialCatalog = _connectionDatabase,
                UserID = _login,
                Password = _password
            };

            return new SqlConnection(builder.ConnectionString);
        }

        
        public DataTable GetData() {
            string Query = @"SELECT
                                sysTab.name AS 'Tabela',
                                sysCol.name AS 'Kolumna',
                                sysType.name As 'Typ'
                            from sys.tables sysTab
                            join sys.columns sysCol on sysTab.object_id = sysCol.object_id
                            join sys.systypes sysType on sysType.xtype = sysCol.system_type_id
                            where sysType.name like 'int'";
            SqlCommand Command = new SqlCommand(Query, GetConnection());
            DataTable sqlResultTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(Command);
            adapter.Fill(sqlResultTable);
            return sqlResultTable;
        }
    

        public bool SetConnection() {




            SqlConnection cnn = GetConnection();
            try {
                cnn.Open();
            }
            catch (SqlException sqlEx) {
                String errorText = string.Empty;


                MessageBox.Show(@$"{sqlEx.Message}", "Błąd łącczenia z serwerem");
                return false;
            }
            cnn.Close();
            return true;
        }


    }
}

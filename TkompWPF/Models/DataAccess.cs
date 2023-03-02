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

namespace TkompWPF.Models
{
    internal class DataAccess
    {

        
        private static string _connectionServer = @"127.0.0.1";
        private static string _connectionPort = @"1433";
        private static string _connectionDatabase = @"DevData";


        

        private SqlConnection GetSQLConnection(string login, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = _connectionServer,
                InitialCatalog = _connectionDatabase,
                UserID = login,
                Password = password
            };

            return new SqlConnection(builder.ConnectionString);
        }


        public DataTable GetData(string login, string password)
        {
            string Query = @"SELECT
                                sysTab.name AS 'Tabela',
                                sysCol.name AS 'Kolumna',
                                sysType.name As 'Typ'
                            from sys.tables sysTab
                            join sys.columns sysCol on sysTab.object_id = sysCol.object_id
                            join sys.systypes sysType on sysType.xtype = sysCol.system_type_id
                            where sysType.name like 'int'";

            SqlCommand Command = new SqlCommand(Query, GetSQLConnection(login,password));
            DataTable sqlResultTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(Command);
            adapter.Fill(sqlResultTable);
            return sqlResultTable;
        }


        public bool VerifyCredentials(string login, string password)
        {

        


            SqlConnection cnn = GetSQLConnection(login,password);
            try
            {
                cnn.Open();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(@$"{sqlEx.Message}", "Błąd łącczenia z serwerem");
                return false;
            }
            cnn.Close();
            return true;
        }


    }
}

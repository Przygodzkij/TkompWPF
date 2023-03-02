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
using System.Security;
using System.Runtime.InteropServices;
using System.Net;

namespace TkompWPF.Models
{
    internal class DataAccess
    {

        
        private static readonly string _connectionServer = @"127.0.0.1";
        private static readonly string _connectionDatabase = @"DevData";


        

        private SqlConnection GetSQLConnection(string login, string password)
        {
            IntPtr bstr = IntPtr.Zero;
            
           
                
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder() {
                    DataSource = _connectionServer,
                    InitialCatalog = _connectionDatabase,
                    UserID = login,
                    Password = password
                };
                return new SqlConnection(builder.ConnectionString);
        }


        public DataTable GetData(NetworkCredential credentials)
        {
            DataTable sqlResultTable = new DataTable();

            try {
                string Query = @"SELECT
                                sysTab.name AS 'Tabela',
                                sysCol.name AS 'Kolumna',
                                sysType.name As 'Typ'
                            from sys.tables sysTab
                            join sys.columns sysCol on sysTab.object_id = sysCol.object_id
                            join sys.systypes sysType on sysType.xtype = sysCol.system_type_id
                            where sysType.name like 'int'";

                SqlCommand Command = new SqlCommand(Query, GetSQLConnection(credentials.UserName, credentials.Password));
                SqlDataAdapter adapter = new SqlDataAdapter(Command);
                adapter.Fill(sqlResultTable);
            }catch (Exception ex) {

                throw new Exception(@$"Wystąpił błąd przy ładowaniu danych:{ex.Message}");
            }
                
            
            
            return sqlResultTable;
        }


        public bool VerifyCredentials(NetworkCredential credentials)
        {
            


            SqlConnection cnn = GetSQLConnection(credentials.UserName, credentials.Password);
            try {
                cnn.Open();
            }
            catch (SqlException sqlEx) {
                throw new Exception(@$"Weryfikacja nieudana:{sqlEx.Message}");
            }
            finally { cnn.Close(); }
            return true;
        }


    }
}

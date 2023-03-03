using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace TkompWPF.Models
{
    internal class DataAccessModel
    {

        //Static connection details
        private static readonly string _connectionServer = @"127.0.0.1";
        private static readonly string _connectionDatabase = @"DevData";



        #region SqlConnection builder
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

        #endregion

        #region SQL Data acquisition method
        public DataTable GetData(NetworkCredential credentials)
        {
            DataTable sqlResultTable = new DataTable();

            try
            {
                /*Queries information about columns in a table for currently selected database.
                 Alternatively we could use: 
                 SELECT * FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_CATALOG LIKE 'DevData' AND DATA_TYPE LIKE 'int'
                 * */
                string Query = @"SELECT
                                sysTab.name AS 'Tabela',
                                sysCol.name AS 'Kolumna',
                                sysType.name As 'Typ'
                            FROM sys.tables sysTab
                            JOIN sys.columns sysCol ON sysTab.object_id = sysCol.object_id
                            JOIN sys.systypes sysType ON sysType.xtype = sysCol.system_type_id
                            WHERE sysType.name LIKE 'int'";

                SqlCommand Command = new SqlCommand(Query, GetSQLConnection(credentials.UserName, credentials.Password));
                SqlDataAdapter adapter = new SqlDataAdapter(Command);
                adapter.Fill(sqlResultTable);
            }
            catch (Exception ex)
            {
                throw new Exception(@$"Wystąpił błąd przy ładowaniu danych: {ex.Message}");
            }



            return sqlResultTable;
        }

        #endregion

        #region SQL connection verification
        public bool VerifyCredentials(NetworkCredential credentials)
        {



            SqlConnection cnn = GetSQLConnection(credentials.UserName, credentials.Password);
            try
            {
                cnn.Open();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(@$"Weryfikacja nieudana: {sqlEx.Message}");
            }
            finally { cnn.Close(); }
            return true;
        }
        #endregion


    }
}

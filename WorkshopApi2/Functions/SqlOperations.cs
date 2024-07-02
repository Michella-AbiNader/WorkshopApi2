using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WorkshopApi2.Functions
{
    public class SqlOperations
    {
        string connectionString = "Data Source=DESKTOP-5GQU1D3;Initial Catalog=AOU_workshop;User ID=micha; Password=micha123 ";

        public DataTable sqlToDataTable(string query, params SqlParameter[] sqlParameter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //DataTable is for reading data from the database
                    DataTable dataTable = new DataTable();
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = query;
                        command.CommandType = CommandType.Text;
                        if(sqlParameter != null && sqlParameter.Length > 0)
                        {
                            command.Parameters.AddRange(sqlParameter);
                        }
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                    connection.Close();
                    return dataTable;

                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
        }
        //TO UPDATE, INSERT AND DELETE
        public void executeSql(string query, params SqlParameter[] sqlParameter)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = query;
                        command.CommandType = CommandType.Text;
                        command.Connection = sqlConnection;
                        if (sqlParameter != null && sqlParameter.Length > 0)
                        {
                            command.Parameters.AddRange(sqlParameter);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
            }
        }
    }
}
using ApprenticeTipsSoftware.RequestResponseModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprenticeTipsSoftware.Classes
{
    public class DatabaseInserter
    {
        public int InsertData(string columnNames, string values, string tableName, string catalog)
        {
            int currentId = 0;
            string connectionstring = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;
            connectionstring = $@"Data Source=LAPTOP-7GM05PUF;Initial Catalog={catalog}; Integrated Security=True;";
            sql = $"insert into {tableName} ({columnNames})";
            sql += $"values('{values}') ";
            sql += "select scope_identity()";
            connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    currentId = Convert.ToInt32(dataReader.GetDecimal(0));
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection!");
            }
            return currentId;
        }

        public int InsertCheckboxData(DetailAdderRequest request, int currentId)
        {
            int newId = 0;
            string connectionstring = null;
            SqlConnection connection;
            SqlCommand command;
            StringBuilder sql = new StringBuilder();
            SqlDataReader dataReader;
            connectionstring = $@"Data Source=LAPTOP-7GM05PUF;Initial Catalog=webform; Integrated Security=True;";
            sql.Append($"update Contact set ");
            sql.Append($"agriculture = '{request.Contact.Agriculture}',");
            sql.Append($"business = '{request.Contact.Business}',");
            sql.Append($"care = '{request.Contact.Care}',");
            sql.Append($"catering = '{request.Contact.Catering}',");
            sql.Append($"construction = '{request.Contact.Construction}',");
            sql.Append($"creative = '{request.Contact.Creative}',");
            sql.Append($"digital = '{request.Contact.Digital}',");
            sql.Append($"education = '{request.Contact.Education}',");
            sql.Append($"engineering = '{request.Contact.Engineering}',");
            sql.Append($"hair = '{request.Contact.Hair}',");
            sql.Append($"health = '{request.Contact.Health}',");
            sql.Append($"legal = '{request.Contact.Legal}',");
            sql.Append($"protective = '{request.Contact.Protective}',");
            sql.Append($"sales = '{request.Contact.Sales}',");
            sql.Append($"transport = '{request.Contact.Transport}'");
            sql.Append($"where id = {currentId} ");
            sql.Append("select scope_identity()");
            connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                command = new SqlCommand(sql.ToString(), connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    newId = Convert.ToInt32(dataReader.GetDecimal(0));
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection!");
            }
            return newId;
        }
    }
}

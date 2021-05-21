using ApprenticeTipsSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprenticeTipsSoftware.Classes
{
    public class DatabaseRetriever
    {
        public List<ApprenticeshipModel> GetApprenticeships(ApprenticeshipFinderRequest request)
        {
            DataTable dt = new DataTable();

            var apprenticeships = new List<ApprenticeshipModel>();

            string connectionString = null;
            SqlConnection connection;
            SqlCommand command;
            StringBuilder sql = new StringBuilder();
            SqlDataReader dataReader;
            connectionString = $@"Data Source=LAPTOP-7GM05PUF;Initial Catalog=webform; Integrated Security=True;";
            sql.Append($"select * from Apprenticeships ");
            sql.Append($"where 1 = 1 ");

            if (request.BoolRoute)
            {
                sql.Append($"and route = '{request.Route}'");
            }

            if (request.BoolLevel)
            {
                sql.Append($"and level = '{request.Level}'");
            }

            if (request.BoolStatus)
            {
                sql.Append($"and status = '{request.Status}'");
            }

            if (request.BoolDuration)
            {
                sql.Append($"and duration = '{request.Duration}'");
            }


            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql.ToString(), connection);
                dataReader = command.ExecuteReader();
                dt.Load(dataReader);
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection!");
            }

            foreach (DataRow row in dt.Rows)
            {
                var apprenticeship = new ApprenticeshipModel();

                apprenticeship.Route = row["route"].ToString();
                apprenticeship.Name = row["name"].ToString();
                apprenticeship.Reference = row["reference"].ToString();
                apprenticeship.Status = row["status"].ToString();
                apprenticeship.Level = (int)row["level"];
                apprenticeship.Funding = row["funding"].ToString();
                apprenticeship.Duration = (int)row["duration"];
                apprenticeship.CoreOptions = (int)row["core_options"];
                apprenticeship.Eqa = row["eqa"].ToString();
                apprenticeship.Link = row["link"].ToString();

                apprenticeships.Add(apprenticeship);

            }

            return apprenticeships;

        }
    }
}

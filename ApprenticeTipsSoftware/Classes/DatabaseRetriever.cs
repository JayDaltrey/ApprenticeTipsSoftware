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
        public List<ApprenticeshipModel> GetApprenticeships(ApprenticeshipFinder appGetter, FilterModel selectedFilter)
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

            if (selectedFilter.BoolRoute)
            {
                sql.Append($"and route = '{appGetter.Route}'");
            }

            if (selectedFilter.BoolLevel)
            {
                sql.Append($"and level = '{appGetter.Level}'");
            }

            if (selectedFilter.BoolStatus)
            {
                sql.Append($"and status = '{appGetter.Status}'");
            }

            if (selectedFilter.BoolDuration)
            {
                sql.Append($"and duration = '{appGetter.Duration}'");
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

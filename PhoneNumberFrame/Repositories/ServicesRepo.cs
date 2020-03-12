using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using PhoneNumberFrame.Models;
using Dapper;


namespace PhoneNumberFrame.Repositories
{
    class ServicesRepo
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<MService> GetServices()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = "exec ShowPeople";
                return db.Query<MService>(sql).ToList();
            }
        }
    }
}

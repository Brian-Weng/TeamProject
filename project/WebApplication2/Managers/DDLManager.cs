using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Helpers;

namespace WebApplication2.Managers
{
    public class DDLManager : DBBase
    {
        public DataTable GetCompanyDDL()
        {
            string queryString =
                $@"SELECT * FROM Company";

            List<SqlParameter> dbParameters = new List<SqlParameter>();
   
            var dt = this.GetDataTable(queryString, dbParameters);

            return dt;

        }
    }
}
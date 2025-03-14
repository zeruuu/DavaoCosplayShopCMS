using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DavaoCosplayShopCMS
{
    public class connection
    {
        SqlConnection conn;
        public SqlConnection getCon()
        {
            conn = new SqlConnection("Data Source=Procht;Initial Catalog=CosplayDB;Integrated Security=True;Encrypt=False");
            return conn;
        }
    }
}

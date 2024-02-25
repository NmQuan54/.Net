using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlBanHang.Connections
{
    internal class Connection
    {
        internal static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=QUAN;Initial Catalog=QLBanHang;Integrated Security=True;");

            try
            {
                connection.Open();
            }
            catch (Exception ex) { }
            {
                throw new NotImplementedException();
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KelimeOyunu
{
    public class DatabaseConnect
    {
        public static SqlConnection BaglantiOlustur()
        {
            return new SqlConnection("Server=DESKTOP-B9LSRAQ\\SQLEXPRESS;Database=KelimeOyunu;Trusted_Connection=True;TrustServerCertificate=True");

        }
    }
}

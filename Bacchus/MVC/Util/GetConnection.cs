using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Bacchus.MVC.Util
{
    class GetConnection
    {
        public static SQLiteConnection Connection;

        /// <summary>
        /// Connect the database in the current environment directory.
        /// </summary>
        /// <returns><c>SQLiteConnection</c></c></returns>
        public static SQLiteConnection ConnectionSQLite()
        {
            string Path = "Data Source = " + System.Environment.CurrentDirectory + "/Bacchus.SQLite";
            Console.WriteLine(Path);
            if (Connection == null)
            {
                Connection = new SQLiteConnection(Path);
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();
                return Connection;
            }
            else
            {
                return Connection;
            }
        }
    }
}

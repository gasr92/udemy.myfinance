using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyFinance.Util
{
    public class DAL
    {
        private static string server = "localhost";
        private static string db = "financeiro";
        private static string user = "root";
        private static string pass = "102030";
        private string connectionString = $"server={server};Database={db};Uid={user};Pwd={pass}";
        private MySqlConnection connection;

        public DAL()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        public DataTable RetDataTable(string sql)
        {
            var dt = new DataTable();
            var cmd = new MySqlCommand(sql,connection);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }

        public void Executar(string sql)
        {
            var cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LuckyBikiDeffenceSocketServer
{
    class MySqlManager
    {
        public string server = "127.0.0.1";
        public int port = 4000;
        public string database = "myapiserver";
        public string id = "root";
        public string pw = "suhan1234";
        public string connectionAddress = string.Empty;

        public void Connection()
        {
            connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", server, port, database, id, pw);
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionAddress);
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

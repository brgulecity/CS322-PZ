using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Savamala.Models
{
    public class DbConnection
    {

        private static MySqlConnection _instance = null;


        private DbConnection()
        {
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=savamaladb;";

            try
            {
                _instance = new MySqlConnection(connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }

        }


        public static MySqlConnection Instance
        {
            get
            {
                if (_instance == null)
                {
                    new DbConnection();
                }

                return _instance;
            } 


        }
    }
}
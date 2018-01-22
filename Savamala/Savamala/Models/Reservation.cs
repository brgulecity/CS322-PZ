using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Savamala.Models
{
    public class Reservation
    {
        public static void Reserve(DateTime datum, string ime, string telefon, int brStola, string kafic)
        {
            try
            {
                DbConnection.Instance.Open();
                string query = "INSERT INTO rezervacije(datum, ime, telefon, brStola, kafic) VALUES (@datum, @ime, @telefon, @brStola, @kafic)";
                MySqlCommand cmd = new MySqlCommand(query, DbConnection.Instance);
                cmd.Parameters.AddWithValue("@datum", datum);
                cmd.Parameters.AddWithValue("@ime", ime);
                cmd.Parameters.AddWithValue("@telefon", telefon);
                cmd.Parameters.AddWithValue("@brStola", brStola);
                cmd.Parameters.AddWithValue("@kafic", kafic);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                DbConnection.Instance.Close();
            }
        }

        public static List<int> ReservedList(string kafic, DateTime datum)
        {
            List<int> returnList = new List<int>();

            try
            {
                DbConnection.Instance.Open();
                string query = "SELECT brStola FROM rezervacije WHERE kafic = @kafic AND datum = @datum";
                MySqlCommand cmd = new MySqlCommand(query, DbConnection.Instance);
                cmd.Parameters.AddWithValue("@kafic", kafic);
                cmd.Parameters.AddWithValue("@datum", datum);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    returnList.Add((int)rdr[0]);
                }
                rdr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                DbConnection.Instance.Close();
            }

            return returnList;
        }
    }
}
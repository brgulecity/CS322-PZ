using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Savamala.Models;
using MySql.Data.MySqlClient;

namespace Savamala.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {


            if (Session["user"] == null)
            {
                Response.Redirect("Login", true);
            }

            string temp = "";

            try
            {
                DbConnection.Instance.Open();
                string query = "SELECT * FROM rezervacije";
                MySqlCommand cmd = new MySqlCommand(query, DbConnection.Instance);
                cmd.CommandType = System.Data.CommandType.Text;
                
                 temp = "<table border = '2'><tr><th>ID Rezervacije</th><th>Datum</th><th>Ime</th><th>Telefon</th><th>Br.Stola</th><th>Kafic</th></tr>";

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    temp += "<tr>";

                    temp += "<td>";
                    temp += reader["id"].ToString();
                    temp += "</td>";
                    temp += "<td>";
                    temp += reader["datum"].ToString();
                    temp += "</td>";
                    temp += "<td>";
                    temp += reader["ime"].ToString();
                    temp += "</td>";
                    temp += "<td>";
                    temp += reader["telefon"].ToString();
                    temp += "</td>";
                    temp += "<td>";
                    temp += reader["brStola"].ToString();
                    temp += "</td>";
                    temp += "<td>";
                    temp += reader["kafic"].ToString();
                    temp += "</td>";
                   

                    temp += "</tr>";



                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                DbConnection.Instance.Close();
                
            }

            temp += "</table>";

            ViewData["korisnici"] = temp;


            return View();
        }

        public ActionResult Login()
        {
            

            return View();
        }

        public ActionResult Cafe1()
        {
            ViewBag.Message = "Cafe1";
            
            return View();
        }

       

        [HttpPost]
        public ActionResult Reserve(DateTime datum, string ime, string telefon, int brStola, string page)
        {
            Reservation.Reserve(datum, ime, telefon, brStola, page);

            return RedirectToAction(page, "Home");
        }

        [HttpPost]
        public JsonResult ListReservations(string kafic, DateTime datum)
        {
            return Json(Reservation.ReservedList(kafic, datum));
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            string username = "";
            string password = "";

            try
            {
                DbConnection.Instance.Open();
                string query = "SELECT * FROM login WHERE user = @user AND password = @pass";
                MySqlCommand cmd = new MySqlCommand(query, DbConnection.Instance);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pass);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    username = rdr["user"].ToString();
                    password = rdr["password"].ToString();
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

            if (username != "")
            {
                Session["user"] = username;
            }
            


            return RedirectToAction("About", "Home");
        }

        [HttpPost]
        public ActionResult Logout(string logout)
        {

            if (logout == "logout")
            {
                Session.Abandon();
                Response.Redirect("Login", true);
            }
            
            


            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {

            try
            {
                DbConnection.Instance.Open();
                string query = "DELETE FROM rezervacije WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, DbConnection.Instance);
                cmd.Parameters.AddWithValue("@id", id);
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
            
            return RedirectToAction("About", "Home");
        }
    }
}
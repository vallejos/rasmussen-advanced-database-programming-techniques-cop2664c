using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text;
// using System.Threading.Tasks;

namespace AdventureWorksCycleApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new List<TopSales>();

            try {
                SqlConnection conn = new SqlConnection("Server=tcp:vallejos.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Persist Security Info=False;User ID=vallejos;Password=Admin123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

                conn.Open();

                SqlCommand cmd = new SqlCommand("select top 10 Totals, Name, ProductID from SalesLT.vallejos_vTop10Sales", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    var sale = new TopSales();

                    sale.Totals = reader.GetInt32(0);
                    sale.Name = reader.GetString(1);
                    sale.ProductID = reader.GetInt32(2);

                    model.Add(sale);
                }

                // reader.Close();
                conn.Close();
            } catch (SqlException e) {
                Console.WriteLine(e.Message);
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

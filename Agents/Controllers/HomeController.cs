using System.Data;
using Agents.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Agents.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // TODO: This is not where this belongs! We'll continue refactoring and put it elsewhere later.
        private List<Agents> AllAgentData()
        {
            var agents = new List<Agents>();

            //NOTE: DO NOT EVER HARDCODE YOUR CONNECTION STRING IN CODE LIKE THIS
            using (var conn = new SqlConnection("Server=.;Database=AgentDb;Trusted_Connection=True;"))
            {
                conn.Open();

                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Agents";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var name = reader["name"].ToString();

                    agents.Add(new Agents
                    {
                        name = name
                    });
                }
            }

            return agents;
        }

        public IActionResult Index()
        {
            var agents = AllAgentData();

            var vm = new HomeViewModel();
            vm.Message = "Look at these wonderful books!";
            vm.Agents = agents;

            return View(vm);
        }

        public IActionResult AgentsWithAjax()
        {
            return View();
        }

        public IActionResult AgentData()
        {
            var books = AllAgentData();

            return Json(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
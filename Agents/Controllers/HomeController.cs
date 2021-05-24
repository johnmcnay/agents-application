
using Agents.ViewModels.Home;
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
        private List<Agent> AllAgentData()
        {
            var agents = new List<Agent>();

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
                    var name = reader["AgentName"].ToString();
                    var workingArea = reader["WorkingArea"].ToString();
                    var phoneNumber = reader["PhoneNo"].ToString();
                    var agentCode = reader["AgentCode"].ToString();

                    agents.Add(new Agent
                    {
                        name = name,
                        workingArea = workingArea,
                        phoneNumber = phoneNumber,
                        agentCode = agentCode
                    });
                }
            }

            return agents;
        }

        public IActionResult Index()
        {
            var agents = AllAgentData();

            var vm = new HomeViewModel();
            vm.Agents = agents;

            return View(vm);
        }

        public IActionResult AgentsWithAjax()
        {
            return View();
        }

        public IActionResult AgentData()
        {
            var agents = AllAgentData();

            return Json(agents);
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
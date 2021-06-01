using Agents.Models;
using Agents.ViewModels.AgentView;
using Agents.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly IConfiguration _configuration;
        private readonly AgentData _agentData;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, AgentData bookData)
        {
            _logger = logger;
            _configuration = configuration;
            _agentData = bookData;
        }

        public IActionResult Index()
        {
            var agents = _agentData.ActiveAgentData();

            var vm = new HomeViewModel();
            vm.Agents = agents;

            return View(vm);
        }

        public IActionResult AgentData()
        {
            var agents = _agentData.AllAgentData();

            return Json(agents);
        }

        //Home/Agent/A001
        public IActionResult Agent(string id)
        {
            var vm = new AgentViewModel();

            vm.agent = _agentData.GetOne(id);

            return View(vm);
        }

        [HttpGet]
        public IActionResult NewAgent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteAgent(string id) 
        {
            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string CommandText = "UPDATE Agents SET isActive = 0 WHERE AgentCode = @agentCode";

                SqlCommand dbCommand = new SqlCommand(CommandText, conn);
                dbCommand.Parameters.AddWithValue("@agentCode", id);
                dbCommand.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult NewAgent(Agent agent)
        {
            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string CommandText = "INSERT INTO Agents VALUES (@agentCode, @name, @workingArea, @commission, @phoneNumber);";

                SqlCommand dbCommand = new SqlCommand(CommandText, conn);
                dbCommand.Parameters.AddWithValue("@agentCode", agent.agentCode);
                dbCommand.Parameters.AddWithValue("@name", agent.name);
                dbCommand.Parameters.AddWithValue("@workingArea", agent.workingArea);
                dbCommand.Parameters.AddWithValue("@commission", agent.commission);
                dbCommand.Parameters.AddWithValue("@phoneNumber", agent.phoneNumber);
                dbCommand.ExecuteNonQuery();
            }

                return RedirectToAction("Index");
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
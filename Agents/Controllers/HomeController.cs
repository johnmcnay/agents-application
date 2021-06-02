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

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, AgentData agentData)
        {
            _logger = logger;
            _configuration = configuration;
            _agentData = agentData;
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
        public IActionResult EditAgent(Agent agent, string originalCode)
        {

            _agentData.EditAgent(agent, originalCode);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteAgent(string id) 
        {
            _agentData.DeleteAgent(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult NewAgent(Agent agent)
        {
         
            _agentData.AddAgent(agent);

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
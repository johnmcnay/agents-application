using Agents.Models;
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

namespace Books.Controllers
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
            var agents = _agentData.AllAgentData();

            var vm = new HomeViewModel();
            vm.Agents= agents;

            return View(vm);
        }

        public IActionResult BooksWithAjax()
        {
            return View();
        }

        public IActionResult AgentData()
        {
            var books = _agentData.AllAgentData();

            return Json(books);
        }

        //Home/Book/42
        public IActionResult Agent(int? id)
        {
            // TODO: Use the id passed and go get the book data.
            // Use that book data to create a new view.
            return View();
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
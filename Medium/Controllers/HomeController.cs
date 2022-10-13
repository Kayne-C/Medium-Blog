using BLL.Repositories.InterfaceRepositories;
using ENTITIES.Entity.Concrete;
using Medium.Filters;
using Medium.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Medium.Controllers
{
    //[LoggedUser]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<User> _genericRepository;
        public HomeController(ILogger<HomeController> logger, IGenericRepository<User> genericRepository)
        {
            _logger = logger;
            _genericRepository = genericRepository;
        }

        public IActionResult Index()
        {
            var list = _genericRepository.GetAll();
            return View();
        }

        public IActionResult AboutUs()
        {
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

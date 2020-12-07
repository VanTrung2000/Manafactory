using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using PXUK16.WEB.Help;
using PXUK16.WEB.Models;

namespace PXUK16.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Manufactory> manufactories = new List<Manufactory>();
            manufactories = Help.ApiHelpercs<List<Manufactory>>.HttpGetAsync("api/manafactory/gets");
            return View(manufactories);

        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateManufactory model)
        {
            if (ModelState.IsValid)
            {
                var result = new CreateManafactoryResult();
                result = Help.ApiHelpercs<CreateManafactoryResult>.HttpPostAsync("api/manafactory/create", "POST", model);

                if (result.ManufactoryId > 0)
                {
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(UpdateManufactory model)
        {
            if (ModelState.IsValid)
            {
                var result = new UpdateManafactoryResult();
                result = Help.ApiHelpercs<UpdateManafactoryResult>.HttpPostAsync("api/manafactory/update", "PUT", model);

                if (result.ManufactoryId > 0)
                {
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            return View(model);
        }
    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using ReflectionApp.Models;

namespace ReflectionApp.Controllers
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
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Detailed(string filePath)
        {
                 Assembly assembly = Assembly.LoadFile(filePath);
                var properties = new List<AssemblyProperties>();

                Type[] types = assembly.GetTypes();
                ViewBag.AssemblyName = assembly.FullName;
                 
                foreach (var type in types)
                 {
                    var methodName = "";
                    int NoOfMethods = 0;
                    MethodInfo[] methods = type.GetMethods();
                     foreach(var method in methods)
                     {
                        methodName = methodName + method + ",";
                        NoOfMethods++;
                     }
                    properties.Add(new AssemblyProperties {
                        type = type.Name,
                        no_of_methods = NoOfMethods,
                        method = methodName
                    });
                }

                ViewBag.Types = properties;


                return View();
        }

     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

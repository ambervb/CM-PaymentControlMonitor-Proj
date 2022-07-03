using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CMPaymentControlMonitor.WebInterface.Controllers
{
    public class A4Controller : Controller
    {
        public IActionResult Backlog()
        {
            return View();
        }
    }
}
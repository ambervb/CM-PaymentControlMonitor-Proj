using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

    //Easter egg
namespace CMPaymentControlMonitor.WebInterface.Controllers
{
    public class EasterEgg : Controller
    {
        // GET: /<controller>/
        public IActionResult PushToDevelop()
        {
            return View();
        }
    }
}

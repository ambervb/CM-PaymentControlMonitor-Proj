using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMPaymentControlMonitor.WebInterface.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        public IActionResult About() =>
            View("About");
    }
}
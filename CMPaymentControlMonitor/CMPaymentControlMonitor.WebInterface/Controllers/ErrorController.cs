using Microsoft.AspNetCore.Mvc;

namespace CMPaymentControlMonitor.WebInterface.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/Error/Status/{statusCode}")]
        public IActionResult Status404(int statusCode)
        {
            if (statusCode == 0)
            {
                statusCode = 500;
            }

            return View($"{statusCode}");
        }
    }
}
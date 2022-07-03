using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMPaymentControlMonitor.WebApi.Controllers
{
    [Route("api/alerts")]
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    public class AlertController : ControllerBase
    {
        private readonly IControlCheckRepository _controlCheckRepository;
        public AlertController(IControlCheckRepository controlCheckRepository)
        {
            _controlCheckRepository = controlCheckRepository;
        }

        /// <summary>
        /// Request to GET all Alerts
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public IActionResult Get()
        {
            var checks = _controlCheckRepository.ControlChecks.Where(cc => cc.Alerts.Any());

            var alerts = new List<Alert>();

            foreach(var cc in checks)
            {
                alerts.AddRange(cc.Alerts);
            }

            if (alerts.Count <= 0)
            {
                return ApiErrors.NoContent(Response);
            }

            return Ok(alerts);
        }
    }
}
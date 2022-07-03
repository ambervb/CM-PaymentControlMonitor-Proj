using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.WebApi.Models;
using Halcyon.HAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMPaymentControlMonitor.WebApi.Controllers
{
    [Route("api/controlchecks")]
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    public class ControlCheckController : ControllerBase
    {
        private IControlCheckRepository _controlCheckRepository;

        public ControlCheckController(IControlCheckRepository controlCheckRepository)
        {
            _controlCheckRepository = controlCheckRepository;
        }

        /// <summary>
        /// Request to GET all ControlChecks
        /// </summary>
        /// <returns>Returns all control checks</returns>
   
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            var controlChecks = _controlCheckRepository.ControlChecks;

            if (controlChecks == null)
            {
                return ApiErrors.NotFound(Response);
            }

            return Ok(controlChecks);
        }

        /// <summary>
        /// Request to GET a single ControlCheck by ID
        /// </summary>
        /// <param name="id">Specify control check id.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            var controlCheck = _controlCheckRepository.ControlChecks.FirstOrDefault(c => c.Id == id);

            if (controlCheck == null)
            {
                return ApiErrors.NotFound(Response);
            }

            var alerts = controlCheck.Alerts.ToList();

            if (alerts.IsNullOrEmpty())
            {
                return ApiErrors.NoContent(Response);
            }

            var response = new HALResponse(controlCheck)
                .AddLinks(new[] {
                    new Link("self", $"/api/controlchecks/{id}")

                })
                .AddEmbeddedCollection("alerts", alerts, new[] {
                    new Link("alert", $"/api/controlchecks/{id}/alerts")

                });

            return Ok(response);
        }

        /// <summary>
        /// Request to GET alerts from a single ControlCheck
        /// </summary>
        /// <param name="id">Specify control check id.</param>
        /// <returns></returns>
        [HttpGet("{id}/alerts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult GetAlerts(int id)
        {
            var check = _controlCheckRepository.ControlChecks.FirstOrDefault(cc => cc.Id == id);

            if (check == null)
            {
                return ApiErrors.NotFound(Response);
            }

            var alerts = new List<Alert>();

            alerts.AddRange(check.Alerts);

            if (alerts.IsNullOrEmpty())
            {
                return ApiErrors.NoContent(Response);
            }

            return Ok(alerts);
        }

        /// <summary>
        /// Request to POST a single ControlCheck
        /// </summary>
        /// <param name="controlCheck">Specify details for new control check.</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Put([FromBody] ControlCheck controlCheck)
        {
            if (!ModelState.IsValid)
            {
                return ApiErrors.BadRequest(Response);
            }

            _controlCheckRepository.Add(controlCheck);
            return CreatedAtAction(nameof(Get), controlCheck.Id, controlCheck);
        }

        /// <summary>
        /// Request to PUT a single ControlCheck by ID
        /// </summary>
        /// <param name="id">Specify control check id.</param>
        /// <param name="controlCheck">Specify control check id.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update([FromRoute] int id, [FromBody] ControlCheck controlCheck)
        {
            if(controlCheck == null)
            {
                return ApiErrors.BadRequest(Response);
            }

            var cc = _controlCheckRepository.ControlChecks.FirstOrDefault(c => c.Id == id);

            if(cc == null)
            {
                return ApiErrors.NotFound(Response);
            }

            _controlCheckRepository.Edit(id, controlCheck);
            
            return Ok(_controlCheckRepository.ControlChecks);
        }

        /// <summary>
        /// Request to DELETE a single ControlCheck by ID
        /// </summary>
        /// <param name="id">Specify control check id.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public IActionResult Delete([FromRoute] int id)
        {
            _controlCheckRepository.Delete(id);

            return Ok(_controlCheckRepository.ControlChecks);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.WebInterface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMPaymentControlMonitor.WebInterface.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IControlCheckRepository _controlCheckRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly IPaymentRepository _paymentRepository;

        public HomeController(IControlCheckRepository controlCheckRepository, 
            IAlertRepository alertRepository, IPaymentRepository paymentRepository)
        {
            _controlCheckRepository = controlCheckRepository;
            _alertRepository = alertRepository;
            _paymentRepository = paymentRepository;
        }

        public IActionResult Dashboard() =>
            View(_controlCheckRepository.ControlChecks);

        public IActionResult FailedTransactionListView(int id)
        {
            var controlCheck = _controlCheckRepository.ControlChecks
                .FirstOrDefault(cc => cc.Id == id);

            if (controlCheck == null) return RedirectToAction("Dashboard");

            controlCheck.Alerts = controlCheck.Alerts.Where(a => a.Resolved == false)
                .OrderByDescending(a => a.AlertCreatedOn)
                .ToList();
            return View(controlCheck);
        }

        public IActionResult TransactionDetailed(int id)
        {
            var alert = _alertRepository.Alerts.AsQueryable().AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alert == null) return RedirectToAction("Dashboard");

            return View(alert);
        }

        //History page
        public ViewResult History() =>
            View(_alertRepository.Alerts
                .Where(a => a.Resolved)
                .OrderByDescending(a => a.AlertCreatedOn)
                .ToList());

        // resolve alert
        [HttpPost]
        public ActionResult ResolveAlert(Alert alert)
        {
            if (!ModelState.IsValid)
            {
                var tempAlert = _alertRepository.Alerts.FirstOrDefault(a =>
                    a.Id == alert.Id);
                tempAlert.Comment = alert.Comment;
                ModelState.AddModelError(tempAlert.Comment, "The comment is too long");
                return View("TransactionDetailed", tempAlert);
            }

            var dbAlert = _alertRepository.Alerts
                .FirstOrDefault(a => a.Id == alert.Id);

            if (dbAlert != null)
            {
                dbAlert.Comment = alert.Comment;
                dbAlert.Resolved = true;
                _alertRepository.ResolveAlert(dbAlert);
            }

            return RedirectToAction("Dashboard");
        }
    }
}
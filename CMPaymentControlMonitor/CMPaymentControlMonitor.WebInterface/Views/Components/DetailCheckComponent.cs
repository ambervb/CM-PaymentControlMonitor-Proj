using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.WebInterface.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPaymentControlMonitor.WebInterface.Views.Components
{
    public class DetailCheckComponent : ViewComponent
    {
        private Alert alert;
        private readonly IPaymentRepository _paymentRepository;

        public DetailCheckComponent(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        

        //shows partial view in detail page matching the correct check type
        public IViewComponentResult Invoke(Alert alertDetail)
        {
            alert = alertDetail;
         
            switch (alert.Check.Id)
            {

                case 1: return View("~/Views/DetailViews/CheckDrugsInUS.cshtml", alert);

                case 2: return View("~/Views/DetailViews/CheckFTAFCountries.cshtml", alert);

                // Payments get retrieved where the buyer is the same
                // and all those within 3 days of the alert's creation date
                case 3:

                    return View("~/Views/DetailViews/Check72h15KAndPayMethod.cshtml", new AlertWithPayments()
                    {
                            Alert = alert,
                            Payments = _paymentRepository.Payments.Where(p =>
                                    p.Order.BuyerName == alert.PaymentId.Order.BuyerName
                                    && p.PaymentCreatedOn <= alert.PaymentId.PaymentCreatedOn
                                    && p.PaymentCreatedOn >= alert.PaymentId.PaymentCreatedOn
                                        .GetValueOrDefault().Subtract(new TimeSpan(3, 0, 0, 0)))
                    .OrderByDescending(p => p.PaymentCreatedOn)
                    .ToList()

                    });

                    
                case 4: return View("~/Views/DetailViews/Gambling.cshtml", alert);


                //retrieved where the merchant is the same
                // and all those within 3 days of the alert's creation date
                case 5:
                    return View("~/Views/DetailViews/Check72h100K.cshtml", new AlertWithPayments()
                    {
                        Alert = alert,
                        Payments = _paymentRepository.Payments.Where(p =>
                                p.Order.Merchant.Id == alert.PaymentId.Order.Merchant.Id
                                && p.PaymentCreatedOn.Value <= alert.PaymentId.PaymentCreatedOn.Value
                                && p.PaymentCreatedOn.Value >= alert.PaymentId.PaymentCreatedOn.Value
                                    .Subtract(new TimeSpan(3, 0, 0, 0)))
                            .OrderByDescending(p => p.PaymentCreatedOn)
                            .ToList()
                    });


                default:  return null;
            }
        }

        
    }
}

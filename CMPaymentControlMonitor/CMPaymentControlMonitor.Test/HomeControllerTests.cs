using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CMPaymentControlMonitor.Domain.Models;
using CMPaymentControlMonitor.WebInterface.Controllers;
using CMPaymentControlMonitor.WebInterface.ViewModels;
using CMPaymentControlMonitor.WebInterface.Views.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace CMPaymentControlMonitor.Test
{
    public class HomeControllerTests
    {
        [Fact]
        public void Dashboard_Shows_Correct_Amount_Of_ControlChecks()
        {
            // Arrange
            var mockControlCheckRepository = new Mock<IControlCheckRepository>();
            mockControlCheckRepository.Setup(cc => cc.ControlChecks)
                .Returns(new List<ControlCheck>()
                {
                    new ControlCheck(),
                    new ControlCheck(),
                    new ControlCheck(),
                    new ControlCheck()
                });

            var controller = new HomeController(mockControlCheckRepository.Object, null, null);

            // Act
            var dashboardList = ((controller.Dashboard() as ViewResult).ViewData.Model as List<ControlCheck>);

            // Assert
            Assert.Equal(4, dashboardList.Count);
        }

        [Fact]
        public void History_Shows_Only_Resolved_Alerts()
        {
            // Arrange
            var mockAlertRepo = new Mock<IAlertRepository>();
            mockAlertRepo.Setup(a => a.Alerts).Returns(
                new List<Alert>()
                {
                    new Alert()
                    {
                        Id = 1,
                        Resolved = false
                    },
                    new Alert()
                    {
                        Id = 2,
                        Resolved = true
                    },
                    new Alert()
                    {
                        Id = 3,
                        Resolved = true
                    },
                    new Alert()
                    {
                        Id = 4,
                        Resolved = false
                    },
                    new Alert()
                    {
                        Id = 5,
                        Resolved = true
                    }
                });

            var controller = new HomeController(null, mockAlertRepo.Object, null);

            // Act
            var historyList = (controller.History().ViewData.Model as List<Alert>);

            // Assert
            Assert.Equal(3, historyList.Count);
            Assert.NotNull(historyList.FirstOrDefault(a => a.Id == 2));
            Assert.NotNull(historyList.FirstOrDefault(a => a.Id == 3));
            Assert.NotNull(historyList.FirstOrDefault(a => a.Id == 5));
            Assert.Null(historyList.FirstOrDefault(a => a.Id == 1));
            Assert.Null(historyList.FirstOrDefault(a => a.Id == 4));
        }

        [Fact]
        public void FailedTransactionListView_Shows_Only_Alerts_That_Are_Not_Resolved()
        {
            // Arrange
            var controlCheck = new ControlCheck()
            {
                Id = 1
            };

            var alert1 = new Alert()
            {
                Id = 1,
                Resolved = true,
                Check = controlCheck
            };

            var alert2 = new Alert()
            {
                Id = 2,
                Resolved = false,
                Check = controlCheck
            };

            var alert3 = new Alert()
            {
                Id = 3,
                Resolved = false,
                Check = controlCheck
            };

            var alert4 = new Alert()
            {
                Id = 4,
                Resolved = true,
                Check = controlCheck
            };

            var alert5 = new Alert()
            {
                Id = 5,
                Resolved = false,
                Check = controlCheck
            };

            controlCheck.Alerts = new List<Alert>()
            {
                alert1, alert2, alert3, alert4, alert5
            };


            var mockControlCheckRepository = new Mock<IControlCheckRepository>();
            mockControlCheckRepository.Setup(cc => cc.ControlChecks).Returns(
                new List<ControlCheck>()
                {
                    controlCheck
                });

            var controller = new HomeController(mockControlCheckRepository.Object, null, null);

            // Act
            var testCC = (controller.FailedTransactionListView(controlCheck.Id) as ViewResult).Model as ControlCheck;

            // Assert
            Assert.NotNull(testCC);
            Assert.Equal(3, testCC.Alerts.Count);
            Assert.NotNull(testCC.Alerts.FirstOrDefault(a => a.Id == 2));
            Assert.NotNull(testCC.Alerts.FirstOrDefault(a => a.Id == 3));
            Assert.NotNull(testCC.Alerts.FirstOrDefault(a => a.Id == 5));
            Assert.Null(testCC.Alerts.FirstOrDefault(a => a.Id == 1));
            Assert.Null(testCC.Alerts.FirstOrDefault(a => a.Id == 4));
        }

        [Fact]
        public void FailedTransactionListView_Shows_Only_Alerts_That_Are_Connected_To_Specific_ControlCheck()
        {
            // Arrange
            var controlCheck = new ControlCheck()
            {
                Id = 1
            };
            var wrongControlCheck = new ControlCheck()
            {
                Id = 2
            };

            var alert1 = new Alert()
            {
                Id = 1,
                Resolved = false,
                Check = wrongControlCheck
            };

            var alert2 = new Alert()
            {
                Id = 2,
                Resolved = false,
                Check = controlCheck
            };

            var alert3 = new Alert()
            {
                Id = 3,
                Resolved = false,
                Check = controlCheck
            };

            var alert4 = new Alert()
            {
                Id = 4,
                Resolved = false,
                Check = wrongControlCheck
            };

            var alert5 = new Alert()
            {
                Id = 5,
                Resolved = false,
                Check = controlCheck
            };

            controlCheck.Alerts = new List<Alert>()
            {
                alert2, alert3, alert5
            };

            wrongControlCheck.Alerts = new List<Alert>()
            {
                alert1, alert4
            };


            var mockControlCheckRepository = new Mock<IControlCheckRepository>();
            mockControlCheckRepository.Setup(cc => cc.ControlChecks).Returns(
                new List<ControlCheck>()
                {
                    wrongControlCheck, controlCheck
                });
            

            var controller = new HomeController(mockControlCheckRepository.Object, null, null);

            // Act
            var testCC = (controller.FailedTransactionListView(controlCheck.Id) as ViewResult).Model as ControlCheck;

            // Assert
            Assert.NotNull(testCC);
            Assert.Equal(3, testCC.Alerts.Count);
            Assert.NotNull(testCC.Alerts.FirstOrDefault(a => a.Id == 2));
            Assert.NotNull(testCC.Alerts.FirstOrDefault(a => a.Id == 3));
            Assert.NotNull(testCC.Alerts.FirstOrDefault(a => a.Id == 5));
            Assert.Null(testCC.Alerts.FirstOrDefault(a => a.Id == 1));
            Assert.Null(testCC.Alerts.FirstOrDefault(a => a.Id == 4));
        }

        [Fact]
        public void TransactionDetailed_Presents_Correct_Alert()
        {
            // Arrange
            var controlCheck = new ControlCheck()
            {
                Id = 1
            };

            var testAlert = new Alert()
            {
                Check = controlCheck,
                Id = 2,
                Resolved = false
            };
            var mockAlertRepo = new Mock<IAlertRepository>();
            mockAlertRepo.Setup(a => a.Alerts).Returns(
                new List<Alert>()
                {
                    new Alert()
                    {
                        Check = controlCheck,
                        Id = 1,
                        Resolved = false
                    },
                    testAlert,
                    new Alert()
                    {
                        Check = controlCheck,
                        Id = 3,
                        Resolved = false
                    }
                });

            var controller = new HomeController(null, mockAlertRepo.Object, null);

            // Act
            var controllerAlert = ((controller.TransactionDetailed(testAlert.Id) as ViewResult).ViewData.Model as Alert);

            // Assert
            Assert.NotNull(controllerAlert);
            Assert.Equal(testAlert, controllerAlert);
        }

        [Fact]
        public void TransactionDetailed_Does_Not_Crash_Application()
        {
            // Arrange
            var testAlert = new Alert()
            {
                Id = 2,
                Resolved = false
            };
            var mockAlertRepo = new Mock<IAlertRepository>();
            mockAlertRepo.Setup(a => a.Alerts).Returns(
                new List<Alert>()
                {
                    new Alert()
                    {
                        Id = 1,
                        Resolved = false
                    },
                    testAlert,
                    new Alert()
                    {
                        Id = 3,
                        Resolved = false
                    }
                });

            var controller = new HomeController(null, mockAlertRepo.Object, null);

            // Act
            var actionName = (controller.TransactionDetailed(5) as RedirectToActionResult).ActionName;

            // Assert
            Assert.NotNull(actionName);
            Assert.Equal("Dashboard", actionName);
        }

        [Fact]
        public void TransactionDetailed_Gives_Correct_Payments_With_For_Check3()
        {
            // Arrange
            var buyerName = "Hornbach";

            var payment1 = new Payment()
            {
                Id = 1,
                PaymentCreatedOn = new DateTime(2018, 01, 02, 12, 0, 0),
                Order = new Order()
                {
                    BuyerName = buyerName
                }
            };

            // Different BuyerName
            var payment2 = new Payment()
            {
                Id = 2,
                PaymentCreatedOn = new DateTime(2018, 01, 02, 0, 0, 0),
                Order = new Order
                {
                    BuyerName = "Different buyer name"
                }
            };

            var payment3 = new Payment()
            {
                Id = 3,
                PaymentCreatedOn = new DateTime(2018, 01, 01, 18, 0, 0),
                Order = new Order
                {
                    BuyerName = buyerName
                }
            };

            // Date outside of 72 hour range
            var payment4 = new Payment()
            {
                Id = 4,
                PaymentCreatedOn = new DateTime(2017, 12, 25, 18, 0, 0), 
                Order = new Order
                {
                    BuyerName = buyerName
                }
            };

            var testAlert = new Alert()
            {
                Check = new ControlCheck()
                {
                    Id = 3
                },
                Id = 2,
                Resolved = false,
                PaymentId = payment1
            };

          

            var mockPaymentRepo = new Mock<IPaymentRepository>();
            mockPaymentRepo.Setup(p => p.Payments).Returns(
                new List<Payment>()
                {
                    payment1, payment2, payment3, payment4
                }.AsQueryable);

            var viewComp = new DetailCheckComponent(mockPaymentRepo.Object);

            // Act
            var testAlertWithPayments = (viewComp.Invoke(testAlert) as ViewViewComponentResult).ViewData.Model as AlertWithPayments;

            // Assert
            Assert.NotNull(testAlertWithPayments);
            Assert.Equal(2, testAlertWithPayments.Payments.Count);
            Assert.NotNull(testAlertWithPayments.Payments.FirstOrDefault(p => p.Id == payment1.Id));
            Assert.NotNull(testAlertWithPayments.Payments.FirstOrDefault(p => p.Id == payment3.Id));
            Assert.Null(testAlertWithPayments.Payments.FirstOrDefault(p => p.Id == payment2.Id));
            Assert.Null(testAlertWithPayments.Payments.FirstOrDefault(p => p.Id == payment4.Id));
        }

        [Fact]
        public void TransactionDetailed_Gives_Correct_Payments_With_For_Check5()
        {
            // Arrange
            var merchant = new Merchant()
            {
                Id = "Idtje"
            };


            var controlCheck = new ControlCheck()
            {
                Id = 5
            };

            var payment1 = new Payment()
            {
                Id = 1,
                PaymentCreatedOn = new DateTime(2018, 01, 02, 12, 0, 0),
                Order = new Order()
                {
                    Merchant = merchant
                }
            };

            // Different BuyerName
            var payment2 = new Payment()
            {
                Id = 2,
                PaymentCreatedOn = new DateTime(2018, 01, 02, 0, 0, 0),
                Order = new Order
                {
                    Merchant = new Merchant()
                    {
                        Id = "nope"
                    }
                }
            };

            var payment3 = new Payment()
            {
                Id = 3,
                PaymentCreatedOn = new DateTime(2018, 01, 01, 18, 0, 0),
                Order = new Order
                {
                    Merchant = merchant
                }
            };

            // Date outside of 72 hour range
            var payment4 = new Payment()
            {
                Id = 4,
                PaymentCreatedOn = new DateTime(2017, 12, 25, 18, 0, 0),
                Order = new Order
                {
                    Merchant = merchant
                }
            };

            var testAlert = new Alert()
            {
                Check = controlCheck,
                Id = 2,
                Resolved = false,
                PaymentId = payment1
            };

            var mockAlertRepo = new Mock<IAlertRepository>();
            mockAlertRepo.Setup(a => a.Alerts).Returns(
                new List<Alert>()
                {
                    new Alert()
                    {
                        Check = controlCheck,
                        Id = 1,
                        Resolved = false
                    },
                    testAlert,
                    new Alert()
                    {
                        Check = controlCheck,
                        Id = 3,
                        Resolved = false
                    }
                });

            var mockPaymentRepo = new Mock<IPaymentRepository>();
            mockPaymentRepo.Setup(p => p.Payments).Returns(
                new List<Payment>()
                {
                    payment1, payment2, payment3, payment4
                }.AsQueryable);

            var viewComp = new DetailCheckComponent(mockPaymentRepo.Object);

            // Act
            var testAlertWithPayments = (viewComp.Invoke(testAlert) as ViewViewComponentResult).ViewData.Model as AlertWithPayments;

            // Assert
            Assert.NotNull(testAlertWithPayments);
            Assert.Equal(2, testAlertWithPayments.Payments.Count);
            Assert.NotNull(testAlertWithPayments.Payments.FirstOrDefault(p => p.Id == payment1.Id));
            Assert.NotNull(testAlertWithPayments.Payments.FirstOrDefault(p => p.Id == payment3.Id));
            Assert.Null(testAlertWithPayments.Payments.FirstOrDefault(p => p.Id == payment2.Id));
            Assert.Null(testAlertWithPayments.Payments.FirstOrDefault(p => p.Id == payment4.Id));
        }

        [Fact]
        public void ResolveAlert_Resolves_Comment()
        {
            // Arrange
            var alert = new Alert
            {
                Id = 1,
                Resolved = false
            };

            // Create test repository and added alert
            var alertRepository = new TestAlertRepository();
            alertRepository.Alerts.Add(alert);

            // Create the alert that it's going to be updated to (aka the comment string)
            var comment = "Check123";
            var updatedAlert = alert;
            updatedAlert.Comment = comment;

            // Create controller pass the test repository along
            var homeController = new HomeController(null, alertRepository, null);

            // Act
            homeController.ResolveAlert(updatedAlert);

            // Assert

            // Make sure the alert has actually been created
            Assert.NotNull(alertRepository.Alerts.FirstOrDefault());

            // Checks if the alert has actually been changed
            Assert.True(alertRepository.Alerts.FirstOrDefault().Resolved);
            Assert.True(alertRepository.Alerts.FirstOrDefault().Comment == comment);

        }

        [Fact]
        public void ResolveAlert_Will_Not_Do_Anything_When_Alert_Does_Not_exist()
        {
            //Arrange
            var alert = new Alert
            {
                Id = 1,
                Resolved = false
            };

            var alertRepository = new TestAlertRepository();
            alertRepository.Alerts.Add(alert);

            var AlertController = new HomeController(null, alertRepository, null);

            //Act
            AlertController.ResolveAlert(new Alert());
            
            //Assert
            Assert.DoesNotContain(alertRepository.Alerts, a => a.Resolved);
            Assert.False(alert.Resolved);
        }
    }
}
